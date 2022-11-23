using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.XPath;
using Core.DataTables;
using Core.Extensions;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using static Core.DataTables.DatatablesJS;

namespace Business.CustomerManagement
{
   public class CustomerManager:ICustomerService
   {
       private ICustomerDal _customerDal;

       public CustomerManager(ICustomerDal customerDal)
       {
           _customerDal = customerDal;
       }
        public IDataResult<Customer> Add(Customer customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ConfirmationType = 0;
            var result =_customerDal.Add(customer);
            return result;
        } 
        
        public IDataResult<List<Customer>> AddRange(List<Customer> customer)
        {
            
            var result =_customerDal.AddRange(customer);
            return new SuccessDataResult<List<Customer>>();
        }

        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult();
        }

        public IResult ConfirmCustomer(int customerId,int logoId)
        {
            var customer = _customerDal.Get(c => c.Id == customerId).Data;

            if (customer == null)
            {
                return new ErrorResult("Cari Bullnamadı");
            }
            customer.ConfirmationType = 0;
            customer.LogoId = logoId;
            return _customerDal.Update(customer);
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult();
        }

        public IDataResult<Customer> GetCustomerById(int id)
        {
            return _customerDal.Get(c => c.Id == id);
        }
          public IDataResult<Customer> GetCustomer(Expression<Func<Customer, bool>> filter = null)
         {
            return _customerDal.Get(filter);
         }

        public IDataResult<List<Customer>> GetAllCustomers()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetList().Data.ToList());
        }

        public IDataResult<List<Customer>> GetUnApprovedCustomers()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetList(c => c.ConfirmationType == 0).Data
                .ToList());
        } 
        public IDataResult<List<AddressDTOs>> GetAddressCLIENTREF(int id)
        {
            var model = string.Format(@"	 SELECT * FROm CustomerConsignments WHERE CLIENTREF={0} ", id).GetQuery<AddressDTOs>();
            return new SuccessDataResult<List<AddressDTOs>>(model);
        }

        public DataTablesObjectResult GetCustomerDatatables(DatatablesObject requestobj)
        {
            string query = " SELECT * FROM CustomerAll ";

            string privadewhere = "";
            var tip = requestobj.additionalvalues.ElementAt(0);

            var confirmationType = requestobj.additionalvalues.ElementAt(1);
            var customerClass = requestobj.additionalvalues.ElementAt(2);
            var region = requestobj.additionalvalues.ElementAt(3);
            var city = requestobj.additionalvalues.ElementAt(4);
            var town = requestobj.additionalvalues.ElementAt(5);
            var arama = requestobj.additionalvalues.ElementAt(6);

            if (!tip.Trim().isNull())
            {
                privadewhere = string.Format(" Slsman='{0}' ", tip);
            }


           if (!confirmationType.isNull())
            { 
                if (!privadewhere.isNull())
                {
                    privadewhere = privadewhere + " AND ";
                }

                privadewhere = privadewhere+ string.Format("  ConfirmationType={0} ", confirmationType);
            } 
            
            if (!customerClass.isNull())
            { 
                if (!privadewhere.isNull())
                {
                    privadewhere = privadewhere + " AND ";
                }

                privadewhere = privadewhere+ string.Format("  CustomerClass='{0}' ", customerClass);
            }
            
            if (!region.isNull())
            { 
                if (!privadewhere.isNull())
                {
                    privadewhere = privadewhere + " AND ";
                }

                privadewhere = privadewhere+ string.Format("  Region='{0}' ", region);
            }  
            if (!city.isNull())
            { 
                if (!privadewhere.isNull())
                {
                    privadewhere = privadewhere + " AND ";
                }

                privadewhere = privadewhere+ string.Format("  City='{0}' ", city);
            } 
            if (!town.isNull())
            { 
                if (!privadewhere.isNull())
                {
                    privadewhere = privadewhere + " AND ";
                }

                privadewhere = privadewhere+ string.Format("  Town='{0}' ", town);
            }   
            if (!arama.isNull())
            { 
                if (!privadewhere.isNull())
                {
                    privadewhere = privadewhere + " AND ";
                }

                privadewhere = privadewhere+ string.Format("  (Name LIKE '%{0}%' OR Phone LIKE '%{0}%' OR CariKodu  LIKE '%{0}%')", arama);
            }
        






            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        } 
        
        public DataTablesObjectResult GetConsignmentDatatables(DatatablesObject requestobj)
        {
            string query = @" SELECT * FROM CustomerConsignments  ";
            string privadewhere = "";
            var tip = requestobj.additionalvalues.ElementAt(0);

            if (!tip.isNull())
            {
                privadewhere = string.Format(" CLIENTREF={0} ", tip);
            }

            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        } 
        public DataTablesObjectResult GetCustomerConfirmationDatatables(DatatablesObject requestobj)
        {

            string query = " SELECT * FROM Customers ";

            string privadewhere = "";
            var tip = requestobj.additionalvalues.ElementAt(0);

            if (!tip.isNull())
            {
                privadewhere = string.Format(" ConfirmationType={0} ", tip.Trim());
            }

            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        } 
        
        public DataTablesObjectResult GetCallReportDatatables(DatatablesObject requestobj)
        {

            string query = " SELECT * FROM CallReport ";

            string privadewhere = "";
            var tip = requestobj.additionalvalues.ElementAt(0);

            if (!tip.isNull())
            {
                privadewhere = string.Format(" phone='{0}' ", tip.Trim());
            }

            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }
    }
}
