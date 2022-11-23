using Business.NotManagement;
using Core.DataTables;
using Core.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class NotController : Controller
    {
        private INotManager _notManager;
        public NotController(INotManager notManager)
        {
            _notManager = notManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IResult Add([FromBody]Not dto)
        {
            var result = _notManager.Add(dto);

            return result;
        } 
        

        public IResult Delete(int id)
        {
            var not = _notManager.Get(x=>x.Id==id);
            var result = _notManager.Delete(not.Data);

            return result;
        }


        [HttpPost]
        public IActionResult GetDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_notManager.GetDatatable(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }
        
        [HttpPost]
        public IActionResult GetBigdataDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_notManager.GetBigdataDatatables(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }
    }
}
