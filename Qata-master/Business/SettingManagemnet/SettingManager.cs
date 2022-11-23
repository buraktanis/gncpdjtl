using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static Core.DataTables.DatatablesJS;

namespace Business.SettingManager
{
    public class SettingManager : ISettingManager
    {
        private ISettingDal _SettingDal;

        public SettingManager(ISettingDal  SettingDal)
        {
            _SettingDal = SettingDal;
        }
        public IDataResult<Setting> Add(Setting entity)
        {
            return _SettingDal.Add(entity);
        }

        public IResult Delete(Setting entity)
        {
            return _SettingDal.Delete(entity);
        }

        public IDataResult<Setting> Get(Expression<Func<Setting, bool>> filter = null)
        {
            return _SettingDal.Get(filter);
        }

        public IDataResult<IList<Setting>> GetList(Expression<Func<Setting, bool>> filter = null)
        {
            return _SettingDal.GetList(filter);
        }

        public IResult Update(Setting entity)
        {
            return _SettingDal.Update(entity);
        }

        public DataTablesObjectResult GetDatatables(DatatablesObject requestobj)
        {
            string query = @" SELECT * FROM Settings ";

            string privadewhere = "";
         
            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }
    }
}
