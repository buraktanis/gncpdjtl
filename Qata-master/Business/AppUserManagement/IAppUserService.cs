using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Results;
using Entities.Concrete;
using static Core.DataTables.DatatablesJS;

namespace Business.AppUserManagement
{
    public interface IAppUserService
    {
        IResult Add(AppUser appUser);
        IResult Update(AppUser appUser);
        IResult Delete(AppUser appUser);
        IDataResult<AppUser> GetAppUserById(int id);
        IDataResult<List<AppUser>> GetAll();
        IDataResult<AppUser> GetAppUserByEmail(string email);
        DataTablesObjectResult GetUserDatatables(DatatablesObject requestobj);

    }
}
