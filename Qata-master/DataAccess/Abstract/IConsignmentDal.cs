using System;
using System.Collections.Generic;
using System.Text;
using Core.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IConsignmentDal:IEntityRepository<CustomerConsignment>
    {
    }
}
