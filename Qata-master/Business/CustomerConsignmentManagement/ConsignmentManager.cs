using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.CustomerConsignmentManagement
{
    public class ConsignmentManager:IConsignmentService
    {
        private IConsignmentDal _consignmentDal;

        public ConsignmentManager(IConsignmentDal consignmentDal)
        {
            _consignmentDal = consignmentDal;
        }
        public IDataResult<CustomerConsignment> GetById(int id)
        {
            return _consignmentDal.Get(c => c.Id == id);
        }  
        public IDataResult<IList<CustomerConsignment>> GetByCustomerId(int id)
        {
            return _consignmentDal.GetList(c => c.CustomerId == id);
        }
        public IResult AddRange(List<CustomerConsignment> list)
        {
            return _consignmentDal.AddRange(list);
        }  
        public IResult Add(CustomerConsignment dto)
        {
            return _consignmentDal.Add(dto);
        }
        
        public IResult Delete(CustomerConsignment dto)
        {
            return _consignmentDal.Delete(dto);
        }

        public IDataResult<IList<CustomerConsignment>> GetList(Expression<Func<CustomerConsignment, bool>> filter = null)
        {
            return _consignmentDal.GetList(filter);
        }
    }
}
