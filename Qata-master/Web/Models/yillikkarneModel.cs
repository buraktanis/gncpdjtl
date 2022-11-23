using Entities.Erp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class yillikkarneModel
    {

        public string yil { get; set; }
        public string ay { get; set; }
        public List<Yillikkarnes> yillikkarne { get; set; }


    }
}