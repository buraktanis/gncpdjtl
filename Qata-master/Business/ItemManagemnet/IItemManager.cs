using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static Core.DataTables.DatatablesJS;

namespace Business.ItemManagemnet
{
    public interface ISettinManager
    {
        IDataResult<Item> Add(Item entity);
        IResult Update(Item entity);
        IResult Delete(Item entity);
        IDataResult<Item> Get(Expression<Func<Item, bool>> filter = null);
        IDataResult<IList<Item>> GetList(Expression<Func<Item, bool>> filter = null);

        DataTablesObjectResult GetDatatables(DatatablesObject requestobj);
    }
}
