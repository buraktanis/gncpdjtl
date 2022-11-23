using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CustomerConsignmentViewModel
    {

        public int Id { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public Customer customer { get; set; }

        public CustomerConsignmentViewModel()
        {
            
            this.Cities = new List<SelectListItem>();
            Cities.Add(new SelectListItem { Text = "Il seciniz", Value = "" });
        }
    }
}
