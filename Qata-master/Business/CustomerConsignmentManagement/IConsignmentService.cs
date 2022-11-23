using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.Results;
using Entities.Concrete;

namespace Business.CustomerConsignmentManagement
{
    public interface IConsignmentService
    {
        public IResult Add(CustomerConsignment dto);
        IResult AddRange(List<CustomerConsignment> list);
        IDataResult<CustomerConsignment> GetById(int id);
        public IResult Delete(CustomerConsignment dto);
        public IDataResult<IList<CustomerConsignment>> GetByCustomerId(int id);

        IDataResult<IList<CustomerConsignment>> GetList(Expression<Func<CustomerConsignment, bool>> filter = null);
    }
}
