using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Core.AdoNET
{
    public class GenericDataAccess
    {
        public GenericDataAccess()
        {
        }
        public DataTable ExecuteSelectCommand(SqlCommand command)
        {

            DataTable table;
            try
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                table = new DataTable();
                table.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
            return table;
        }


        // execute an update, delete, or insert command
        // and return the number of affected rows
        public int ExecuteNonQuery(SqlCommand command)
        {
            // The number of affected rows
            int affectedRows = -1;
            // Execute the command making sure the connection gets closed in the end
            try
            {
                // Open the connection of the command
                command.Connection.Open();
                // Execute the command and get the number of affected rows
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log eventual errors and rethrow them                
                throw ex;
            }
            finally
            {
                // Close the connection
                command.Connection.Close();
            }
            // return the number of affected rows
            return affectedRows;
        }

        // execute a select command and return a single result as a string
        public object ExecuteScalar(SqlCommand command)
        {
            // The value to be returned
            object value = "";
            // Execute the command making sure the connection gets closed in the end
            try
            {
                // Open the connection of the command
                command.Connection.Open();
                // Execute the command and get the number of affected rows
                value = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                value = ex;
            }
            finally
            {
                // Close the connection
                command.Connection.Close();
            }
            // return the result
            return value;
        }


        // creates and prepares a new SqlCommand object on a new connection
        public SqlCommand CreateCommand()
        {

            // Obtain the database provider name
            // Obtain the database connection string
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString.ToString();
            // Create a new data provider factory
            // Obtain a database specific connection object
            // Obtain a database specific connection object
            SqlConnection conn = new SqlConnection();
            // Set the connection string
            conn.ConnectionString = connectionString;
            // Create a database specific command object
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            // Set the command type to stored procedure
            comm.CommandType = CommandType.Text;
            // Return the initialized command object
            return comm;
        }
        public SqlCommand CreateCommand(string connectiontype)
        {

            if (connectiontype.isNull())
            {
                connectiontype = "SqlConnectionString";
            }
             


            string connectionString = ConfigurationManager.ConnectionStrings[connectiontype].ConnectionString.ToString();
            // Create a new data provider factory
            // Obtain a database specific connection object
            // Obtain a database specific connection object
            SqlConnection conn = new SqlConnection();
            // Set the connection string
            conn.ConnectionString = connectionString;
            // Create a database specific command object
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            // Set the command type to stored procedure
            comm.CommandType = CommandType.Text;
            // Return the initialized command object
            return comm;
        }

        public DataTable GetRecords(string CommandText)
        {
            SqlCommand comm = new GenericDataAccess().CreateCommand();
            comm.CommandText = CommandText;
            DataTable table = new GenericDataAccess().ExecuteSelectCommand(comm);
            return table;
        }

        public object CreateParameter(string paremetername, object parametervalue)
        {
            return new SqlParameter(paremetername, parametervalue);
        }

    }
}
