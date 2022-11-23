using System;
using System.Collections.Generic;
using System.Text;
using Core.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.AccountManagement
{
    public interface IAuthService
    {
        
        IDataResult<AppUser> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        
    }
}
