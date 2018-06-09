﻿using System;
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

        MySqlConnection conn = new MySqlConnection();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFormOnLoad(object sender, EventArgs e)
        {
            String myConnectionString = "";

            conn.ConnectionString = myConnectionString;

            try
            {
                conn.Open();

                String query = "select * from pupils order by pupilsSqlId asc";
                try
                {
                    MySqlDataAdapter mysqlDataAdapter = new MySqlDataAdapter(query, conn);
                    DataSet dataSet = new DataSet();
                    mysqlDataAdapter.Fill(dataSet, "std");
                    dgvPupils.DataSource = dataSet;
                    dgvPupils.DataMember = "std";
                    dgvPupils.Refresh();
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString());
                }

                MessageBox.Show("Connected");
            }
            catch (Exception e1)
            {
                MessageBox.Show("Not connected due to: " + e1.ToString());
            }
        }
    }
}
