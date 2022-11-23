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

namespace Business.BigDataManagenment
{
    public class BigDataManager : IBigDataManager
    {

        private IBigDataDal _bigDataDal;

        public BigDataManager(IBigDataDal bigDataDal)
        {
            _bigDataDal = bigDataDal;
        }
        public IDataResult<BigData> Add(BigData entity)
        {
            return _bigDataDal.Add(entity);
        }
            public IResult AddRange(IList<BigData> entity)
        {
            return _bigDataDal.AddRange(entity);
        }

        public IResult Delete(BigData entity)
        {
            return _bigDataDal.Delete(entity);
        }

        public IDataResult<BigData> Get(Expression<Func<BigData, bool>> filter = null)
        {
            return _bigDataDal.Get(filter);
        }

        public IDataResult<IList<BigData>> GetList(Expression<Func<BigData, bool>> filter = null)
        {
            return _bigDataDal.GetList(filter);
        }

        public IResult Update(BigData entity)
        {
            return _bigDataDal.Update(entity);
        }

        public DataTablesObjectResult GetBigData(DatatablesObject requestobj)
        {
            string query = string.Format(@" SELECT * FROM BigDatasAll ");




            string privadewhere = "";
            var tip = requestobj.additionalvalues.ElementAt(0);

            if (!tip.Trim().isNull())
            {
                privadewhere = string.Format(" Slsman='{0}' ", tip);
            }
            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }
    }
}
