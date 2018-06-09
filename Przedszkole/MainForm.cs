using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace Przedszkole
{
    public partial class MainForm : Form
    {

        private MySqlConnection conn = new MySqlConnection();
        private String myConnectionString = "";
        private String selectedDateString;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFormOnLoad(object sender, EventArgs e)
        {

            onDateSelected(sender, null);
            conn.ConnectionString = myConnectionString;

            try
            {
                conn.Open();
                reloadPupilsList();
            }
            catch (Exception e1)
            {
                MessageBox.Show("Not connected due to: " + e1.ToString());
            }
        }

        private void addPupil(object sender, EventArgs e)
        {
            String insert = "INSERT INTO pupils (name) values (@name)";
            String name = textBoxDodaj.Text;
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = insert;
            command.Parameters.AddWithValue("@name", name);
            command.ExecuteNonQuery();
            reloadPupilsList();
        }

        private void reloadPupilsList()
        {
            dgvPupils.DataSource = null;
            dgvPupils.Rows.Clear();
            String query = "select pupilsSqlId Numer, name Imie from pupils order by pupilsSqlId asc";

            MySqlDataAdapter mySqlDataAdapter;
            DataSet dataSet;
            try
            {
                mySqlDataAdapter = new MySqlDataAdapter(query, conn);
                dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet, "std");
                dgvPupils.DataSource = dataSet;
                dgvPupils.DataMember = "std";
                dgvPupils.Refresh();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
            finally
            {
                mySqlDataAdapter = null;
                dataSet = null;
            }
        }

        private void onDateSelected(object sender, DateRangeEventArgs e)
        {
            selectedDateString = calendar.SelectionStart.ToShortDateString();
            labelSelectedDate.Text = selectedDateString;
            clearRegister();
        }

        private void reloadRegister(object sender, EventArgs e)
        {
            clearRegister();

            String query = "SELECT p.pupilsSqlId Numer, DATE_FORMAT(r.timeIn, '%H:%i') Przyjscie, " +
                "DATE_FORMAT(r.timeOut, '%H:%i') Wyjscie FROM pupils p " +
                "LEFT JOIN (SELECT * FROM register WHERE DATE_FORMAT(timeIn, '%d.%m.%Y') = '" + selectedDateString+"') r " +
                "ON p.pupilsSqlId = r.pupilId ORDER BY p.pupilsSqlId ASC; ";

            MySqlDataAdapter mySqlDataAdapter;
            DataSet dataSet;
            try
            {
                mySqlDataAdapter = new MySqlDataAdapter(query, conn);
                dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet, "std");
                dgvRegister.DataSource = dataSet;
                dgvRegister.DataMember = "std";
                dgvRegister.Refresh();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
            finally
            {
                mySqlDataAdapter = null;
                dataSet = null;
            }
        }

        private void clearRegister()
        {
            dgvRegister.DataSource = null;
            dgvRegister.Rows.Clear();
        }

        private void onCellEdit(object sender, DataGridViewCellEventArgs e)
        {
            String newTime = dgvRegister[e.ColumnIndex, e.RowIndex].Value.ToString();
            String pupil = dgvRegister[0, e.RowIndex].Value.ToString();
            int column = e.ColumnIndex;
            switch (column)
            {
                case 0:
                    {
                        MessageBox.Show("Tej kolumny nie można edytować");
                        break;
                    }
                case 1:
                    {
                        if (isTimeIn(pupil))
                        {
                            updateInTime(pupil, newTime);
                        }
                        else
                        {
                            insertTimeIn(pupil, newTime);
                        }
                        MessageBox.Show("Zaktualizowano");
                        break;
                    }
                case 2:
                    {
                        if (isTimeIn(pupil))
                        {
                            updateOutTime(pupil, newTime);
                        }
                        else
                        {
                            MessageBox.Show("Wypelnij najpierw godzine przyjscia");
                        }
                        break;
                    }            
                  
            }
            clearRegister();
        }

        private void updateInTime(String pupilId, String time)
        {
            String update = "UPDATE register SET timeIn = CONCAT(date(timeIn),' " + time + ":00') " +
                "WHERE pupilId = " + pupilId + " AND DATE_FORMAT(timeIn, '%d.%m.%Y') = '" + selectedDateString + "';";
            MessageBox.Show(update);
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = update;
                command.ExecuteNonQuery();
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
            
        }

        private void updateOutTime(String pupilId, String time)
        {
            String update = "UPDATE register SET timeOut = CONCAT(date(timeIn),' " + time + ":00') " +
                "WHERE pupilId = " + pupilId + " AND DATE_FORMAT(timeIn, '%d.%m.%Y') = '" + selectedDateString + "';";

            MessageBox.Show(update);

            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = update;
                command.ExecuteNonQuery();
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
 
        }

        private Boolean isTimeIn(String pupilId)
        {
            String query = "SELECT IF((SELECT count(timeIn) FROM register " +
                "WHERE pupilId="+pupilId+" AND DATE_FORMAT(timeIn, '%d.%m.%Y') = '"+selectedDateString+"')>0, 1, 0);";

            MySqlCommand command;
            int result;

            try
            {
                command = new MySqlCommand(query, conn);
                result = Convert.ToInt32(command.ExecuteScalar());
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
                result = -1;
            }

            if (result == 1) return true;
            else return false;

        }

        private void insertTimeIn(String pupilId, String newTime)
        {
            String insert = "INSERT INTO register (pupilId, timeIn) " +
                "VALUES ("+pupilId+", STR_TO_DATE(CONCAT('"+selectedDateString+"', ' ', '"+newTime+"'), '%d.%m.%Y %H:%i'));";

            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = insert;
                command.ExecuteNonQuery();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }

        }

    }
}
