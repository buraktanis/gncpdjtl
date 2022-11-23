using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business;
using Core.DataTables;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using static Core.DataTables.DatatablesJS;

namespace Business.AppUserManagement
{
    public class AppUserManager:IAppUserService
    {
        private  IAppUserDal _appUserDal;

        public AppUserManager(IAppUserDal appUserDal)
        {
            _appUserDal = appUserDal;
        }

        public IResult Add(AppUser appUser)
        {
            appUser.CreatedDate=DateTime.Now;
            _appUserDal.Add(appUser);
            return new SuccessResult();
        }

        public IResult Update(AppUser appUser)
        {
            _appUserDal.Update(appUser);
            return new SuccessResult();
        }

        public IResult Delete(AppUser appUser)
        {
            _appUserDal.Delete(appUser);
            return new SuccessResult();
        }

        public IDataResult<AppUser> GetAppUserById(int id)
        {
            return new SuccessDataResult<AppUser>(_appUserDal.Get(a => a.Id == id).Data);
        }

        public IDataResult<List<AppUser>> GetAll()
        {
            return new SuccessDataResult<List<AppUser>>(_appUserDal.GetList().Data.OrderBy(x=>x.Name).ToList());
        }

        public IDataResult<AppUser> GetAppUserByEmail(string email)
        {
             
            return _appUserDal.Get(a => a.Email == email);
        }

        public DataTablesObjectResult GetUserDatatables(DatatablesObject requestobj)
        {
            string query = " SELECT * FROM AppUsers ";

            string privadewhere = "";

            return new DatatablesJS.DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }

    }
}
