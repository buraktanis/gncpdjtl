using Business.AppUserManagement;
using Business.ConfigurationManagement;
using Business.CustomerConsignmentManagement;
using Business.CustomerManagement;
using Business.ItemManagemnet;
using Business.LogoManagement;
using Business.OrderItemManagement;
using Business.OrderManagement;
using Core.DataTables;
using Core.Extensions;
using Core.Results;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Services.Logo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private ISettinManager _itemManager;
        private IOrderService _orderService;
        private IOrderItemService _orderItemService;
        private IConfigureService _configureService;
        private ICustomerService _customerService;
        private ILogoManagement _logoManagement;
        private IConsignmentService _consignmentService;
        private IAppUserService _appUserService;
        private ILogoService _logoService;
        public OrderController(IOrderService orderService, IConfigureService configureService,
            ICustomerService customerService, ILogoManagement logoManagement,
            IOrderItemService orderItemService,
            IConsignmentService consignmentService,
            IAppUserService appUserService,
            ILogoService logoService,
            ISettinManager itemManager
            )

        {

            _itemManager = itemManager;
            _logoService = logoService;
            _appUserService = appUserService;
            _consignmentService = consignmentService;
            _orderItemService = orderItemService;
            _logoManagement = logoManagement;
            _orderService = orderService;
            _configureService = configureService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {


            return View();
        }

        [HttpGet]
        public IActionResult NewOrder()
        {

            var configureResult = _configureService.GetAll();
            var users = _appUserService.GetAll();
            var model = new OrderViewModel()
            {

                WareHouses = (from i in configureResult.Data.Where(x => x.Type == 6).ToList()
                              select new SelectListItem
                              {
                                  Text = string.Format("{0}  ({1})", i.Name, i.Info),
                                  Value = i.Id.ToString()
                              }).ToList(),

                Slsmans = (from i in users.Data
                           select new SelectListItem
                           {
                               Text = i.Name,
                               Value = i.Name
                           }).ToList(),

            };

            model.DeliveryType.AddRange((from i in configureResult.Data.Where(x => x.Type == 8).OrderBy(x => x.Info).ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.Info,
                                             Value = i.Name
                                         }).ToList());
            model.PaymentTypes.AddRange((from i in configureResult.Data.Where(x => x.Type == 7).ToList()
                                         select new SelectListItem
                                         {
                                             Text = string.Format("{0}  ({1})", i.Name, i.Info),
                                             Value = i.Name
                                         }).ToList());
            return View(model);
        }

        [HttpPost]
        public IActionResult NewOrder(Order order)
        {
            order.Status = -1;
            order.OrderDate = DateTime.Now;
            var result = _orderService.Add(order);
            if (result.Success)
            {

                return RedirectToAction("UpdateOrder", new { id = result.Data.Id });
            }

            return View(order);
        }

        [HttpGet]
        public IActionResult UpdateOrder(int id)
        {
            var result = _orderService.GetOrderById(id);
            var customerResult = _customerService.GetCustomerById(result.Data.CustomerId);
            var configureResult = _configureService.GetAll();
            var address = _customerService.GetAddressCLIENTREF(customerResult.Data.LogoId).Data;
            var users = _appUserService.GetAll();
            var model = new OrderViewModel()
            {
                customer = customerResult.Data,
                DeliveryType = (from i in configureResult.Data.Where(x => x.Type == 8).OrderBy(x => x.Info).ToList()
                                select new SelectListItem
                                {
                                    Text = i.Info,
                                    Value = i.Name
                                }).ToList(),
                PaymentTypes = (from i in configureResult.Data.Where(x => x.Type == 7).ToList()
                                select new SelectListItem
                                {
                                    Text = string.Format("{0}  ({1})", i.Name, i.Info),
                                    Value = i.Name
                                }).ToList(),
                WareHouses = (from i in configureResult.Data.Where(x => x.Type == 6).ToList()
                              select new SelectListItem
                              {
                                  Text = string.Format("{0}  ({1})", i.Name, i.Info),
                                  Value = i.Id.ToString()
                              }).ToList(),
                Slsmans = (from i in users.Data
                           select new SelectListItem
                           {
                               Text = i.Name,
                               Value = i.Name
                           }).ToList()
            };
            model.AddressDTOs.AddRange((from i in address
                                        select new SelectListItem
                                        {
                                            Text = i.CityId + " " + i.TownId + " " + i.Address,
                                            Value = i.Id.ToString()
                                        }).ToList());
            model.STOK_DURUM = _logoManagement.GetLogoStok().Data;
            model.Order = result.Data;
            var itemlist = _itemManager.GetList().Data;
            foreach (var item in itemlist)
            {
                model.STOK_DURUM.Add(new STOK_DURUM
                {
                    AD = item.Name,
                    Kod = item.Code,
                    STOK=item.Stock,
                    LOGICALREF = item.LogoId

                });
            }


            return View(model);


        }

        [HttpPost]
        public IActionResult UpdateOrder(Order order)
        {
            var result = _orderService.Update(order);
            if (result.Success)
            {
                return RedirectToAction("UpdateOrder", new { id = order.Id });
            }

            return View();
        }

        public IResult DeleteOrder(int id)
        {
            var order = _orderService.GetOrderById(id).Data;
            var result = _orderService.Delete(order);
            return new SuccessResult();
        }



        public IActionResult OrderItem(int id)
        {
            ViewBag.id = id;
            var model = _logoManagement.GetLogoStok();
            return View(model.Data);
        }

        public IResult AddOrderItem([FromBody] AddOrderItemDTOs model)
        {
            if (model.Price == 0)
            {
                return new ErrorResult("Fiyat 0 Olamaz");
            }
            if (model.Amount == 0)
            {
                return new ErrorResult("Miktar 0 Olamaz");
            }

            var logoitem = _logoManagement.Get_MALZEME_LISTESI_By_LOGICALREF(model.LogoId).Data.FirstOrDefault();
            var item = _itemManager.Get(x => x.LogoId == model.LogoId);

            if (logoitem == null && item.Data == null)
            {
                return new ErrorResult("Mazeme Bulunmadı");

            }

            var orderItem = new OrderItem();
            if (logoitem != null)
            {
                orderItem = new OrderItem
                {
                    OrderId = model.OrderId,
                    Vat = logoitem.VAT.toint32(),
                    UnitPrice = model.Price,
                    ProductName = logoitem.Malz_Adı,
                    UnitsInStock = model.Amount,
                    LogoKod = logoitem.Malz_Kodu,
                    LogoId = model.LogoId,
                    ItemType = 0

                   
                };
            }

            if (item.Data != null)
            {
                orderItem = new OrderItem
                {
                    OrderId = model.OrderId,
                    Vat = 18,
                    UnitPrice = model.Price,
                    ProductName = item.Data.Name,
                    UnitsInStock = model.Amount,
                    LogoKod = item.Data.Code,
                    LogoId = model.LogoId,
                    ItemType=item.Data.ItemType
                    
                };
            }

            var itemcontrol = _orderItemService.GetOrderItemByLogoIdAndOrderId(model.LogoId, model.OrderId);
            if (itemcontrol.Data == null)
            {
                var orderitem = _orderItemService.Add(orderItem);
            }



            return new SuccessResult();
        }

        [HttpPost]
        public IActionResult GetOrderDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_orderService.GetOrderDatatables(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }   
        [HttpPost]
        public IActionResult GetCustomerOrders([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_orderService.GetCustomerOrders(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }
        [HttpPost]
        public IActionResult GetOrderConfirmayionDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_orderService.GetOrderConfirmayionDatatables(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }

        [HttpPost]
        public IActionResult GetStokDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_logoManagement.GetStokDatatables(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }

        [HttpPost]
        public IActionResult GetOrderItemByOrderIdDatatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                var result = _orderItemService.GetOrderItemByOrderIdDatatables(requestobj);
                return new JsonResult(_orderItemService.GetOrderItemByOrderIdDatatables(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }
        public IActionResult StockControl()
        {
            var model = _logoManagement.GetLogoStok();
            return View(model.Data);
        }

        public IResult UpdateStatus(int id, int status)
        {

            var order = _orderService.GetOrderById(id).Data;
            if (order == null)
            {
                return new ErrorResult("Sipariş Bulunamadı");
            }
            var OrderItemlist = _orderItemService.GetOrderItemByOrderId(id);
            if (OrderItemlist.Data.Count == 0)
            {
                return new ErrorResult("Mazeme Bulunamadı");
            }

            var customer = _customerService.GetCustomerById(order.CustomerId);

            if (customer.Data == null)
            {
                return new ErrorResult("Cari Bulunamadı");
            }
            var consignment = new CustomerConsignment();
            consignment.Address = customer.Data.Address;
            consignment.CityId = customer.Data.City;
            consignment.TownId = customer.Data.Town;

            if (order.CustomerConsignmentId > 0)
            {
                var consignmentResult = _consignmentService.GetById(order.CustomerConsignmentId).Data;
                if (consignmentResult != null)
                {
                    consignment.Address = consignmentResult.Address;
                    consignment.CityId = consignmentResult.CityId;
                    consignment.TownId = consignmentResult.TownId;
                }


            }


            var logo = _logoService.AddOrderLogo(order, (List<OrderItem>)OrderItemlist.Data, customer.Data, consignment);

            if (!logo.Success)
            {
                return new ErrorResult("Logo hatası");
            }
            order.OrderNo = logo.Message;
            order.Status = status;
            var result = _orderService.Update(order);
            return result;
        }

        public IResult UpdateStatusOnay(int id, int status)
        {

            var order = _orderService.GetOrderById(id).Data;
            if (order == null)
            {
                return new ErrorResult("Sipariş Bulunamadı");
            }
            var OrderItemlist = _orderItemService.GetOrderItemByOrderId(id);
            if (OrderItemlist.Data.Count == 0)
            {
                return new ErrorResult("Mazeme Bulunamadı");
            }

            var customer = _customerService.GetCustomerById(order.CustomerId);

            if (customer.Data == null)
            {
                return new ErrorResult("Cari Bulunamadı");
            }



            order.Status = status;
            var result = _orderService.Update(order);
            return result;
        }
        public IActionResult OrderConfirmation()
        {
            return View();
        }

        public IResult DeleteOrderItem(int id)
        {
            var orderItem = _orderItemService.GetOrderItemById(id).Data;
            var result = _orderItemService.Delete(orderItem);

            return new SuccessResult();
        }
        public IResult GetConsignmentCustomerId(int id)
        {
            return new SuccessDataResult<IList<CustomerConsignment>>(_consignmentService.GetByCustomerId(id).Data);
        } 
        public IResult GetCariBakiyeByLogoId(string id)
        {
            
            return _logoManagement.CariBakiyeByLogoId(id);
        }

        public IDataResult<List<AddressDTOs>> GetAddressCLIENTREF(int id)
        {

            return new SuccessDataResult<List<AddressDTOs>>(_customerService.GetAddressCLIENTREF(id).Data);
        }
    }
}
