using System;
using System.Collections.Generic;
using System.Text;
using Core.Repositories.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCityDal:EfEntityRepositoryBase<City,ApplicationDbContext>,ICityDal
    {
    }
}
