using System;
using System.Collections.Generic;
using System.Text;
using Business.AppUserManagement;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.AccountManagement
{
    public class AuthManager:IAuthService
    {
        private IAppUserDal _appUserDal;
        private IAppUserService _appUserService;

        public AuthManager(IAppUserDal appUserDal, IAppUserService appUserService)
        {
            _appUserDal = appUserDal;
            _appUserService = appUserService;
        }

        public IDataResult<AppUser> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _appUserService.GetAppUserByEmail(userForLoginDto.Email);
            if (userToCheck.Data==null)
            {
                return new ErrorDataResult<AppUser>("User not found!");
            }

            if (userToCheck.Data.Email==userForLoginDto.Email && userToCheck.Data.Password==userForLoginDto.Password)
            {
                return new SuccessDataResult<AppUser>(userToCheck.Data);
            }

            return new ErrorDataResult<AppUser>("User not found!");
        }

        public IResult UserExists(string email)
        {
            if (_appUserService.GetAppUserByEmail(email).Data==null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
