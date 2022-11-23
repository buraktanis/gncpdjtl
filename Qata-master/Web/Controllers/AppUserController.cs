using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Business.AppUserManagement;
using Core.Results;
using Entities.Concrete;
using Core.DataTables;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize(Roles = "Yonetim")]
    public class AppUserController : Controller
    {
        private IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AppUser appUser)
        {



            var result = _appUserService.Add(appUser);
            if (result.Success)
            {
                return View("GetAllAppUsers");
            }

            return View(appUser);
        }

        //[Route("{controller}/GetUserDatatables")]
        [HttpPost]
        public IActionResult GetUserDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_appUserService.GetUserDatatables(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }




        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(AppUser appUser)
        {
            var result = _appUserService.Update(appUser);
            if (result.Success)
            {
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var result = _appUserService.GetAppUserById(id);
            return View(result.Data);
        }

        
  
        public IResult Delete(int id)
        {
            AppUser user = _appUserService.GetAppUserById(id).Data;
            var result = _appUserService.Delete(user);
            return new SuccessResult();
        }
        [HttpGet]
        public IActionResult GetAllAppUsers()
        {
           

            return View();
        }

        
    }
}
