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
    public class EfAppUserDal:EfEntityRepositoryBase<AppUser,ApplicationDbContext>,IAppUserDal
    {
        public IDataResult<List<AppUser>> RawSqlQuery(string sqlString)
        {
            using (var context = new ApplicationDbContext())
            {
                var list = context.AppUsers.FromSqlRaw(sqlString).ToList();
                return new SuccessDataResult<List<AppUser>>(list);
            } 
        }
    }
}
