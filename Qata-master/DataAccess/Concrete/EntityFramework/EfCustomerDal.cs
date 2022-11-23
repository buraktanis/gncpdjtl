using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Repositories.EntityFramework;
using Core.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, ApplicationDbContext>, ICustomerDal
    {

        //public IDataResult<List<Customer>> RawSqlQuery(string sqlString)
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        var list = context.Customers.FromSqlRaw(sqlString).ToList();
        //        return new SuccessDataResult<List<Customer>>(list);
        //    }
        //}
        //public IDataResult<bool> Ra(List<Customer> logocustomers)
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        //  var list = context.Customers.FromSqlRaw(sqlString).ToList();
        //        var crmcustomers = context.Customers.Where(x => x.LogoId != 0).ToList();
        //        foreach (var item in crmcustomers)
        //        {
        //          var customers= logocustomers.FirstOrDefault(x => x.LogoId == item.LogoId);
        //            if (customers != null)
        //            {
        //                item.Slsman = customers.Slsman;
        //                item.CustomerClass = customers.CustomerClass;
        //            }
                   
        //        }
        //        context.SaveChanges();
        //    }
        //    return new SuccessDataResult<bool>(true);
        //}

    }
}
