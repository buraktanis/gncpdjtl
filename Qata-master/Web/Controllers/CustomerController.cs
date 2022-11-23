using Business.AppUserManagement;
using Business.BigDataManagenment;
using Business.ConfigurationManagement;
using Business.CustomerConsignmentManagement;
using Business.CustomerManagement;
using Business.LogoManagement;
using Core.DataTables;
using Core.Extensions;
using Core.Helpers;
using Core.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private IBigDataManager _bigDataManager;
        private ICustomerService _customerService;
        private IConfigureService _configureService;
        private IAppUserService _appUserService;
        private IConsignmentService _consignmentService;
        private ILogoManagement _logoManagement;
        private ILogoService _logoService;
        public CustomerController(ICustomerService customerService,
            IConfigureService configureService,
            IAppUserService appUserService,
            IConsignmentService consignmentService,
            ILogoManagement logoManagement,
            ILogoService logoService,
            IBigDataManager bigDataManager)
        {
            _bigDataManager = bigDataManager;
            _logoService = logoService;
            _logoManagement = logoManagement;
            _customerService = customerService;
            _configureService = configureService;
            _appUserService = appUserService;
            _consignmentService = consignmentService;
        }

        public IActionResult Test()
        {


            return View();
        }


        public IActionResult GetAllCustomers()
        {
            //var result = _customerService.GetAllCustomers();
            return View(GetCustomerViewModel().Data);
        }

        public IActionResult Index()
        {
            //var m = _logoManagement.Carilist();
            ////foreach (var item in m.Data)
            ////{
            ////    item.TaxAdministration = "";
            ////}
            //var cu = _customerService.AddRange(m.Data);
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult AddCustomer()
        {
            return View(GetCustomerViewModel().Data);
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult AddCustomer(CustomerViewModel viewModel)
        {
            var Customer = GetCustomerViewModel().Data;
            Customer.Customer = viewModel.Customer;

            if (viewModel.Customer.Type == 0)
            {
             
                if (Customer.Customer.TaxAdministration.isNull())
                {
                    ModelState.AddModelError("Customer.TaxAdministration", "Zorunlu Alan");

                    return View(Customer);
                }
                var tax = _logoManagement.TaxTcControl(viewModel.Customer.TaxAdministration);
                if (tax.Data.Count > 0)
                {
                    ModelState.AddModelError("Customer.TaxAdministration", string.Format("Bu kayıt logoda “{0}” adına kayıtlıdır", tax.Data[0].Name));
                    return View(Customer);

                }



            }

            if (viewModel.Customer.Type == 1)
            {
                if (Customer.Customer.TcIdentityNumber.isNull())
                {
                    ModelState.AddModelError("Customer.TcIdentityNumber", "Zorunlu Alan");

                    return View(Customer);
                }
                var tax = _logoManagement.TaxTcControl(viewModel.Customer.TcIdentityNumber);
                if (tax.Data.Count > 0)
                {
                    ModelState.AddModelError("Customer.TcIdentityNumber", string.Format("Bu kayıt logoda “{0}” adına kayıtlıdır", tax.Data[0].Name));
                    return View(Customer);

                }

            }


            var result = _customerService.Add(Customer.Customer);

            var alertVM = new AlertVM();

            alertVM.title = "İşlem Başarılı";
            alertVM.redirectingUrl = "/Customer/UpdateCustomer/" + result.Data.Id;
            return View("_Alert", alertVM);

        }

        public IResult TcAndVergiKontrol(string id, int type)
        {
            if (id.isNull())
            {
                return new ErrorResult("Lütfen numarayı giriniz");
            }

            var tax = _logoManagement.TaxTcControl(id);
            if (tax.Data.Count > 0)
            {
                return new ErrorResult(string.Format("Bu kayıt logoda “{0}” adına kayıtlıdır. Temsilcisi {1}", tax.Data[0].Name, tax.Data[0].Slsman));
            }
            return new SuccessResult();

        }   
        public IResult TelControl(string id)
        {
            if (id.isNull())
            {
                return new ErrorResult("Lütfen numarayı giriniz");
            }

            var tax = _logoManagement.TelControl(id);
            if (tax.Data.Count > 0)
            {
                return new ErrorResult(string.Format("Bu kayıt logoda “{0}” adına kayıtlıdır.Temsilcisi {1}", tax.Data[0].Name, tax.Data[0].Slsman));
            }
            return new SuccessResult();

        }


        public IDataResult<CustomerViewModel> GetCustomerViewModel()
        {
            var configureResult = _configureService.GetAll();
            var cities = _configureService.GetAllCities();
            var users = _appUserService.GetAll();
            var model = new CustomerViewModel
            {
                CustomerTypes = new List<SelectListItem> { new SelectListItem { Text = "Firma", Value = "0" },
                    new SelectListItem { Text = "Sahsi Firma", Value = "1" },
                    new SelectListItem { Text = "Nihai Tüketici", Value = "2" }
                },
                KindOfCustomers = (from i in configureResult.Data.Where(x => x.Type == 2).ToList()
                                   select new SelectListItem
                                   {
                                       Text = i.Name,
                                       Value = i.Name
                                   }).ToList(),
                CustomerStatus = new List<SelectListItem> { new SelectListItem { Text = "Aktif", Value = "0" }, new SelectListItem { Text = "Pasif", Value = "1" } },
                CustomerClasses = (from i in configureResult.Data.Where(x => x.Type == 4).ToList()
                                   select new SelectListItem
                                   {
                                       Text = i.Info,
                                       Value = i.Name
                                   }).ToList(),
                Regions = (from i in configureResult.Data.Where(x => x.Type == 5).ToList()
                           select new SelectListItem
                           {
                               Text = string.Format("{0}  ({1})", i.Name, i.Info),
                               Value = i.Name
                           }).ToList(),
                Representatives = (from i in users.Data
                                   select new SelectListItem
                                   {
                                       Text = i.Name,
                                       Value = i.Name
                                   }).ToList(),

            };
            model.Cities.AddRange((from i in cities.Data
                                   select new SelectListItem
                                   {
                                       Text = i.Name,
                                       Value = i.Name
                                   }).ToList());

            if (!CurrentSession.Roles.Contains("Yonetim"))
            {
                model.Representatives = (from i in users.Data.Where(s => s.Id.ToString() == CurrentSession.id)
                                         select new SelectListItem
                                         {
                                             Text = i.Name,
                                             Value = i.Name
                                         }).ToList();
            }

            model.Customer = new Customer();
            model.Notlar = configureResult.Data.Where(x => x.Type == 11).ToList();
            return new SuccessDataResult<CustomerViewModel>(model);
        }

        public IDataResult<CustomerViewModel> GetCustomerViewModel(int id)
        {
            var cities = _configureService.GetAllCities();
            var users = _appUserService.GetAll();
            var customerResult = _customerService.GetCustomerById(id);
            var consignment = _consignmentService.GetById(id);
            var configureResult = _configureService.GetAll();
            var city = _configureService.GetTownsByCityName(customerResult.Data.City);
            List<Town> towns = new List<Town>();
            if (city.Data != null)
            {
                towns = _configureService.GetTownsByCityId(city.Data.Id).Data.ToList();
            }
          
            var model = new CustomerViewModel
            {
                CustomerTypes = new List<SelectListItem> { 
                    new SelectListItem { Text = "Firma", Value = "0" }, 
                    new SelectListItem { Text = "Sahsi Firma", Value = "1" },
                 new SelectListItem { Text = "Nihai Tüketici", Value = "2" }
                },
                KindOfCustomers = (from i in configureResult.Data.Where(x => x.Type == 2).ToList()
                                   select new SelectListItem
                                   {
                                       Text = i.Name,
                                       Value = i.Name
                                   }).ToList(),
                CustomerStatus = new List<SelectListItem> { new SelectListItem { Text = "Aktif", Value = "0" }, new SelectListItem { Text = "Pasif", Value = "1" } },
                CustomerClasses = (from i in configureResult.Data.Where(x => x.Type == 4).ToList()
                                   select new SelectListItem
                                   {
                                       Text = i.Info,
                                       Value = i.Name
                                   }).ToList(),
                Regions = (from i in configureResult.Data.Where(x => x.Type == 5).ToList()
                           select new SelectListItem
                           {
                               Text = string.Format("{0}  ({1})", i.Name, i.Info),
                               Value = i.Name
                           }).ToList(),
                Cities = (from i in cities.Data
                          select new SelectListItem
                          {
                              Text = i.Name,
                              Value = i.Name
                          }).ToList(),
                Representatives = (from i in users.Data
                                   select new SelectListItem
                                   {
                                       Text = i.Name,
                                       Value = i.Name
                                   }).ToList(),
                Towns = (from i in towns
                         select new SelectListItem
                         {
                             Text = i.Name,
                             Value = i.Name
                         }).ToList(),

            };
            model.Customer = customerResult.Data;
            model.CustomerConsignment = consignment.Data;

            return new SuccessDataResult<CustomerViewModel>(model);
        }


  



        public IActionResult CustomerConsignmentAdd(int id)
        {

            var model = new CustomerConsignmentViewModel();
            model.Id = id;
            model.customer = _customerService.GetCustomerById(id).Data;
            var cities = _configureService.GetAllCities();
            model.Cities = (from i in cities.Data
                            select new SelectListItem
                            {
                                Text = i.Name,
                                Value = i.Id.ToString()
                            }).ToList();

            return View(model);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult UpdateCustomer(int id)
        {

            return View(GetCustomerViewModel(id).Data);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            var result = _customerService.Update(customer);
            if (result.Success)
            {
                return RedirectToAction("UpdateCustomer", new { id = customer.Id });
            }

            return View(customer);
        }



        public IResult UpdateconfirmationType(int id, int confirmationType)
        {

            var customer = _customerService.GetCustomerById(id).Data;
            if (customer == null)
            {
                return new ErrorResult("Cari Bullunadı");
            }

            var logo = _logoService.CLCARDAdd(customer);
            if (!logo.Success)
            {
                return new ErrorResult("Logo  Hattasi");
            }
            customer.ConfirmationType = confirmationType;
            customer.LogoId = logo.Message.toint32();

            var model = _logoService.GetCLCARDByLogoId(customer.LogoId);

            customer.CariKodu = model.Data.CODE;

            var result = _customerService.Update(customer);
            return result;
        }



        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IResult AddCustomerConsignment([FromBody] CustomerConsignment dto)
        {


            var consignment = _consignmentService.GetList(x => x.CLIENTREF == dto.CLIENTREF);
            var countconsignment = consignment.Data.Count() + 100;
            dto.Code = "0" + countconsignment.ToString();
            var logo = _logoService.ArpShipmentLocationsPost(dto);

            if (!logo.Success)
            {
                return new SuccessResult("Logo Hatası");
            }
            dto.LogoId = logo.Message.toint32();

            var result = _consignmentService.Add(dto);

            return result;
        }

        public IResult DeleteCustomer(int id)
        {
            Customer customer = _customerService.GetCustomerById(id).Data;
            var result = _customerService.Delete(customer);
            return result;
        }
        public IResult DeleteCustomerConsignment(int id)
        {
            var c = _consignmentService.GetById(id).Data;
            var result = _consignmentService.Delete(c);
            return result;
        }


        public IActionResult CustomerConfirmation()
        {

            return View();
        }
        public IActionResult BigData()
        {

            return View();
        }

        //public IActionResult ConfirmCustomer(int id)
        //{
        //   var result= _customerService.ConfirmCustomer(id);
        //    return RedirectToAction("CustomerConfirmation");
        //}


        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult GetCustomerDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                var result = _customerService.GetCustomerDatatables(requestobj);
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }  
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult GetCallReportDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_customerService.GetCallReportDatatables(requestobj));
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult GetCustomerConfirmationDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_customerService.GetCustomerConfirmationDatatables(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult GetConsignmentDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_customerService.GetConsignmentDatatables(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }  
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult GetCARIEKSTRE([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_logoManagement.GetCARIEKSTRE(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }

        public IResult LogoUpdate(int id)
        {

            var customer = _customerService.GetCustomerById(id).Data;
            if (customer == null)
            {
                return new ErrorResult("Cari bulunamadı");
            }
            if (customer.LogoId == 0)
            {
                return new ErrorResult("LogoId bulunamadı");
            }

            var logo = _logoService.GetCLCARDByLogoId(customer.LogoId);
            if (!logo.Success)
            {
                return new ErrorResult("Logo  Hatasi");
            }
            customer.CariKodu = logo.Data.CODE;
            var result = _customerService.Update(customer);
            return result;
        }


        public IDataResult<List<Town>> GetTowns(string id)
        {
            var city = _configureService.GetTownsByCityName(id);
            if (city.Data == null)
            {

                return new SuccessDataResult<List<Town>>();
            }
            var towns = _configureService.GetTownsByCityId(city.Data.Id).Data.ToList();

            return new SuccessDataResult<List<Town>>(towns);
        } 
        public IDataResult<List<Cities_Area>> GetArea(string id)
        {
            var areas = _configureService.GetCities_Area(id);
            if (areas.Data == null)
            {

                return new SuccessDataResult<List<Cities_Area>>();
            }
            return new SuccessDataResult<List<Cities_Area>>(areas.Data);
        }
        public IDataResult<List<AppUser>> GetUserAll()
        {
            return _appUserService.GetAll();
        }



    }
}
