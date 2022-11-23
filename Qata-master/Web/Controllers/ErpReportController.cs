using Business.ErpReportManagement;
using Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ErpReportController : Controller
    {

        private IErpReportManagement _erpReportManagement;
        public ErpReportController(IErpReportManagement erpReportManagement)
        {
            _erpReportManagement = erpReportManagement;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult bayiPerformans(string bastarih, string bistarih, string temsilci = "Tüm Kişiler", string tips = "")
        {
            string temsorgu = "";
            ViewBag.select = temsilci;
            if (temsilci != "Tüm Kişiler")
            {
                temsorgu = string.Format(" AND fat.[Satış Temsilcisi]='{0}' ", temsilci);
            }
            if (bastarih.isNull() || bistarih.isNull())
            {
                bastarih = DateTime.Now.AddYears(-3).ToString("yyyy-MM-dd");
                bistarih = DateTime.Now.ToString("yyyy-MM-dd");
            }

            dynamic model = string.Format(@"SELECT fat.[Satış Temsilcisi] SORUMLU, cari.[Cari Kodu] KODU, cari.[Cari Ünvanı] UNVAN, ISNULL(SUM([Satır Miktarı]),0) AS ADET, COUNT(DISTINCT fat.[Fatura No]) AS SIPARIS, ROUND(ISNULL(SUM(fat.[Satır Net Tutarı KDV Dahil]),0), 0) AS CIRO, COUNT(DISTINCT fat.[Model]) AS CESIT, ISNULL(FORMAT((SELECT MIN([Fatura Tarihi]) FROM ARY_XXX_AYRINTILI_SATIS_RAPORU WHERE [Cari Kodu]=cari.[Cari Kodu] and [Fatura Tarihi] BETWEEN '{0}' AND '{1}'),'yyyy-MM-dd'),'') ILK_SIPARIS,
 ISNULL(FORMAT((SELECT MAX([Fatura Tarihi]) FROM ARY_XXX_AYRINTILI_SATIS_RAPORU WHERE [Cari Kodu]=cari.[Cari Kodu] and [Fatura Tarihi] BETWEEN '{0}' AND '{1}'),'yyyy-MM-dd'),'') SON_SIPARIS FROM ARY_017_CARI_MUSTER_TEMSILCI cari LEFT JOIN ARY_XXX_AYRINTILI_SATIS_RAPORU fat ON fat.[Cari Kodu]=cari.[Cari Kodu] AND 
 fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' WHERE cari.[Cari Kodu]<>'' {2}  GROUP BY fat.[Satış Temsilcisi], cari.[Cari Kodu], cari.[Cari Ünvanı] Having  COUNT(DISTINCT fat.[Fatura No])>0 ORDER BY cari.[Cari Ünvanı]", bastarih, bistarih, temsorgu).GetDynamicQuery("SCSlogo");


            return View(model);
        }
        public IActionResult personelPerformans(string bastarih, string bistarih, string markaselect = "Tum Markalar", string tips = "")
        {

            string temsorgu = "";
            ViewBag.select = markaselect;
            ViewBag.tips = tips;

            if (markaselect != "Tum Markalar")
            {
                temsorgu = string.Format(" AND fat.[Marka Kodu]='{0}' ", markaselect);
            }

            if (!tips.isNull())
            {
                temsorgu = temsorgu + string.Format(" AND fat.[Malzeme Grup Kodu] IN ('{0}') ", tips.Replace(",", "','"));
            }


            if (bastarih.isNull() || bistarih.isNull())
            {
                bastarih = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                bistarih = DateTime.Now.ToString("yyyy-MM-dd");
            }
            ViewBag.bastarih = bastarih;
            ViewBag.bistarih = bistarih;

            dynamic model = string.Format(@"SELECT  (COUNT(DISTINCT fat.[Cari Kodu])*100/tem2.bayi)  PENETRASYON , tem2.bayi BAYI, SUM([Satır Miktarı]) AS ADET, ROUND(SUM([Satır Net Tutarı KDV Dahil]),0) AS CIRO,
	COUNT(DISTINCT fat.[Cari Kodu]) AS FIRMA, COUNT(DISTINCT [Malzeme Kodu]) AS CESIT, COUNT(DISTINCT [Marka Kodu]) AS MARKA, ISNULL(fat.[Satış Temsilcisi],'?') AS KULLANICI 
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat 
	LEFT JOIN (SELECT [Satış Temsilcisi], COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI GROUP BY [Satış Temsilcisi]) tem2 ON tem2.[Satış Temsilcisi]=fat.[Satış Temsilcisi] 
	WHERE fat.[Fatura Türü] LIKE 'Perakende Satış%' AND fat.[Satır Türü]='Malzeme' AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' {2}
	GROUP BY fat.[Satış Temsilcisi], tem2.bayi 
	ORDER BY fat.[Satış Temsilcisi]", bastarih, bistarih, temsorgu).GetDynamicQuery("SCSlogo");


            return View(model);
        }
    }
}
