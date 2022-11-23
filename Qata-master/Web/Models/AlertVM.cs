using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class AlertVM
    {
        public string title { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public bool IsRedirecting { get; set; }
        public string redirectingUrl { get; set; }
        public int redirectingTimeout { get; set; }


        public List<string> Errors { get; set; }


        public void AddRangeError(List<string> obj)
        {
            Errors.AddRange(obj);
        }
        public void AddError(string message)
        {
            Errors.Add(message);
        }
        public AlertVM()
        {
            Errors = new List<string>();
            title = "";
            text = "";
            IsRedirecting = true;
            redirectingUrl = "/Home/Index";
            redirectingTimeout = 15000;
            icon = "success";
        }
    }
}
