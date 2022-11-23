using Entities.Erp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class AlislarModel
    {
        public List<Alis> alislar { get; set; }
        public IEnumerable<string> markalar { get; set; }
        public int gunselect { get; set; }
    }
}
