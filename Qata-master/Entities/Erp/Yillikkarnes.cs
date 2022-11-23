using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Erp
{
    public class Yillikkarnes
    {
        public int yil { get; set; }
        public int ay { get; set; }

        public double adet { get; set; }

        public double ciro { get; set; }
        public double kar { get; set; }

        public int firma { get; set; }
        public int cesit { get; set; }
        public int marka { get; set; }
        public string kullanici { get; set; }
        
    }
}
