using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.AccountManagement;
using Business.AppUserManagement;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Core.Results;
using Microsoft.AspNetCore.Http;
using Core.Helpers;
using Core.Extensions;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IAppUserService _appUserService;
        public AuthController(IAuthService authService, IAppUserService appUserService)
        {
            _appUserService = appUserService;
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
      
            var id = CurrentSession.GetCookies("cookiepersistent");
            if (!id.isNull())
            {
                var model = _appUserService.GetAppUserById(id.toint32());

                if (OtoLogin(model.Data))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            if (ModelState.IsValid)
            {
                var userToLogin = _authService.Login(userForLoginDto);
                if (!userToLogin.Success)
                {
                    ModelState.AddModelError("Error", "Kullanıcı Bulunmadı");
                    return View(userForLoginDto);
                }


                AppUser appUser = userToLogin.Data;


                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, appUser.Name ));
                identity.AddClaim(new Claim(ClaimTypes.Email, appUser.Email));
                identity.AddClaim(new Claim(ClaimTypes.Role, appUser.Department));
                var principal = new ClaimsPrincipal(identity);

                if (userForLoginDto.IsPersistent)
                {
                    SetCookies("cookiepersistent", appUser.Id.ToString());
                }
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");

            }
            return View(userForLoginDto);
        }

        public bool OtoLogin(AppUser appUser)
        {


            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, appUser.Name));
            identity.AddClaim(new Claim(ClaimTypes.Email, appUser.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, appUser.Department));
            var principal = new ClaimsPrincipal(identity);


            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

           
            return true;

        }
        public IResult SetCookies(string key, string val)
        {
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append(key, val, cookie);


            return new SuccessResult();
        }

        public IResult RemoveCookies(string key)
        {

            Response.Cookies.Delete(key, new CookieOptions()
            {
                Secure = true,
            });
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(-1);
            option.Secure = true;
            option.IsEssential = true;
            Response.Cookies.Append(key, string.Empty, option);
            //Then delete the cookie
            Response.Cookies.Delete(key);

            return new SuccessResult();
        }
        [HttpPost]
        [HttpGet]
        public IActionResult Logout()
        {
            RemoveCookies("cookiepersistent");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }
    }
}
