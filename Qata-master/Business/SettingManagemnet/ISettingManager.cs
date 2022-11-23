using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static Core.DataTables.DatatablesJS;

namespace Business.SettingManager
{
    public interface ISettingManager
    {
        IDataResult<Setting> Add(Setting entity);
        IResult Update(Setting entity);
        IResult Delete(Setting entity);
        IDataResult<Setting> Get(Expression<Func<Setting, bool>> filter = null);
        IDataResult<IList<Setting>> GetList(Expression<Func<Setting, bool>> filter = null);

        DataTablesObjectResult GetDatatables(DatatablesObject requestobj);
    }
}
