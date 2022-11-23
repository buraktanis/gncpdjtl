using Core.Results;
using Entities.Erp;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.DataTables.DatatablesJS;

namespace Business.ErpSalesManagement
{
    public interface IErpSalesManagement
    {
        public DataResult<List<Yillikkarnes>> Getyillikkarne(string yil, string ay);

        public DataResult<List<Alis>> GetAlis(int gunselect);
        DataTablesObjectResult GetSalesList(DatatablesObject requestobj);
        DataTablesObjectResult GetStokmaliyet(DatatablesObject requestobj);
        public DataTablesObjectResult GetSalesDetail(DatatablesObject requestobj);
        public DataResult<dynamic> GetTips();
        public DataResult<dynamic> Getbrands();
        public DataResult<dynamic> Gettahsilatraporus(string temsici);
        public DataResult<dynamic> fibabank();
        public DataResult<dynamic> kargo();
        public DataResult<dynamic> cariekstre();
        public DataResult<dynamic> iade(string iade, string iade2);
        public DataResult<dynamic> ciro(string yıl, string ay);
        public DataResult<dynamic> satisanaliz(string yıl);
        public DataResult<List<Personelperformas>> kisiay();
        public DataResult<List<Personelperformas>> kisigun();
        public DataResult<List<Brandperformas>> markaay();
        public DataResult<List<Brandperformas>> markagun();
        public DataResult<List<Accountcodemapping>> GetAccountcodelist();
        DataTablesObjectResult GetCARIEKSTRE(DatatablesObject requestobj);

    }
}