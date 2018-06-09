using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace Przedszkole
{
    class Db
    {

        const String myConnectionString = "";
        private MySqlConnection connection;
        public Boolean isConnected = false;

        public Db()
        {
            connection = new MySqlConnection();
            connection.ConnectionString = myConnectionString;

            try
            {
                connection.Open();
                isConnected = true;
            }
            catch (Exception e1)
            {
                MessageBox.Show("Not connected due to: " + e1.ToString());
            }
        }

        public void executeNonQuery(String statement)
        {
            MySqlCommand command;
            try
            {
                command = connection.CreateCommand();
                command.CommandText = statement;
                command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                command = null;
            }

        }

        public DataSet executeQuery(String query) 
        {
            MySqlDataAdapter mySqlDataAdapter;
            DataSet dataSet;
            try
            {
                mySqlDataAdapter = new MySqlDataAdapter(query, connection);
                dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet, "std");
                return dataSet;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                return null;
            }
        }

        public int executeQueryScalarInt(String query)
        {
            MySqlCommand command;
            int result;

            try
            {
                command = new MySqlCommand(query, connection);
                result = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                result = -1;
            }

            return result;
        }

    }
}
