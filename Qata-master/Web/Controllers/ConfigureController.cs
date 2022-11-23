using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.ConfigurationManagement;
using Entities.Concrete;
using Core.DataTables;
using Core.Results;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]
    public class ConfigureController : Controller
    {
        private IConfigureService _configureService;

        public ConfigureController(IConfigureService configureService)
        {
            _configureService = configureService;
        }

        public IActionResult Index(int id)
        {
            var result = _configureService.GetConfigurationTypeAll().Data.FirstOrDefault(x => x.Id == id);
        
          return View(result);

        }

        [HttpPost]
        public IActionResult GetConfigurationDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_configureService.GetConfigurationDatatables(requestobj));
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }



        [HttpGet]
        public IActionResult NewConfiguration(int id)
        {
            var result = _configureService.GetConfigurationTypeAll().Data.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = result.Name;
            ViewBag.tid = result.Id;
            return View();
        }


   

        [HttpPost]
        public IActionResult NewConfiguration(Configuration order)
        {
            order.Status = 1;
            order.Id = 0;
            var result = _configureService.Add(order);
            if (result.Success)
            {
                return RedirectToAction("Index",new { id = order.Type });
            }

            return View(order);
        }

        [HttpGet]
        public IActionResult UpdateConfiguration(int id)
        {
            var result = _configureService.GetConfigurationById(id);
            if (result.Success)
            {
                return View(result.Data);
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpdateConfiguration(Configuration config)
        {
            var result = _configureService.Update(config);
            if (result.Success)
            {
                return RedirectToAction("Index", new { id = config.Type });
            }

            return View(config);
        }

        public IResult DeleteConfiguration(int id)
        {
            var config = _configureService.GetConfigurationById(id).Data;
            var result = _configureService.Delete(config);
            return result;
        }
    }
}

