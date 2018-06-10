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
            comboBoxMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxYear.DropDownStyle = ComboBoxStyle.DropDownList;
            dgvRaport1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            db = new Db();
            onDateSelected(sender, null);

            reloadPupilsList();
        }

        private void addPupil(object sender, EventArgs e)
        {
            String name = textBoxDodaj.Text;
            String insert = "INSERT INTO pupils (name) values ('"+name+"')";
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
            selectedDateString = calendar.SelectionStart.Date.ToString("dd.MM.yyyy");
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
            if (!InputValidator.validateTime(newTime))
            {
                MessageBox.Show("Niepoprawny format danych. Uzyj [hh:mm]");
                reloadRegister();
                return;
            }
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
                            if (isTimeOut(pupil))
                            {
                                String timeOut = getTimeOut(pupil);
                                if (!InputValidator.compareTimes(newTime, timeOut))
                                {
                                    MessageBox.Show("Godzina przyjscia nie moze byc pozniejsza niz godzina wyjscia");
                                    reloadRegister();
                                    return;
                                }
                            }
                            updateTimeIn(pupil, newTime);
                        }
                        else
                        {
                            insertTimeIn(pupil, newTime);
                        }
                        //MessageBox.Show("Zaktualizowano");
                        break;
                    }
                case 2:
                    {
                        if (isTimeIn(pupil))
                        {
                            String timeIn = getTimeIn(pupil);
                            if(InputValidator.compareTimes(timeIn, newTime))
                            {
                                updateTimeOut(pupil, newTime);
                            }
                            else
                            {
                                MessageBox.Show("Godzina przyjscia nie moze byc pozniejsza niz godzina wyjscia");
                            }
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

        private void updateTimeIn(String pupilId, String time)
        {
            String update = "UPDATE register SET timeIn = CONCAT(date(timeIn),' " + time + ":00') " +
                "WHERE pupilId = " + pupilId + " AND DATE_FORMAT(timeIn, '%d.%m.%Y') = '" + selectedDateString + "';";
            db.executeNonQuery(update);
            reloadRegister();
        }

        private void updateTimeOut(String pupilId, String time)
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

        private Boolean isTimeOut(String pupilId)
        {
            String query = "SELECT IF((SELECT count(timeOut) FROM register " +
                "WHERE pupilId=" + pupilId + " AND DATE_FORMAT(timeIn, '%d.%m.%Y') = '" + selectedDateString + "')>0, 1, 0);";

            if (db.executeQueryScalarInt(query) == 1) return true;
            else return false;
        }

        private String getTimeIn(String pupilId)
        {
            String query = "SELECT DATE_FORMAT(timeIn, '%H:%i') FROM register " +
                "WHERE pupilId=" + pupilId + " AND DATE_FORMAT(timeIn, '%d.%m.%Y') = '" + selectedDateString + "';";

            return db.executeQueryScalarString(query);
        }

        private String getTimeOut(String pupilId)
        {
            String query = "SELECT DATE_FORMAT(timeOut, '%H:%i') FROM register " +
                "WHERE pupilId=" + pupilId + " AND DATE_FORMAT(timeIn, '%d.%m.%Y') = '" + selectedDateString + "';";

            return db.executeQueryScalarString(query);
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

        private void generateRaport1(object sender, MouseEventArgs e)
        {
            dgvRaport1.DataSource = null;
            dgvRaport1.Rows.Clear();

            String month = comboBoxMonth.SelectedItem.ToString();
            String year = comboBoxYear.SelectedItem.ToString();
            String pupil = textBoxPupilNumber.Text;

            //MessageBox.Show(month + ", " + year + ", " + pupil);

            String query = "SELECT "+

                "DATE_FORMAT(timeIn, '%d') dzien, "
	            +"DATE_FORMAT(timeIn, '%H:%i') przyjscie, " 
	            +"DATE_FORMAT(timeOut, '%H:%i') wyjscie, "
	            +"IF(CONCAT(DATE(timeIn), ' 08:30:00') > timeIn, DATE_FORMAT(TIMEDIFF(CONCAT(DATE(timeIn), ' 08:30:00'), timeIn), '%H:%i'), DATE_FORMAT(CONCAT(DATE(timeIn), ' 00:00:00'), '%H:%i')) rano, "
   	            +"IF(CONCAT(DATE(timeOut), ' 13:30:00') < timeOut, DATE_FORMAT(TIMEDIFF(timeOut, CONCAT(DATE(timeOut), ' 13:30:00')), '%H:%i'), DATE_FORMAT(CONCAT(DATE(timeOut), ' 00:00:00'), '%H:%i')) popoludniu, "

	            +"CONVERT(DATE_FORMAT(IF(CONCAT(DATE(timeIn), ' 08:30:00') > timeIn, TIMEDIFF(CONCAT(DATE(timeIn), ' 08:30:00'), timeIn), CONVERT(CONCAT(DATE(timeIn), ' 00:00:00'), DATETIME)), '%H'), UNSIGNED) + "
                +"IF(CONVERT(DATE_FORMAT(IF(CONCAT(DATE(timeIn), ' 08:30:00') > timeIn, TIMEDIFF(CONCAT(DATE(timeIn), ' 08:30:00'), timeIn), CONVERT(CONCAT(DATE(timeIn), ' 00:00:00'), DATETIME)), '%i'), UNSIGNED) > 0, 1, 0) GodzinRano, "
    
                +"CONVERT(DATE_FORMAT(IF(CONCAT(DATE(timeOut), ' 13:30:00') < timeOut, TIMEDIFF(timeOut, CONCAT(DATE(timeOut), ' 13:30:00')), CONVERT(CONCAT(DATE(timeOut), ' 00:00:00'), DATETIME)), '%H'), UNSIGNED) + "
                +"IF(CONVERT(DATE_FORMAT(CONVERT(IF(CONCAT(DATE(timeOut), ' 13:30:00') < timeOut, TIMEDIFF(timeOut, CONCAT(DATE(timeOut), ' 13:30:00')), CONVERT(CONCAT(DATE(timeOut), ' 00:00:00'), DATETIME)), DATETIME), '%i'), UNSIGNED) > 0, 1, 0) GodzinPopoludniu "

                +"FROM register "
                +"WHERE "

                +"DATE_FORMAT(timeIn, '%m') = '"+month+"' "
                +"AND DATE_FORMAT(timeIn, '%Y') = '"+year+"' "
                +"AND pupilId = "+pupil+"; ";

            //MessageBox.Show(query);

            dgvRaport1.DataSource = db.executeQuery(query);
            dgvRaport1.DataMember = "std";
            dgvRaport1.Refresh();

        }
    }
}