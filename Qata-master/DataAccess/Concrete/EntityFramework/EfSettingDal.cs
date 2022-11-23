using Core.Repositories.EntityFramework;
using Core.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSettingDal : EfEntityRepositoryBase<Setting, ApplicationDbContext>, ISettingDal
    {
        public IDataResult<List<AppUser>> RawSqlQuery(string sqlString)
        {
            throw new NotImplementedException();
        }
    }
}
