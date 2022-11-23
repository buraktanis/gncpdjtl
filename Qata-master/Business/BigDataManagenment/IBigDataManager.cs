using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static Core.DataTables.DatatablesJS;

namespace Business.BigDataManagenment
{
 
    public interface IBigDataManager
    {
        DataTablesObjectResult GetBigData(DatatablesObject requestobj);
        IDataResult<BigData> Add(BigData entity);
        IResult Update(BigData entity);
        IResult Delete(BigData entity);
        IDataResult<BigData> Get(Expression<Func<BigData, bool>> filter = null);
        IDataResult<IList<BigData>> GetList(Expression<Func<BigData, bool>> filter = null);
 
        IResult AddRange(IList<BigData> entity);
    }
}
