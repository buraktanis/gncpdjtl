using Core.Results;
using Entities.Concrete;
using Entities.Services.Logo;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.DataTables.DatatablesJS;

namespace Business.LogoManagement
{
     public interface ILogoManagement
    {
        public DataResult<CariBakiye> CariBakiyeByLogoId(string logoid);
        public DataResult<List<Customer>> TaxTcControl(string no);
        public DataTablesObjectResult GetStokDatatables(DatatablesObject requestobj);
        public DataResult<List<STOK_DURUM>> GetLogoStok();
        IResult bayiPerformans(string bastarih, string bistarih, string temsilci = "Tüm Kişiler", string tips = "");
        public DataResult<List<Customer>> Carilist();
        public DataResult<List<MALZEME_LISTESI>> Get_MALZEME_LISTESI_By_LOGICALREF(int logoid);
        DataTablesObjectResult GetBigData(DatatablesObject requestobj);
        DataResult<List<ARY_017_SİPARİŞ_HAREKETLERI>> ARY_017_SİPARİŞ_HAREKETLERI_List(List<string> carinos);
        public DataTablesObjectResult GetCARIEKSTRE(DatatablesObject requestobj);
        public DataResult<List<Customer>> TelControl(string no);
    }
}
