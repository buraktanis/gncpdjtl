using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class BigdataViewModel
    {
      
        public BigData bigData { get; set; }
        public List<CustomerConsignment> Consignments { get; set; }
        public CustomerConsignment bigDataConsignment { get; set; }
        public List<SelectListItem> bigDataTypes { get; set; }
        public List<SelectListItem> KindOfbigDatas { get; set; }
        public List<SelectListItem> bigDataStatus { get; set; }
        public List<SelectListItem> bigDataClasses { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> Regions { get; set; }
        public List<SelectListItem> Towns { get; set; }
        public List<SelectListItem> Representatives { get; set; }
        public List<Configuration> Notlar { get; set; }
        public BigdataViewModel()
        {
            this.Towns = new List<SelectListItem>();
            Towns.Add(new SelectListItem { Text = "Ilce seciniz",  });
            this.Cities = new List<SelectListItem>();
            Cities.Add(new SelectListItem { Text = "Il seciniz" });
        }

    }
}
