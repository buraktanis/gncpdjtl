using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Services.Logo
{
    public class MALZEME_LISTESI
    {
        public string Malz_Adı { get; set; }
        public string Malz_Kodu { get; set; }
        public string Birimi { get; set; }
        public double VAT { get; set; }
        public double Fiili_Stok { get; set; }

        public int LOGICALREF { get; set; }


    }
}
