using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CustomerViewModel
    {
      
        public Customer Customer { get; set; }
        public List<Configuration> Notlar { get; set; }
        public List<CustomerConsignment> Consignments { get; set; }
        public CustomerConsignment CustomerConsignment { get; set; }
        public List<SelectListItem> CustomerTypes { get; set; }
        public List<SelectListItem> KindOfCustomers { get; set; }
        public List<SelectListItem> CustomerStatus { get; set; }
        public List<SelectListItem> CustomerClasses { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> Regions { get; set; }
        public List<SelectListItem> Towns { get; set; }
        public List<SelectListItem> Representatives { get; set; }
        public CustomerViewModel()
        {
            this.Towns = new List<SelectListItem>();
            Towns.Add(new SelectListItem { Text = "Ilce seciniz", Value="" });
            this.Cities = new List<SelectListItem>();
            Cities.Add(new SelectListItem { Text = "İl seciniz", Value = "" });
        }

    }
}
