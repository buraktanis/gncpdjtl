using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Services.Logo
{
    public class STOK_DURUM
    {

        public string Kod { get; set; }
        public string MarkaKodu { get; set; }
        public string AD { get; set; }
        public string MalzemeGrupKodu { get; set; }
        public string Model { get; set; }
        public string AltGrup { get; set; }
        public double STOK { get; set; }
        public double ONERI_SIP { get; set; }
        public double ONAYLI_SIP { get; set; }
        public double KLN_STOK { get; set; }
        public double Bekleyen_STOK { get; set; }
        public DateTime GUNCEL_TARIH { get; set; }
        public int LOGICALREF { get; set; }
        public double mal_min { get; set; }
        public double mal_max { get; set; }
        public double mal_son { get; set; }

        public double mal_ort { get; set; }



    }
}