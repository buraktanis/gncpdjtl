using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static Core.DataTables.DatatablesJS;

namespace Business.NotManagement
{
   public interface INotManager
    {

        DataTablesObjectResult GetDatatable(DatatablesObject requestobj);
        DataTablesObjectResult GetBigdataDatatables(DatatablesObject requestobj);
        IDataResult<Not> Add(Not entity);
        IResult Update(Not entity);
        IResult Delete(Not entity);
        IDataResult<Not> Get(Expression<Func<Not, bool>> filter = null);
        IDataResult<IList<Not>> GetList(Expression<Func<Not, bool>> filter = null);

        IResult AddRange(IList<Not> entity);
    }
}
