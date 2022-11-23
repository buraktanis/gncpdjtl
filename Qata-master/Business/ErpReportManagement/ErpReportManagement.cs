using Core.Extensions;
using Core.Results;
using Entities.Erp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.ErpReportManagement
{
    public class ErpReportManagement : IErpReportManagement
    {
        public DataResult<List<Personelperformas>> Getdashboardrapor(string bastarih, string bistarih, string markaselect, string turselect)
        {
            string temsorgu = "";


            if (markaselect != "Tüm Markalar" && !markaselect.isNull())
            {
                temsorgu = string.Format(" AND fat.[Marka Kodu]='{0}' ", markaselect);
            }

            if (turselect != "Tüm Türler" && !turselect.isNull())
            {
                temsorgu = temsorgu + string.Format(" AND fat.[Malzeme Grup Kodu] ='{0}' ", turselect);
            }

            var model = string.Format(@"SELECT fat.[Fatura Tarihi] as tarihdate , SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit 
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' {2}  
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Fatura Tarihi] ORDER BY fat.[Fatura Tarihi] ", bastarih, bistarih, temsorgu).GetQuery<Personelperformas>("SCSlogo");

            foreach (var item in model)
            {
                item.tarih = item.tarihdate.ToString("yyyy-MM-dd");
                item.ciro = item.ciro / 1000;


            }
            return new SuccessDataResult<List<Personelperformas>>(model);
           
        }

        public DataResult<List<Personelperformas>> Getdashboardrapor2(string bastarih, string bistarih, string markaselect, string turselect)
        {
            string temsorgu = "";


            if (markaselect != "Tüm Markalar" && !markaselect.isNull())
            {
                temsorgu = string.Format(" AND fat.[Marka Kodu]='{0}' ", markaselect);
            }

            if (turselect != "Tüm Türler" && !turselect.isNull())
            {
                temsorgu = temsorgu + string.Format(" AND fat.[Malzeme Grup Kodu] ='{0}' ", turselect);
            }

            var model = string.Format(@" SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici,CASE (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]) WHEN 0 THEN 0 ELSE
               ((SELECT  COUNT(DISTINCT fsayisi.[Cari Kodu]) AS FIRMA FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fsayisi 
     	WHERE fsayisi.[Satır Net Tutarı KDV Dahil]<2000 AND fsayisi.[Fatura Türü]='Perakende Satış Faturası' AND 
	  fsayisi.[Satır Türü]='Malzeme' AND fsayisi.[Satış Temsilcisi]=fat.[Satış Temsilcisi] AND fsayisi.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' 
	 GROUP BY fsayisi.[Satış Temsilcisi])*100/
	 (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]))  END AS penetrasyon
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' {2}  
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC ", bastarih, bistarih, temsorgu).GetQuery<Personelperformas>("SCSlogo");

          
            return new SuccessDataResult<List<Personelperformas>>(model);
        } 
        
        public DataResult<CiroGunHaftaAyModel> GetCiroGunHaftaAy(string tempsorgu)
        {

            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (!tempsorgu.isNull())
            {
                tempsorgu = string.Format("  AND fat.[Satış Temsilcisi] ='{0}' ", tempsorgu);
            }

      

            var gunluklist = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici
            FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' 
            AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{0}'  {1}  GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC  ", DateTime.Now.ToString("yyyy-MM-dd"), tempsorgu).GetQuery<Personelperformas>("SCSlogo");

            var ayliklist = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici
            FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' 
            AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}'  {2}  GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC  ", firstDayOfMonth.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), tempsorgu).GetQuery<Personelperformas>("SCSlogo");

            var haftaliklist = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici
            FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' 
            AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}'  {2}  GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC  ", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), tempsorgu).GetQuery<Personelperformas>("SCSlogo");
            var model = new CiroGunHaftaAyModel();
            model.gunlukciro = gunluklist.Sum(x => x.ciro);
            model.gunlukadet = gunluklist.Sum(x => x.adet);

            model.aylikciro = ayliklist.Sum(x => x.ciro);
            model.aylikadet = ayliklist.Sum(x => x.adet);

            model.haftalikciro = haftaliklist.Sum(x => x.ciro);
            model.haftalikadet = haftaliklist.Sum(x => x.adet);

            return new SuccessDataResult<CiroGunHaftaAyModel>(model);
        }    

        public DataResult<dynamic> bayiPerformans(string bastarih, string bistarih, string temsilci = "Tüm Kişiler", string tips = "")
        {

            string temsorgu = "";
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


            return new SuccessDataResult<dynamic>(model);
        }
        public DataResult<dynamic> personelPerformans(string bastarih, string bistarih, string temsilci = "Tüm Kişiler", string tips = "")
        {

            string temsorgu = "";



            if (!tips.isNull())
            {
                temsorgu = temsorgu + string.Format(" AND fat.[Malzeme Grup Kodu] IN ('{0}') ", tips.Replace(",", "','"));
            }


            if (bastarih.isNull() || bistarih.isNull())
            {
                bastarih = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                bistarih = DateTime.Now.ToString("yyyy-MM-dd");
            }


            dynamic model = string.Format(@"SELECT  (COUNT(DISTINCT fat.[Cari Kodu])*100/tem2.bayi)  PENETRASYON , tem2.bayi BAYI, SUM([Satır Miktarı]) AS ADET, ROUND(SUM([Satır Net Tutarı KDV Dahil]),0) AS CIRO,
	COUNT(DISTINCT fat.[Cari Kodu]) AS FIRMA, COUNT(DISTINCT [Malzeme Kodu]) AS CESIT, COUNT(DISTINCT [Marka Kodu]) AS MARKA, ISNULL(fat.[Satış Temsilcisi],'?') AS KULLANICI 
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat 
	LEFT JOIN (SELECT [Satış Temsilcisi], COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI GROUP BY [Satış Temsilcisi]) tem2 ON tem2.[Satış Temsilcisi]=fat.[Satış Temsilcisi] 
	WHERE fat.[Fatura Türü] LIKE 'Perakende Satış%' AND fat.[Satır Türü]='Malzeme' AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' {2}
	GROUP BY fat.[Satış Temsilcisi], tem2.bayi 
	ORDER BY fat.[Satış Temsilcisi]", bastarih, bistarih, temsorgu).GetDynamicQuery("SCSlogo");


            return new SuccessDataResult<dynamic>(model);
        }

    }
}
