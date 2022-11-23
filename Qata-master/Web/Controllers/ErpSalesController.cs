using Business.ErpSalesManagement;
using Core.DataTables;
using Core.Extensions;
using Core.Helpers;
using Core.Results;
using Entities.Erp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class ErpSalesController : Controller
    {

        private IErpSalesManagement _erpSalesManagement;

        public ErpSalesController(IErpSalesManagement erpSalesManagement)
        {
            _erpSalesManagement = erpSalesManagement;
        }
        

        public IActionResult Sale()
        {
            return View();
        }
        public IActionResult Stokmaliyet()
        {
            return View();
        }

        public IActionResult Tahsilatraporu()
        {
            string tempsorgu = "";
            if (!CurrentSession.Roles.Contains("Yonetim"))
            {
                tempsorgu = string.Format(" [TEMSİLCİ] ='{0}' AND", CurrentSession.Username);
            }

            var model = _erpSalesManagement.Gettahsilatraporus(tempsorgu).Data;
            return View(model);
        }

        [HttpPost]
        public IActionResult GetSalesList([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_erpSalesManagement.GetSalesList(requestobj));
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }



        public IActionResult Alis(int gunselect = 7)
        {

            var alislarModel = new AlislarModel();
            alislarModel.alislar = _erpSalesManagement.GetAlis(gunselect).Data;


            alislarModel.markalar = alislarModel.alislar.GroupBy(r => new { r.marka }).Select(x => x.Key).Select(x => x.marka);
            alislarModel.gunselect = gunselect;

            return View(alislarModel);
        }


        [HttpPost]
        public IActionResult GetSalesDetail([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_erpSalesManagement.GetSalesDetail(requestobj));
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }

        [HttpPost]
        public IActionResult GetStokmaliyet([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_erpSalesManagement.GetStokmaliyet(requestobj));
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }


        public IDataResult<dynamic> GetTips()
        {
            return _erpSalesManagement.GetTips();
        }
        public IDataResult<dynamic> Getbrands()
        {
            return _erpSalesManagement.Getbrands();
        }
        public IActionResult fibabank()
        {
            var model = _erpSalesManagement.fibabank().Data;
            return View(model);
        }
        public IActionResult kargo()
        {
            var model = _erpSalesManagement.kargo().Data;
            return View(model);
        }
        public IActionResult cariekstre()
        {
            var model = _erpSalesManagement.cariekstre().Data;
            return View(model);
        }
        public IActionResult iade(string iade, string iade2)
        {
            var model = new iadeModel();
            model.Satir_tutariToplam = 0;


            model.model1 = _erpSalesManagement.iade(iade, iade2).Data;

            return View(model);
        }
        public IActionResult ciro(string yıl, string ay)
        {
            var model = new ciroModel();
            model.ciroToplam = 0;
            model.iadeToplam = 0;
            model.netToplam = 0;
            model.model1 = _erpSalesManagement.ciro(yıl, ay).Data;

            return View(model);
        }
        public IActionResult satisanaliz(string yıl)
        {
            var model = new satisanalizModel();
            model.model1 = _erpSalesManagement.satisanaliz(yıl).Data;

            return View(model);
        }
        public IActionResult kisiay()
        {

            var model = new kisiayModel();
            model.kisiay = _erpSalesManagement.kisiay().Data;

            return View(model);

        }
        public IActionResult kisigun()
        {

            var model = new kisigunModel();
            model.kisigun = _erpSalesManagement.kisigun().Data;

            return View(model);

        }
        public IActionResult markaay()
        {
            var model = new markaayModel();
            model.markaay = _erpSalesManagement.markaay().Data;

            return View(model);
        }
        public IActionResult markagun()
        {
            var model = new markagunModel();
            model.markagun = _erpSalesManagement.markagun().Data;

            return View(model);
        }
        public IActionResult aylikkarne(int ay = 0, string markaselect = "Tum Markalar", string tips = "")
        {
            List<aylikkarneModel> model = new List<aylikkarneModel>();

            string temsorgu = "";
            ViewBag.markaselect = markaselect;
            ViewBag.tips = tips;
            ViewBag.ay = ay;


            if (markaselect != "Tum Markalar")
            {
                temsorgu = string.Format(" AND fat.[Marka Kodu]='{0}' ", markaselect);
            }

            if (!tips.isNull())
            {
                temsorgu = temsorgu + string.Format(" AND fat.[Malzeme Grup Kodu] IN ('{0}') ", tips.Replace(",", "','"));
            }


            for (int i = 0; i <= ay; i++)
            {
                aylikkarneModel aylikkarne = new aylikkarneModel();

                aylikkarne.aylikkarnes = string.Format(@"SELECT YEAR(DATEADD(MONTH,{0},GETDATE())) yil, CASE WHEN MONTH(DATEADD(MONTH,{0},GETDATE()))<10 THEN '0' + CONVERT(CHAR,MONTH(DATEADD(MONTH,{0},GETDATE()))) ELSE CONVERT(CHAR,MONTH(DATEADD(MONTH,{0},GETDATE()))) END ay, ISNULL(tem2.bayi,0) bayi, ISNULL(SUM([Satır Miktarı]),0) AS ADET, ROUND(ISNULL(SUM([Satır Net Tutarı KDV Dahil]),0),0) AS ciro, ROUND(ISNULL(SUM([Satış Kar]),0),0) AS kar, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Malzeme Kodu]) AS cesit, COUNT(DISTINCT [Marka Kodu]) AS marka, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat LEFT JOIN (SELECT [Satış Temsilcisi], COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI   GROUP BY [Satış Temsilcisi]) tem2 ON tem2.[Satış Temsilcisi]=fat.[Satış Temsilcisi] WHERE fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' AND fat.[Yıl]=YEAR(DATEADD(MONTH,{0},GETDATE())) AND CONVERT(INT,LEFT(fat.AY,2))=CONVERT(CHAR,MONTH(DATEADD(MONTH,{0},GETDATE()))) {1} GROUP BY fat.[Satış Temsilcisi], tem2.bayi ", -i, temsorgu).GetQuery<Aylikkarne>("SCSlogo");
                if (aylikkarne.aylikkarnes.Count > 0)
                {
                    aylikkarne.ay = aylikkarne.aylikkarnes[0].ay.ToString();
                    aylikkarne.yil = aylikkarne.aylikkarnes[0].yil;
                    model.Add(aylikkarne);
                }


            }

            return View(model);

        }
        public IActionResult yillikkarne(string yil = "2021", string ay = "1,2,3,4,5,6,7,8,9,10,11,12")
        {
            List<yillikkarneModel> model = new List<yillikkarneModel>();


            yillikkarneModel yillikkarne = new yillikkarneModel();
            string temsorgu = "";
            ViewBag.ay = ay;
            ViewBag.yil = yil;


            yillikkarne.yillikkarne = string.Format(@"SELECT CONVERT(INT,LEFT(fat.AY,2)) AS ay, ISNULL(SUM([Satır Miktarı]),0) AS adet, ROUND(ISNULL(SUM([Satır Net Tutarı KDV Dahil]),0),0) AS ciro, ROUND(ISNULL(SUM([Satış Kar]),0),0) AS kar, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Malzeme Kodu]) AS cesit, COUNT(DISTINCT [Marka Kodu]) AS marka, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat 
 WHERE fat.[Satış Temsilcisi]!='' AND MONTH(fat.[Fatura Tarihi])  IN({0}) AND fat.[Yıl]={1} AND fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme'  GROUP BY fat.[Satış Temsilcisi],CONVERT(INT,LEFT(fat.AY,2)) ORDER BY CONVERT(INT,LEFT(fat.AY,2))", ay, yil).GetQuery<Yillikkarnes>("SCSlogo");


            return View(yillikkarne);
        }

        public IActionResult accountcodelist()
        {

            List<Accountcodemapping> model = new List<Accountcodemapping>();

            accountcodelistModel accountcodelist = new accountcodelistModel();

            accountcodelist.accountcodelist = string.Format(@"SELECT * FROM accountcodemapping").GetQuery<Accountcodemapping>("SCSlogo");
            

            return View(accountcodelist);
        }

        public IActionResult imei(string imei)
        {
            string tempsorgu = string.Format(" IMEI ='{0}' ", imei); 




            var model = new imeiModel();
            model.model1 = string.Format(@"SELECT * FROM ARY_XXX_IMEI_BAZLI_ALIM  WHERE {0} ", tempsorgu).GetDynamicQuery("SCSlogo");

            model.model2 = string.Format(@"SELECT * FROM ARY_XXX_IMEI_BAZLI_SATIS  WHERE {0} ", tempsorgu).GetDynamicQuery("SCSlogo");



            return View(model);
        }

        public IActionResult top5()
        {
            var model = new top5Model();

            string yıl = string.Format(" Yıl ='{0}' ", DateTime.Now.ToString("yyyy"));
            string ay = "";
            if (DateTime.Now.Month == 1)
            {
                ay = " AY = '01-Ocak' ";
            }
            else if (DateTime.Now.Month == 2)
            {
                ay = " AY = '02-Şubat' ";
            }
            else if (DateTime.Now.Month == 3)
            {
                ay = " AY = '03-Mart' ";
            }
            else if (DateTime.Now.Month == 4)
            {
                ay = " AY = '04-Nisan' ";
            }
            else if (DateTime.Now.Month == 5)
            {
                ay = " AY = '05-Mayıs' ";
            }
            else if (DateTime.Now.Month == 6)
            {
                ay = " AY = '06-Haziran' ";
            }
            else if (DateTime.Now.Month == 7)
            {
                ay = " AY = '07-Temmuz' ";
            }
            else if (DateTime.Now.Month == 8)
            {
                ay = " AY = '08-Ağustos' ";
            }
            else if (DateTime.Now.Month == 9)
            {
                ay = " AY = '09-Eylül' ";
            }
            else if (DateTime.Now.Month == 10)
            {
                ay = " AY = '10-Ekim' ";
            }
            else if (DateTime.Now.Month == 11)
            {
                ay = " AY = '11-Kasım' ";
            }
            else if (DateTime.Now.Month == 12)
            {
                ay = " AY = '12-Aralık' ";
            }


            model.top5ciro = string.Format(@"SELECT    Top (5) ROW_NUMBER() OVER(ORDER BY SUM([Satır Tutarı]) ) AS sira, SUM(- [Satır Tutarı]) AS Ciro, Yıl, AY, Slsman
FROM            dbo.ERP_AYRINTILI_FATURA where  Slsman!='SAVAŞ KAYACAN' and Slsman!='FIRAT KAYACAN' and Slsman!='BARIŞ ÖZCAN' and  {0} and {1}
GROUP BY  Yıl, AY, Slsman order by Ciro desc ", yıl, ay).GetDynamicQuery("SCSlogo");

            model.top5yenilenmis = string.Format(@"SELECT       TOP(5) ROW_NUMBER() OVER(ORDER BY SUM([Satır Tutarı]) ) AS sira,  [Malzeme Grup Kodu] AS malzeme_grup_kodu, SUM(- [Satır Tutarı]) AS Ciro, Yıl, AY, Slsman
FROM            dbo.ERP_AYRINTILI_FATURA where  Slsman!='SAVAŞ KAYACAN' and Slsman!='FIRAT KAYACAN' and Slsman!='BARIŞ ÖZCAN' and [Malzeme Grup Kodu]='YENİLENMİŞ CİHAZ' AND {0} and {1}
GROUP BY [Malzeme Grup Kodu], Yıl, AY, Slsman", yıl, ay).GetDynamicQuery("SCSlogo");

            model.top5ceptelefonu = string.Format(@"SELECT       TOP(5) ROW_NUMBER() OVER(ORDER BY SUM([Satır Tutarı]) ) AS sira,  [Malzeme Grup Kodu] AS malzeme_grup_kodu, SUM(- [Satır Tutarı]) AS Ciro, Yıl, AY, Slsman
FROM            dbo.ERP_AYRINTILI_FATURA where  Slsman!='SAVAŞ KAYACAN' and Slsman!='FIRAT KAYACAN' and Slsman!='BARIŞ ÖZCAN' and [Malzeme Grup Kodu]='CEP TELEFONU' AND {0} and {1}
GROUP BY [Malzeme Grup Kodu], Yıl, AY, Slsman", yıl, ay).GetDynamicQuery("SCSlogo");

            model.top5aksesuar = string.Format(@"SELECT       TOP(5) ROW_NUMBER() OVER(ORDER BY SUM([Satır Tutarı]) ) AS sira,  [Malzeme Grup Kodu] AS malzeme_grup_kodu, SUM(- [Satır Tutarı]) AS Ciro, Yıl, AY, Slsman
FROM            dbo.ERP_AYRINTILI_FATURA where  Slsman!='SAVAŞ KAYACAN' and Slsman!='FIRAT KAYACAN' and Slsman!='BARIŞ ÖZCAN' and [Malzeme Grup Kodu]='AKSESUAR' AND {0} and {1}
GROUP BY [Malzeme Grup Kodu], Yıl, AY, Slsman", yıl, ay).GetDynamicQuery("SCSlogo");

            return View(model);
        }

        public IActionResult cariekstresi(string bastarih,string bistarih,string satistemsilcisi)
        {
            if (CurrentSession.Roles.Contains("Yonetim"))
            {
                satistemsilcisi = "";
            }
            else
            {
                satistemsilcisi = string.Format(" and TEMSİLCİ = '{0}' ", CurrentSession.Username);
            }
            
            string tarih1 = string.Format(" ' {0} ' ", bastarih);
            string tarih2 = string.Format(" ' {0} ' ", bistarih);

            var model = new cariModel();

            model.model1 = string.Format(@"SELECT [CH KODU]
      ,[CH ÜNVANI] as chunvani,
      [TARIH]
      ,[HAREKET_TURU]
      ,[BORÇ]
      ,[ALACAK]
      ,[BAKIYE]
      ,[TEMSİLCİ]
      
  FROM [tiger].[dbo].[ARY_XXX_CARI_EKSTRE] where (TARIH between {0} and {1}) {2}", tarih1,tarih2,satistemsilcisi).GetDynamicQuery("SCSlogo");

            return View(model);

        }

        public IActionResult gtstemsilci()
        {
            return View();
        }

        public IActionResult gtsiparis()
        {
            return View();
        }
        public IActionResult stokyaslandirma()
        {


            dynamic model = string.Format(@"SELECT  [MLZ_KODU] ,[MLZ_ADI],[MIKTAR],[MIKTAR_STOK],[GIRIS_TARIHI],[STOK_GUN],[CH_UNVANI] FROM [tiger].[dbo].[ARY_STOK_YASLANDIRMA] where MIKTAR_STOK > 0 ").GetDynamicQuery("SCSlogo");

            return View(model);
        }
    }
}
