using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static Core.DataTables.DatatablesJS;

namespace Business.ItemManagemnet
{
    public class SettinManager : ISettinManager
    {
        private IItemDal _itemDal;

        public SettinManager(IItemDal  itemDal)
        {
            _itemDal = itemDal;
        }
        public IDataResult<Item> Add(Item entity)
        {
            return _itemDal.Add(entity);
        }

        public IResult Delete(Item entity)
        {
            return _itemDal.Delete(entity);
        }

        public IDataResult<Item> Get(Expression<Func<Item, bool>> filter = null)
        {
            return _itemDal.Get(filter);
        }

        public IDataResult<IList<Item>> GetList(Expression<Func<Item, bool>> filter = null)
        {
            return _itemDal.GetList(filter);
        }

        public IResult Update(Item entity)
        {
            return _itemDal.Update(entity);
        }

        public DataTablesObjectResult GetDatatables(DatatablesObject requestobj)
        {
            string query = @" SELECT * FROM Items ";

            string privadewhere = "";
         
            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }
    }
}
