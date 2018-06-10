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

        public void executeNonQuery(String statement)
        {
            MySqlConnection connection;
            connection = new MySqlConnection();
            connection.ConnectionString = myConnectionString;
            connection.Open();
            MySqlCommand command;
            try
            {
                command = connection.CreateCommand();
                command.CommandText = statement;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                command = null;
                connection.Close();
            }

        }

        public DataSet executeQuery(String query)
        {
            MySqlConnection connection;
            connection = new MySqlConnection();
            connection.ConnectionString = myConnectionString;
            connection.Open();
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
            finally
            {
                dataSet = null;
                mySqlDataAdapter = null;
                connection.Close();
            }
        }

        public int executeQueryScalarInt(String query)
        {
            MySqlConnection connection;
            connection = new MySqlConnection();
            connection.ConnectionString = myConnectionString;
            connection.Open();
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
            finally
            {
                command = null;
                connection.Close();
            }

            return result;
        }

        public String executeQueryScalarString(String query)
        {
            MySqlConnection connection;
            connection = new MySqlConnection();
            connection.ConnectionString = myConnectionString;
            connection.Open();
            MySqlCommand command;
            String result = null;

            try
            {
                command = new MySqlCommand(query, connection);
                result = Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
            finally
            {
                command = null;
                connection.Close();
            }

            return result;
        }

    }
}