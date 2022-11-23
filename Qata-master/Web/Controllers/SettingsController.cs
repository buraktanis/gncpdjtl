using Business.SettingManager;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class SettingsController : Controller
    {
        private ISettingManager _settingManager;

        public SettingsController(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var result = _settingManager.Get(x => x.Id == 1);
            if (result.Success)
            {
                return View(result.Data);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Update(Setting config)
        {
            var result = _settingManager.Update(config);

            return View(config);
        }
    }
}
