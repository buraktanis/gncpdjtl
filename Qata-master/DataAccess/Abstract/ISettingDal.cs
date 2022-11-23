using System;
using System.Collections.Generic;
using System.Text;
using Core.Repositories;
using Core.Results;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ISettingDal : IEntityRepository<Setting>
    {
        IDataResult<List<AppUser>> RawSqlQuery(string sqlString);
    }
}
