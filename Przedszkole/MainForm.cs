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

        private String selectedDateString;
        private Db db;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFormOnLoad(object sender, EventArgs e)
        {
            db = new Db();
            onDateSelected(sender, null);

            if (db.isConnected) reloadPupilsList();
        }

        private void addPupil(object sender, EventArgs e)
        {
            String name = textBoxDodaj.Text;
            String insert = "INSERT INTO pupils (name) values '"+name+"'";
            db.executeNonQuery(insert);
 
            reloadPupilsList();
        }

        private void reloadPupilsList()
        {
            dgvPupils.DataSource = null;
            dgvPupils.Rows.Clear();
            String query = "select pupilsSqlId Numer, name Imie from pupils order by pupilsSqlId asc";         
            dgvPupils.DataSource = db.executeQuery(query);
            dgvPupils.DataMember = "std";
            dgvPupils.Refresh();

        }

        private void onDateSelected(object sender, DateRangeEventArgs e)
        {
            selectedDateString = calendar.SelectionStart.ToShortDateString();
            labelSelectedDate.Text = selectedDateString;
            reloadRegister();
        }

        private void reloadRegister()
        {
            clearRegister();

            String query = "SELECT p.pupilsSqlId Numer, DATE_FORMAT(r.timeIn, '%H:%i') Przyjscie, " +
                "DATE_FORMAT(r.timeOut, '%H:%i') Wyjscie FROM pupils p " +
                "LEFT JOIN (SELECT * FROM register WHERE DATE_FORMAT(timeIn, '%d.%m.%Y') = '" + selectedDateString+"') r " +
                "ON p.pupilsSqlId = r.pupilId ORDER BY p.pupilsSqlId ASC; ";

            dgvRegister.DataSource = db.executeQuery(query);
            dgvRegister.DataMember = "std";
            dgvRegister.Refresh();

        }

        private void clearRegister()
        {
            dgvRegister.DataSource = null;
            dgvRegister.Rows.Clear();
        }

        private void onCellEdit(object sender, DataGridViewCellEventArgs e)
        {
            String newTime = dgvRegister[e.ColumnIndex, e.RowIndex].Value.ToString();
            if (!InputValidator.validateTime(newTime)) return;
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
            reloadRegister();
        }

        private void updateInTime(String pupilId, String time)
        {
            String update = "UPDATE register SET timeIn = CONCAT(date(timeIn),' " + time + ":00') " +
                "WHERE pupilId = " + pupilId + " AND DATE_FORMAT(timeIn, '%d.%m.%Y') = '" + selectedDateString + "';";
            db.executeNonQuery(update);
            reloadRegister();
        }

        private void updateOutTime(String pupilId, String time)
        {
            String update = "UPDATE register SET timeOut = CONCAT(date(timeIn),' " + time + ":00') " +
                "WHERE pupilId = " + pupilId + " AND DATE_FORMAT(timeIn, '%d.%m.%Y') = '" + selectedDateString + "';";
            db.executeNonQuery(update);
            reloadRegister();
        }

        private Boolean isTimeIn(String pupilId)
        {
            String query = "SELECT IF((SELECT count(timeIn) FROM register " +
                "WHERE pupilId="+pupilId+" AND DATE_FORMAT(timeIn, '%d.%m.%Y') = '"+selectedDateString+"')>0, 1, 0);";

            if (db.executeQueryScalarInt(query) == 1) return true;
            else return false;

        }

        private void insertTimeIn(String pupilId, String newTime)
        {
            String insert = "INSERT INTO register (pupilId, timeIn) " +
                "VALUES ("+pupilId+", STR_TO_DATE(CONCAT('"+selectedDateString+"', ' ', '"+newTime+"'), '%d.%m.%Y %H:%i'));";

            db.executeNonQuery(insert);
            reloadRegister();
        }

        private void reloadReg(object sender, EventArgs e)
        {
            reloadRegister();
        }
    }
}