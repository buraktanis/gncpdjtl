using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Services.Logo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public OrderItem OrderItem { get; set; }
        public List<SelectListItem> DeliveryAddresses { get; set; }
        public List<SelectListItem> WareHouses { get; set; }
        public List<SelectListItem> PaymentTypes { get; set; }
        public List<SelectListItem> DeliveryType { get; set; }
        public List<SelectListItem> AddressDTOs { get; set; }
        public List<SelectListItem> Slsmans { get; set; }
        public List<STOK_DURUM> STOK_DURUM { get; set; }
        public Customer customer { get; set; }

        public OrderViewModel()
        {
            STOK_DURUM = new List<STOK_DURUM>();
            PaymentTypes = new List<SelectListItem>();
            AddressDTOs = new List<SelectListItem>();
            PaymentTypes.Add(new SelectListItem { Text = "Seçiniz", Value = "" });
            DeliveryType = new List<SelectListItem>();
            DeliveryType.Add(new SelectListItem { Text = "Seçiniz", Value = "" });
            AddressDTOs.Add(new SelectListItem { Text = "Merkez", Value = "0" });
        }

    }
}
