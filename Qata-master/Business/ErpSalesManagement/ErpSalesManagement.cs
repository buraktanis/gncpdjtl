using Core.DataTables;
using Core.Extensions;
using Core.Results;
using Entities.Erp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.DataTables.DatatablesJS;
using Core.Helpers;

namespace Business.ErpSalesManagement
{
    public class ErpSalesManagement : IErpSalesManagement
    {


        public DataTablesObjectResult GetCARIEKSTRE(DatatablesObject requestobj)
        {
            string query = string.Format(@" SELECT [CH ÜNVANI] unvan, TARIH tarih, ISLEMNO islemno, CEILING(BORÇ) borc, CEILING(ALACAK) alacak, CEILING(BAKIYE) bakiye, TEMSİLCİ temsilci, [CH KODU]carikodu, HAREKET_TURU hareketturu
            FROM ARY_XXX_CARI_EKSTRE_LOGOB2B  ");

            requestobj.dbtype = "SCSlogo";

            string privadewhere = "";
            var codu = requestobj.additionalvalues.ElementAt(0);

            if (!codu.Trim().isNull())
            {
                privadewhere = string.Format(" t.[carikodu]='{0}' ", codu);
            }


            if (!requestobj.additionalvalues.ElementAt(1).isNull() && !requestobj.additionalvalues.ElementAt(2).isNull())
            {
                if (!privadewhere.isNull())
                {
                    privadewhere = privadewhere + " AND ";
                }

                privadewhere = privadewhere + string.Format(" t.[tarih] BETWEEN '{0}' AND '{1}' ", requestobj.additionalvalues.ElementAt(1), requestobj.additionalvalues.ElementAt(2));
            }

            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }

        public DataResult<dynamic> Gettahsilatraporus(string tempsorgu)
        {
            try
            {

                var model = string.Format(@"SELECT  TEMSİLCİ temsilci, [CH KODU] kod, [CH UNVANI] unvan, GECEN_GUN, CASE WHEN GECEN_GUN<0 THEN 'Vade Geçmemiş' ELSE 'VADE GEÇMİŞ!' END [MODÜL]  
    , SUM(TUTAR) TUTAR FROM ARY_XXX_TAHSILAT WHERE {0} [TEMSİLCİ] NOT IN ('PASİF','BOŞ','HUKUK') GROUP BY [CH KODU], [CH UNVANI],
  TEMSİLCİ,GECEN_GUN, CASE WHEN GECEN_GUN<0 THEN 'Vade Geçmemiş' ELSE 'VADE GEÇMİŞ!' END ORDER
 BY [CH UNVANI], CASE WHEN GECEN_GUN<0 THEN 'Vade Geçmemiş' ELSE 'VADE GEÇMİŞ!' END, SUM(TUTAR) DESC", tempsorgu).GetDynamicQuery("SCSlogo");
                return new SuccessDataResult<dynamic>(model);
            }
            catch (Exception ex)
            {


                return new ErrorDataResult<dynamic>(ex.Message);
            }
        }



        public DataResult<dynamic> GetTips()
        {

            try
            {

                var model = string.Format(" SELECT DISTINCT  I.STGRPCODE tip FROm LG_017_ITEMS I Where I.STGRPCODE NOT IN ('','DEMİRBAŞ','DİĞER SATIŞLAR') ").GetDynamicQuery("SCSlogo");
                return new SuccessDataResult<dynamic>(model);
            }
            catch (Exception ex)
            {


                return new ErrorDataResult<dynamic>(ex.Message);
            }



        }

        public DataResult<List<Alis>> GetAlis(int gunselect)
        {

            try
            {

                var model = string.Format(@"SELECT urun.[Malzeme Grup Kodu] tur, ISNULL(urun.[Marka Kodu],'?') marka, urun.Model model, urun.[Malzeme Adı] urun, urun.[Malzeme Kodu] kod,
 ROUND(alim.[Satır Net Tutarı KDV Dahil]/alim.[Satır Miktarı],0) AS fiyat, alim.[Satır Miktarı] adet, CONVERT(DATE,alim.[Fatura Tarihi],23) tarih, alim.[Cari Kodu] cari, alim.[Cari Ünvanı] unvan, ISNULL(temsilci.[Satış Temsilcisi],'?') kisi 
FROM ARY_017_01_AYRINTILI_FATURA_RAPORU urun LEFT JOIN ARY_017_01_AYRINTILI_FATURA_RAPORU alim ON alim.[Malzeme Kodu]=urun.[Malzeme Kodu] LEFT JOIN ARY_017_CARI_MUSTER_TEMSILCI temsilci ON alim.[Cari Kodu]=temsilci.[Cari Kodu] 
WHERE urun.[Fatura Tarihi]>GetDate()-{0} AND urun.[Fatura Türü]='Satınalma Faturası' And alim.[Fatura Tarihi]>GetDate()-{0} AND alim.[Fatura Türü]='Satınalma Faturası' GROUP BY urun.[Malzeme Grup Kodu], urun.[Marka Kodu], urun.Model, urun.[Malzeme Kodu], urun.[Malzeme Adı], alim.[Satır Net Tutarı KDV Dahil], alim.[Satır Miktarı], alim.[Fatura Tarihi], alim.[Cari Kodu], alim.[Cari Ünvanı], temsilci.[Satış Temsilcisi] 
ORDER BY urun.[Malzeme Grup Kodu], urun.[Marka Kodu], urun.[Malzeme Adı], alim.[Fatura Tarihi] DESC, alim.[Satır Miktarı] DESC", gunselect).GetQuery<Alis>("SCSlogo");
                return new SuccessDataResult<List<Alis>>(model);
            }
            catch (Exception ex)
            {


                return new ErrorDataResult<List<Alis>>(ex.Message);
            }



        }

        
        public DataResult<List<Yillikkarnes>> Getyillikkarne(string yil , string ay )
        {

            try
            {
                var model = string.Format(@"SELECT CONVERT(INT,LEFT(fat.AY,2)) AS ay, ISNULL(SUM([Satır Miktarı]),0) AS adet, ROUND(ISNULL(SUM([Satır Net Tutarı KDV Dahil]),0),0) AS ciro, ROUND(ISNULL(SUM([Satış Kar]),0),0) AS kar, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Malzeme Kodu]) AS cesit, COUNT(DISTINCT [Marka Kodu]) AS marka, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat 
 WHERE fat.[Satış Temsilcisi]!='' AND MONTH(fat.[Fatura Tarihi])  IN({0}) AND fat.[Yıl]={1} AND fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme'  GROUP BY fat.[Satış Temsilcisi],CONVERT(INT,LEFT(fat.AY,2)) ORDER BY CONVERT(INT,LEFT(fat.AY,2))", ay, yil).GetQuery<Yillikkarnes>("SCSlogo");
                return new SuccessDataResult<List<Yillikkarnes>>(model);

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Yillikkarnes>>(ex.Message);

            }

        }
        

        public DataTablesObjectResult GetSalesList(DatatablesJS.DatatablesObject requestobj)
        {
            string query = string.Format(@" SELECT  fat.[Malzeme Grup Kodu] tip, fat.[Fatura No] faturano,  REPLACE(REPLACE(fat.[Cari Ünvanı],CHAR(13),''),CHAR(10),'') unvan, fat.[Satış Temsilcisi] temsilci, fat.[Fatura Tarihi] tarih,
     SUM(fat.[Satır Miktarı]) adet, fat.Vade vade, SUM(fat.[Satır Net Tutarı KDV Dahil]) tutar, SUM(CASE WHEN fat.[Çıkış Maliyeti KDV Dahil]>0 THEN (fat.[Satış Kar]) END) kar, CONVERT(INT,ISNULL(REPLACE(REPLACE(REPLACE(fat.Vade,'KREDİ KARTI',1),'PEŞİN',0),' GÜN VADE',''),0)) vadesi 
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat 
	WHERE fat.[Satır Miktarı]>0 AND fat.[Satır Türü]='Malzeme'
	GROUP BY fat.[Fatura No], fat.[Cari Kodu], fat.[Cari Ünvanı], fat.[Fatura Türü], fat.[İl], fat.[Satış Temsilcisi], fat.[Fatura Tarihi],fat.Vade,fat.[Malzeme Grup Kodu] ");
            requestobj.dbtype = "SCSlogo";
            string result = "";
            if (!requestobj.additionalvalues.ElementAt(0).isNull() && !requestobj.additionalvalues.ElementAt(0).isNull())
            {
                result = result + string.Format(" t.tarih BETWEEN '{0}' AND '{1}' ", requestobj.additionalvalues.ElementAt(0), requestobj.additionalvalues.ElementAt(1));
            }
            if (!requestobj.additionalvalues.ElementAt(2).isNull())
            {
                if (!result.isNull())
                    result = result + " AND ";

                result = result + string.Format(" t.tip='{0}' ", requestobj.additionalvalues.ElementAt(2));
            }
            if (!requestobj.additionalvalues.ElementAt(3).isNull())
            {
                if (!result.isNull())
                    result = result + " AND ";

                result = result + string.Format(" t.temsilci='{0}' ", requestobj.additionalvalues.ElementAt(3));
            }
            if (!requestobj.additionalvalues.ElementAt(4).isNull() && !requestobj.additionalvalues.ElementAt(5).isNull())
            {
                if (!result.isNull())
                    result = result + " AND ";

                result = result + string.Format(" t.adet>={0} AND  t.adet<={1}  ", requestobj.additionalvalues.ElementAt(4), requestobj.additionalvalues.ElementAt(5));
            }
            if (!requestobj.additionalvalues.ElementAt(6).isNull() && !requestobj.additionalvalues.ElementAt(7).isNull())
            {
                if (!result.isNull())
                    result = result + " AND ";

                result = result + string.Format(" t.tutar>={0} AND  t.tutar<={1}  ", requestobj.additionalvalues.ElementAt(6), requestobj.additionalvalues.ElementAt(7));
            }
            if (!requestobj.additionalvalues.ElementAt(8).isNull() && !requestobj.additionalvalues.ElementAt(9).isNull())
            {
                if (!result.isNull())
                    result = result + " AND ";

                result = result + string.Format(" t.kar>={0} AND  t.kar<={1}  ", requestobj.additionalvalues.ElementAt(8), requestobj.additionalvalues.ElementAt(9));
            }
            if (!requestobj.additionalvalues.ElementAt(10).isNull() && !requestobj.additionalvalues.ElementAt(11).isNull())
            {
                if (!result.isNull())
                    result = result + " AND ";

                result = result + string.Format(" t.vadesi>={0} AND  t.vadesi<={1}  ", requestobj.additionalvalues.ElementAt(10), requestobj.additionalvalues.ElementAt(11));
            }


            return new DataTablesObjectResult().getresults(requestobj, query, result);

        }

        public DataTablesObjectResult GetSalesDetail(DatatablesObject requestobj)
        {
            string query = string.Format(@" SELECT  fat.[Malzeme Adı] malzemeadi, fat.[Fatura No] faturano,fat.[Satır Miktarı] satirmiktari,fat.[Satır Toplamı] satirtoplami,fat.[Çıkış Maliyeti KDV Dahil] maliyet,fat.[Satır Türü],(fat.[Satış Kar])/fat.[Satır Miktarı] kar,CASE  WHEN fat.Vade ='PEŞİN' THEN CONVERT(INT,ISNULL(REPLACE(REPLACE(fat.Vade,'PEŞİN',0),' GÜN VADE',''),0))  WHEN fat.Vade ='KREDİ KARTI' THEN CONVERT(INT,ISNULL(REPLACE(REPLACE(fat.Vade,'KREDİ KARTI',1),' GÜN VADE',''),0))  ELSE 0 END AS vadesi FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat ");

            string result = string.Format(" t.faturano='{0}' ", requestobj.additionalvalues.ElementAt(0));
            requestobj.dbtype = "SCSlogo";

            return new DataTablesObjectResult().getresults(requestobj, query, result);
        }
        public DataTablesObjectResult GetStokmaliyet(DatatablesObject requestobj)
        {
            string query = string.Format(@"
SELECT m.[Malzeme Grup Kodu] tip, smtb.MalzRef id, m.[Malz_Kodu] kod, m.[Marka Kodu] marka, m.[Model] model_kodu, m.Malz_Adı model, ROUND(smtb.[MIN_Fiyat]*(1+(smtb.[KDV_Oranı]/100)),0) mal_min, ROUND(smtb.[MAX_Fiyat]*(1+(smtb.[KDV_Oranı]/100)),0) mal_max, ROUND(smtb.[Son_Alış_Fiyatı]*(1+(smtb.[KDV_Oranı]/100)),0) mal_son, ROUND(smtb.[Ortalama_Değer]*(1+(smtb.[KDV_Oranı]/100)),0) mal_ort, m.Fiili_Stok stok
 FROM ARY_017_MALZEME_LISTESI m  LEFT JOIN ARY_XXX_STOK_MALIYET_TB smtb ON m.[Malz_Kodu]= smtb.[Malzeme Kodu] Where m.Fiili_Stok!=0 AND  m.Malz_Adı NOT LIKE '%NUMUNE%' AND  m.Model NOT LIKE '%NUMUNE%' ");
            string result = "";
            if (!requestobj.additionalvalues.ElementAt(0).isNull())
            {

                result = result + string.Format(" t.tip='{0}' ", requestobj.additionalvalues.ElementAt(0));
            }

            requestobj.dbtype = "SCSlogo";

            return new DataTablesObjectResult().getresults(requestobj, query, result);
        }

        public DataResult<dynamic> Getbrands()
        {

            try
            {

                var model = string.Format(" SELECT [Marka Kodu] MARKA FROM ARY_017_01_AYRINTILI_FATURA_RAPORU WHERE [Marka Kodu] IS NOT NULL GROUP BY [Marka Kodu] ORDER BY [Marka Kodu] ").GetDynamicQuery("SCSlogo");
                return new SuccessDataResult<dynamic>(model);
            }
            catch (Exception ex)
            {


                return new ErrorDataResult<dynamic>(ex.Message);
            }


        }
        public DataResult<dynamic> fibabank()
        {
            try
            {

                var model = string.Format(@"SELECT dbo.AOSiparis.id, dbo.AOSiparis.Cariid, dbo.AOSiparis.Siparisno, dbo.AOSiparis.Toplamtutar, dbo.AOSiparis.Siparisdate, CONVERT(NVARCHAR(10), Siparisdate, 104) as DAT, dbo.AOSiparis.Status, dbo.AOSiparis.id AS Expr1, dbo.AOSiparis.Cariid AS Expr2,
                  dbo.AOSiparis.Siparisno AS Expr3, dbo.AOSiparis.Toplamtutar AS Expr4, dbo.AOSiparis.Status AS Expr5, dbo.AOSiparis.Siparisdate AS Expr8, dbo.LG_017_CLCARD.CODE, dbo.LG_017_CLCARD.DEFINITION_,
                  dbo.LG_017_CLCARD.LOGICALREF, dbo.ARY_017_CARI_MUSTER_TEMSILCI.[Satış Temsilcisi] AS SatisTemsilcisi, dbo.AOSiparis.Sepetjson, dbo.AOSiparis.TypeOdeme, dbo.AOSiparis.Token, dbo.AOSiparis.Isdirek, dbo.AOSiparis.hash,
                  dbo.AOSiparis.OdemeResponsejson, dbo.AOSiparisDetay.Siparisid, dbo.AOSiparisDetay.Malzid, dbo.AOSiparisDetay.Fiyat, dbo.AOSiparisDetay.FiyatLogo, dbo.AOSiparisDetay.Adi, dbo.AOSiparisDetay.Adet
FROM     dbo.AOSiparis INNER JOIN
                  dbo.LG_017_CLCARD ON dbo.AOSiparis.Cariid = dbo.LG_017_CLCARD.LOGICALREF INNER JOIN
                  dbo.ARY_017_CARI_MUSTER_TEMSILCI ON dbo.LG_017_CLCARD.CODE = dbo.ARY_017_CARI_MUSTER_TEMSILCI.[Cari Kodu] FULL OUTER JOIN
                  dbo.AOSiparisDetay ON dbo.AOSiparis.id = dbo.AOSiparisDetay.Siparisid
WHERE(dbo.AOSiparis.Status = 2)

ORDER BY dbo.AOSiparis.Siparisdate DESC").GetDynamicQuery("SCSlogo");
                return new SuccessDataResult<dynamic>(model);
            }
            catch (Exception ex)
            {


                return new ErrorDataResult<dynamic>(ex.Message);
            }
        }
        public DataResult<dynamic> kargo()
        {
            try
            {

                var model = string.Format(@"select * from ERP_Kargo_Takip").GetDynamicQuery("SCSlogo");
                return new SuccessDataResult<dynamic>(model);
            }
            catch (Exception ex)
            {


                return new ErrorDataResult<dynamic>(ex.Message);
            }
        }
        public DataResult<dynamic> cariekstre()
        {
            try
            {

                var model = string.Format(@"SELECT [Cari Kodu] kodu,[Cari Ünvanı] unvan, [Satış Temsilcisi] SatisTemsilcisi
  FROM ARY_017_CARI_MUSTER_TEMSILCI").GetDynamicQuery("SCSlogo");
                return new SuccessDataResult<dynamic>(model);
            }
            catch (Exception ex)
            {


                return new ErrorDataResult<dynamic>(ex.Message);
            }
        }
        public DataResult<dynamic> iade(string iade, string iade2)
        {






            try
            {
                string tempsorgu = string.Format(" Yıl ='{0}' ", iade);
                string tempsorgu2 = string.Format(" AY = '{0}' ", iade2);
                var model = string.Format(@"SELECT        dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.DONEM, dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.FATURA, dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.[Cari Kodu], 
                         dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.[Cari Ünvanı] AS cari_unvani, dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.Yıl, dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.AY, 
                         dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.[Fatura Türü] as fatura_turu, dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.[Satır Türü] AS satir_turu, dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.[Malzeme Kodu], 
                         dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.[Malzeme Adı], dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.[Satır Miktarı], abs(dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.[Satır Tutarı]) AS Satir_tutari, 
                         dbo.ARY_017_CARI_MUSTER_TEMSILCI.[Cari Kodu] AS Expr1, dbo.ARY_017_CARI_MUSTER_TEMSILCI.[Satış Temsilcisi] AS Satis_Temsilcisi
FROM            dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU INNER JOIN
                         dbo.ARY_017_CARI_MUSTER_TEMSILCI ON dbo.ARY_017_01_AYRINTILI_FATURA_RAPORU.[Cari Kodu] = dbo.ARY_017_CARI_MUSTER_TEMSILCI.[Cari Kodu]
WHERE   {0}", tempsorgu + "AND" + tempsorgu2).GetDynamicQuery("SCSlogo");

                return new SuccessDataResult<dynamic>(model);
            }
            catch (Exception ex)

            {
                return new ErrorDataResult<dynamic>(ex.Message);
            }
        }
        public DataResult<dynamic> ciro(string yıl, string ay)
        {
            try
            {
                string yılsorgu = string.Format(" dbo.ciroDetay.Yıl = '{0}' ", yıl);
                string aysorgu = string.Format(" dbo.ciroDetay.AY = '{0}' ", ay);

                var model = string.Format(@"SELECT dbo.ciroDetay.malzeme_grup_kodu, ((dbo.ciroDetay.Ciro)-ISNULL((dbo.iadeDetay2.iade),0)) as ciro, dbo.ciroDetay.Yıl, dbo.ciroDetay.AY, dbo.ciroDetay.[Slsman] as Satis_Temsilcisi,  ISNULL(abs(dbo.iadeDetay2.iade), 0) as İade, 
                           dbo.ciroDetay.Ciro as Net
FROM dbo.iadeDetay2 RIGHT JOIN
                         dbo.ciroDetay ON dbo.iadeDetay2.malzeme_grup_kodu = dbo.ciroDetay.malzeme_grup_kodu AND dbo.iadeDetay2.Yıl = dbo.ciroDetay.Yıl AND dbo.iadeDetay2.AY = dbo.ciroDetay.AY AND
                         dbo.iadeDetay2.Satis_Temsilcisi = dbo.ciroDetay.[Slsman] where {0}", yılsorgu + "and" + aysorgu).GetDynamicQuery("SCSlogo");

                return new SuccessDataResult<dynamic>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<dynamic>(ex.Message);
            }
        }
        public DataResult<dynamic> satisanaliz(string yıl)
        {
            try
            {
                string yılsorgu = string.Format(" Yıl = '{0}' ", yıl);
                var model = string.Format(@"SELECT [cariunvan]
      ,[Slsman]
      ,[Yıl]
      ,[ikinciel]
      ,[ikincielhamcihaz]
      ,[ikincielcihaz]
      ,[aksesuar]
      ,[cep_telefonu]
      ,[bilgisayar]
      ,[digerurun]
      ,[evaksesuari]
      ,[hoparlor]
      ,[tablet]
      ,[yenilenmiscihaz]
      ,[toplam]
  FROM [tiger].[dbo].[satisanaliztoplam] where {0}", yılsorgu).GetDynamicQuery("SCSlogo");
                return new SuccessDataResult<dynamic>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<dynamic>(ex.Message);
            }
        }
        public DataResult<List<Personelperformas>> kisiay()
        {
            try
            {

                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var model = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici,CASE (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]) WHEN 0 THEN 0 ELSE
               ((SELECT  COUNT(DISTINCT fsayisi.[Cari Kodu]) AS FIRMA FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fsayisi 
     	WHERE fsayisi.[Satır Net Tutarı KDV Dahil]<2000 AND fsayisi.[Fatura Türü]='Perakende Satış Faturası' AND 
	  fsayisi.[Satır Türü]='Malzeme' AND fsayisi.[Satış Temsilcisi]=fat.[Satış Temsilcisi] AND fsayisi.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' 
	 GROUP BY fsayisi.[Satış Temsilcisi])*100/
	 (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]))  END AS penetrasyon
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme'
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC", firstDayOfMonth.ToString(Formats.dateFormatsql), DateTime.Now.ToString(Formats.dateFormatsql)).GetQuery<Personelperformas>("SCSlogo");
                return new SuccessDataResult<List<Personelperformas>>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Personelperformas>>(ex.Message);
            }
        }
        public DataResult<List<Personelperformas>> kisigun()
        {
            try
            {
                var model = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici,CASE (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]) WHEN 0 THEN 0 ELSE
               ((SELECT  COUNT(DISTINCT fsayisi.[Cari Kodu]) AS FIRMA FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fsayisi 
     	WHERE fsayisi.[Satır Net Tutarı KDV Dahil]<2000 AND fsayisi.[Fatura Türü]='Perakende Satış Faturası' AND 
	  fsayisi.[Satır Türü]='Malzeme' AND fsayisi.[Satış Temsilcisi]=fat.[Satış Temsilcisi] AND fsayisi.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' 
	 GROUP BY fsayisi.[Satış Temsilcisi])*100/
	 (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]))  END AS penetrasyon
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme'
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC", DateTime.Now.ToString(Formats.dateFormatsql), DateTime.Now.ToString(Formats.dateFormatsql)).GetQuery<Personelperformas>("SCSlogo");
                return new SuccessDataResult<List<Personelperformas>>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Personelperformas>>(ex.Message);
            }

        }
        public DataResult<List<Brandperformas>> markaay()
        {
            try
            {
                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var model = string.Format(@"SELECT fat.[Marka Kodu] marka, SUM([Satır Miktarı])*(-1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(-1) AS ciro, 
              COUNT(DISTINCT [Model]) AS cesit, COUNT(DISTINCT fat.[Cari Kodu]) AS firma,
                    COUNT(DISTINCT fat.[İl]) AS sehir, COUNT(DISTINCT tem.[Satış Temsilcisi]) AS sorumlu FROM ARY_017_01_AYRINTILI_FATURA_RAPORU
         fat LEFT OUTER JOIN ARY_017_CARI_MUSTER_TEMSILCI tem ON tem.[Cari Kodu]=fat.[Cari Kodu]
                WHERE fat.FATURA='SATIŞ VE DAĞITIM' AND fat.[Satır Türü]='Malzeme' AND fat.[Marka Kodu] IS NOT NULL AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' GROUP BY fat.[Marka Kodu]
          ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(-1) DESC", firstDayOfMonth.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd")).GetQuery<Brandperformas>("SCSlogo");
                return new SuccessDataResult<List<Brandperformas>>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Brandperformas>>(ex.Message);
            }
        }

        public DataResult<List<Brandperformas>> markagun()
        {
            try
            {
                var model = string.Format(@"SELECT fat.[Marka Kodu] marka, SUM([Satır Miktarı])*(-1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(-1) AS ciro, 
              COUNT(DISTINCT [Model]) AS cesit, COUNT(DISTINCT fat.[Cari Kodu]) AS firma,
                    COUNT(DISTINCT fat.[İl]) AS sehir, COUNT(DISTINCT tem.[Satış Temsilcisi]) AS sorumlu FROM ARY_017_01_AYRINTILI_FATURA_RAPORU
         fat LEFT OUTER JOIN ARY_017_CARI_MUSTER_TEMSILCI tem ON tem.[Cari Kodu]=fat.[Cari Kodu]
                WHERE fat.FATURA='SATIŞ VE DAĞITIM' AND fat.[Satır Türü]='Malzeme' AND fat.[Marka Kodu] IS NOT NULL AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' GROUP BY fat.[Marka Kodu]
          ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(-1) DESC", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd")).GetQuery<Brandperformas>("SCSlogo");
                return new SuccessDataResult<List<Brandperformas>>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Brandperformas>>(ex.Message);
            }
        }

        

        public DataResult<List<Accountcodemapping>> GetAccountcodelist()
        {
            try
            {
                List<Accountcodemapping> model = new List<Accountcodemapping>();

                model = string.Format(@"SELECT * FROM accountcodemapping").GetQuery<Accountcodemapping>("SCSlogo");

                return new SuccessDataResult<List<Accountcodemapping>>(model);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<List<Accountcodemapping>>(ex.Message);
            }
        }

        

    }
}