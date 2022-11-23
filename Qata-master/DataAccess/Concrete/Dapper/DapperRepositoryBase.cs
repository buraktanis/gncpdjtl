using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

namespace DataAccess.Concrete.Dapper
{
    public abstract class DapperRepositoryBase
    {
        protected string _connectionString;


        public DapperRepositoryBase(IConfiguration configuration)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString.ToString();
        }



        protected void OpenConnection(IDbConnection dbConnection)
        {
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }



        protected void CloseConnection(IDbConnection dbConnection)
        {
            if (dbConnection.State == ConnectionState.Open)
            {
                dbConnection.Close();
            }
        }
    }
}
