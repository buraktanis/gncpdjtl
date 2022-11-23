using Core.DataTables;
using Core.Extensions;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static Core.DataTables.DatatablesJS;

namespace Business.NotManagement
{

    public class NotManager : INotManager
    {
        private INotDal _notDal;
        public NotManager(INotDal notDal)
        {
            _notDal = notDal;
        }
        public IDataResult<Not> Add(Not entity)
        {
            entity.createDate = DateTime.Now;
            return _notDal.Add(entity);
        }

        public IResult AddRange(IList<Not> entity)
        {
            return _notDal.AddRange(entity);
        }

        public IResult Delete(Not entity)
        {
            return _notDal.Delete(entity);
        }

        public IDataResult<Not> Get(Expression<Func<Not, bool>> filter = null)
        {
            return _notDal.Get(filter);
        }

        public DatatablesJS.DataTablesObjectResult GetDatatable(DatatablesJS.DatatablesObject requestobj)
        {
            string query = @" SELECT * FROM Nots  ";

            string privadewhere = "";
            var CariId = requestobj.additionalvalues.ElementAt(0);
            var temsilci = requestobj.additionalvalues.ElementAt(1);

            if (!CariId.isNull())
            {
                privadewhere = string.Format(" t.CariId={0} ", CariId);
            }

            if (!temsilci.isNull())
            {
                privadewhere = privadewhere  +string.Format(" AND t.Username='{0}' ", temsilci);
            }

            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }
        public DatatablesJS.DataTablesObjectResult GetBigdataDatatables(DatatablesJS.DatatablesObject requestobj)
        {
            string query = @" SELECT * FROM Nots  ";

            string privadewhere = "";
            var CariId = requestobj.additionalvalues.ElementAt(0);
            var temsilci = requestobj.additionalvalues.ElementAt(1);
            if (!CariId.isNull())
            {
                privadewhere = string.Format(" t.BigdataId={0} ", CariId);
            }

            if (!temsilci.isNull())
            {
                privadewhere = privadewhere + string.Format(" AND t.Username='{0}' ", temsilci);
            }
            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }

        public IDataResult<IList<Not>> GetList(Expression<Func<Not, bool>> filter = null)
        {
            return _notDal.GetList(filter);
        }

        public IResult Update(Not entity)
        {
            return _notDal.Update(entity);
        }
    }
}
