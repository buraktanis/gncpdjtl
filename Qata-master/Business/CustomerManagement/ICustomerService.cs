using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using Core.Results;
using Entities.Concrete;
using Entities.DTOs;
using static Core.DataTables.DatatablesJS;

namespace Business.CustomerManagement
{
   public interface ICustomerService
   {
       IDataResult<Customer> Add(Customer customer);
       IResult Update(Customer customer);
       IResult ConfirmCustomer(int customerId, int logoId);
       IResult Delete(Customer customer);
       IDataResult<Customer> GetCustomerById(int id);
       IDataResult<List<Customer>> GetAllCustomers();
       IDataResult<List<Customer>> GetUnApprovedCustomers();
       DataTablesObjectResult GetCustomerDatatables(DatatablesObject requestobj);
       DataTablesObjectResult GetConsignmentDatatables(DatatablesObject requestobj);
       DataTablesObjectResult GetCustomerConfirmationDatatables(DatatablesObject requestobj);
        public IDataResult<List<AddressDTOs>> GetAddressCLIENTREF(int id);
        public IDataResult<List<Customer>> AddRange(List<Customer> customer);
        IDataResult<Customer> GetCustomer(Expression<Func<Customer, bool>> filter = null);
        public DataTablesObjectResult GetCallReportDatatables(DatatablesObject requestobj);
   }
}
