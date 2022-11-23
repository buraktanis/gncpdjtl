using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Repositories;
using Core.Results;
using Dapper;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.Dapper
{
    class DapperBigDataRepository : DapperRepositoryBase, IBigDataDal
    {


        public DapperBigDataRepository(IConfiguration configuration) : base(configuration)
        {

        }


        public IDataResult<BigData> Add(BigData employee)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                OpenConnection(sqlConnection);
                sqlConnection.Query<BigData>(@"INSERT INTO Employees ([Name],[LastName],[Gender],[AddressId],[DepartmentCode],[DepartmentName],[ReportsTo],[Created]) 
                                             VALUES (@Name,@LastName,@Gender,@AddressId,@DepartmentCode,@DepartmentName,@ReportsTo,@Created)",
                                             employee);
                CloseConnection(sqlConnection);
                return new SuccessDataResult<BigData>(employee);
            }
        }

        public IResult AddRange(IList<BigData> entityList)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(BigData entity)
        {
            throw new NotImplementedException();
        }

        public BigData Get(Func<BigData, bool> predicate)
        {
            List<BigData> employees;
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                OpenConnection(sqlConnection);
                employees = sqlConnection.Query<BigData>("SELECT * FROM BigDatas").ToList();
                CloseConnection(sqlConnection);
            }

            return employees.FirstOrDefault(predicate);
        }

        public IDataResult<BigData> Get(Expression<Func<BigData, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public IList<BigData> GetAll()
        {
            List<BigData> employees;

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                OpenConnection(sqlConnection);
                employees = sqlConnection.Query<BigData>("SELECT * FROM Employees").ToList();
                CloseConnection(sqlConnection);
            }

            return employees;
        }

        public IDataResult<IList<BigData>> GetList(Expression<Func<BigData, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public int Remove(BigData employee)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                OpenConnection(sqlConnection);
                sqlConnection.Query<BigData>(@"DELETE FROM Employees WHERE Id = @Id", employee);
                CloseConnection(sqlConnection);
                return 1;
            }
        }





        public int Update(BigData employee)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                OpenConnection(sqlConnection);
                sqlConnection.Query<BigData>(@"UPDATE Employees SET [Name]=@Name, [LastName]=@LastName, [AddressId]=@AddressId, 
                                             [DepartmentCode]=@DepartmentCode, [DepartmentName]=@DepartmentName, [ReportsTo]=@ReportsTo 
                                             WHERE Id = @Id", employee);
                CloseConnection(sqlConnection);
                return 1;
            }
        }

        public IResult UpdateAsync(BigData entity)
        {
            throw new NotImplementedException();
        }

        IDataResult<BigData> IEntityRepository<BigData>.Add(BigData entity)
        {
            throw new NotImplementedException();
        }

        IResult IEntityRepository<BigData>.Update(BigData entity)
        {
            throw new NotImplementedException();
        }
    }
 
}
//https://github.com/ErenYilmaz97/Bootcamp-Solid-Practice/tree/master/SolidPractice.DataAccess