using Business.ItemManagemnet;
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
    public class ItemController : Controller
    {
        private ISettinManager _ItemManager;
        public ItemController(ISettinManager itemManager)
        {
            _ItemManager = itemManager;
        }
        public IActionResult Index()
        {
            return View();
        }  
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Item item)
        {
            var model = _ItemManager.Add(item);
           
            return View("Index");
        }   
     
        public IActionResult Update(int id)
        {
            var model = _ItemManager.Get(x => x.Id==id);

            return View(model.Data);
        } 
        
        [HttpPost]
        public IActionResult Update(Item item)
        {
            var model = _ItemManager.Update(item);
           
            return View(item);
        }
        public IResult Delete(int id)
        {
            var orderItem = _ItemManager.Get(x=>x.Id==id).Data;
            var result = _ItemManager.Delete(orderItem);

            return new SuccessResult();
        }
        [HttpPost]
        public IActionResult GetDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_ItemManager.GetDatatables(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }
    }
}
