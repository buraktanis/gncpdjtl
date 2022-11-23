using Core.Extensions;
using Core.Results;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Services.Logo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.DataTables.DatatablesJS;

namespace Business.LogoManagement
{
    public class LogoManagement : ILogoManagement
    {




        public DataResult<CLCARD> GetCLCardbyLOGICALREF(int LOGICALREF)
        {

            try
            {

                var model = string.Format("SELECT * FROM LG_017_CLCARD WHERE LOGICALREF='{0}'", LOGICALREF).GetQuery<CLCARD>("SCSlogo").FirstOrDefault();
                return new SuccessDataResult<CLCARD>(model);
            }
            catch (Exception ex)
            {


                return new ErrorDataResult<CLCARD>(ex.Message);
            }



        }
        public DataResult<CariBakiye> CariBakiyeByLogoId(string logoid)
        {
            try
            {
                var model = string.Format(@"SELECT [CH KODU] as Kode,
                 [CH ÜNVANI] as Unvan
                ,[BAKIYE] as Bakiye
                ,[TEMSİLCİ] as Temsilci
                 ,[LOGICALREF]
                FROM [tiger].[dbo].[ARY_XXX_CARI_BAKIYE] where [LOGICALREF]={0}", logoid).GetQuery<CariBakiye>("SCSlogo").FirstOrDefault();
                return new SuccessDataResult<CariBakiye>(model);
            }
            catch (Exception ex)
            {


                return new ErrorDataResult<CariBakiye>(ex.Message);
            }



        } 


        public DataResult<List<ARY_017_SİPARİŞ_HAREKETLERI>> ARY_017_SİPARİŞ_HAREKETLERI_List(List<string> carinos)
        {
            try
            {

                var model = string.Format(@"SELECT [Sipariş No] as CariCodu FROM[tiger].[dbo].[ARY_017_SİPARİŞ_HAREKETLERI] WHERE [Sipariş No] IN('{0}') AND İrsaliye_No IS NOT NULL", string.Join("','", carinos)).GetQuery<ARY_017_SİPARİŞ_HAREKETLERI>("SCSlogo");
                return new SuccessDataResult<List<ARY_017_SİPARİŞ_HAREKETLERI>>(model);
            }
            catch (Exception ex)
            {


                return new ErrorDataResult<List<ARY_017_SİPARİŞ_HAREKETLERI>>(new List<ARY_017_SİPARİŞ_HAREKETLERI>(),ex.Message);
            }

        }

        public DataResult<dynamic> SiparisDateyList(int LOGICALREF)
        {

            try
            {

                var model = string.Format(@"SELECT s.*,ml.Malz_Adı Adi ,ml.Malz_Kodu as Stockcode  FROM AOSiparisDetay s JOIN  ARY_017_MALZEME_LISTESI ml ON s.Malzid=ml.LOGICALREF  where s.Siparisid={0} ", LOGICALREF).GetQuery<dynamic>();
                return new SuccessDataResult<dynamic>(model);
            }
            catch (Exception ex)
            {


                return new ErrorDataResult<dynamic>(ex.Message);
            }



        }


        public DataResult<List<MALZEME_LISTESI>> Get_MALZEME_LISTESI_By_LOGICALREF(int logoid)
        {
            List<MALZEME_LISTESI> model = string.Format(@"SELECT   [Malz_Kodu]
                                                    ,[Malz_Adı]
                                                    ,[Birimi]
                                                    ,[Malzeme Grup Kodu]
                                                    ,[Marka Kodu]
                                                    ,[Model]
                                                    ,[AltGrup]
                                                    ,[BARKOD]
                                                    ,[Fiili_Stok]
                                                    ,[LOGICALREF]
                                                    ,[VAT]
                                                    ,[SPECODE]
                                                    ,[ACTIVE]
                                                     FROM [tiger].[dbo].[ARY_017_MALZEME_LISTESI]  Where LOGICALREF={0}", logoid).GetQuery<MALZEME_LISTESI>("SCSlogo");

            return new SuccessDataResult<List<MALZEME_LISTESI>>(model);
        }    
        public DataResult<List<Customer>> Carilist()
        {
            List<Customer> model = string.Format(@" SELECT   [Cari Kodu] AS CariKodu
                                                               ,[Cari Ünvanı] AS Name
                                                               ,[Satış Temsilcisi] As Slsman
                                                               ,[Bayi Tipi] As TypeCari
                                                               ,[Bölge] as Region
                                                         	  ,[İlçe] as Town
                                                               ,[Şehir]  as City
                                                               ,[LOGICALREF] as LogoId
                                                               ,[TELNRS1] as Phone
                                                               ,[TAXNR] as TaxAdministration
                                                               ,[INCHARGE] as PersonInCharge
                                                               ,[SPECODE3] as CustomerClass
                                                               ,[TCKNO] as TcIdentityNumber
                                                               ,[ACTIVE] as Status
                                                               ,[ADDR1] as Address
                                                               ,[SPECODE] as KindOf
                                                               ,[EMAILADDR] as Email
                                                               ,[POSTCODE] as PostCode
                                                           FROM [tiger].[dbo].[ARY_017_CARI_MUSTER_TEMSILCI] WHERE [TCKNO]=''  ", "").GetQuery<Customer>("SCSlogo");

            return new SuccessDataResult<List<Customer>>(model);
        }   
        
        public DataResult<List<Customer>> TaxTcControl(string no)
        {
            List<Customer> model = string.Format(@"SELECT [Cari Ünvanı]as Name,[Satış Temsilcisi] as Slsman  FROM ARY_017_CARI_MUSTER_TEMSILCI Where TAXNR='{0}' or TCKNO='{0}'", no).GetQuery<Customer>("SCSlogo");

            return new SuccessDataResult<List<Customer>>(model);
        }
           public DataResult<List<Customer>> TelControl(string no)
        {
            List<Customer> model = string.Format(@"SELECT [Cari Ünvanı]as Name,[Satış Temsilcisi] as Slsman  FROM ARY_017_CARI_MUSTER_TEMSILCI Where TELNRS1='{0}'", no).GetQuery<Customer>("SCSlogo");

            return new SuccessDataResult<List<Customer>>(model);
        }


        public DataResult<List<STOK_DURUM>> GetLogoStok()
        {
            List<STOK_DURUM> STOK_DURUMs = string.Format(@"  SELECT [Kod]
      
      ,[AD]
      ,[MarkaKodu]
      ,[MalzemeGrupKodu]
      
      ,[STOK]
      
      ,[ONAYLI_SIP]
      
      
      ,[mal_min]
      ,[mal_max]
      ,[mal_son]
      ,[mal_ort]
      
  FROM [tiger].[dbo].[stokmaliyetkontrol] where STOK > 0  ").GetQuery<STOK_DURUM>("SCSlogo");

            List<OrderItemBekleyenDto> bekliyenler = string.Format(@" SELECT i.LogoKod ,SUM(i.UnitsInStock) Stock  FROM OrderItems i JOIN Orders o ON i.OrderId=o.Id WHERE o.Status=0 GROUP BY i.LogoKod   ").GetQuery<OrderItemBekleyenDto>();


            foreach (var item in STOK_DURUMs)
            {
                var bekliyen = bekliyenler.FirstOrDefault(x => x.LogoKod == item.Kod);
                if (bekliyen != null)
                {
                    item.Bekleyen_STOK = bekliyen.Stock;
                }

            }

            return new SuccessDataResult<List<STOK_DURUM>>(STOK_DURUMs);
        }

        public DataTablesObjectResult GetStokDatatables(DatatablesObject requestobj)
        {
            string query = string.Format(@" SELECT  KOD
                               ,AD
                               ,[Marka Kodu] as MarkaKodu
                               ,[Malzeme Grup Kodu] MalzemeGrupKodu
                               ,Model
                               ,AltGrup
                               ,STOK
                               ,ONERI_SIP
                               ,ONAYLI_SIP
                               ,KLN_STOK as kalanstok,
                               GUNCEL_TARIH,
                               LOGICALREF
                           FROM [tiger].[dbo].[ARY_XXX_STOK_DURUM_4] where KLN_STOK>0 ");

            requestobj.dbtype = "SCSlogo";

            string privadewhere = "";

            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        } 
        public DataTablesObjectResult GetBigData(DatatablesObject requestobj)
        {
            string query = string.Format(@" SELECT [id]
                                             ,[Adi]
                                             ,[Telefon]
                                             ,[Il]
                                             ,[Musteritipi]
                                             ,[Atanan]
                                              FROM [tiger].[dbo].[BigData] ");

            requestobj.dbtype = "SCSlogo";

            string privadewhere = "";
            var tip = requestobj.additionalvalues.ElementAt(0);

            if (!tip.Trim().isNull())
            {
                privadewhere = string.Format(" Atanan='{0}' ", tip);
            }
            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }    
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


        public IResult bayiPerformans(string bastarih, string bistarih, string temsilci = "Tüm Kişiler", string tips = "")
        {
            string temsorgu = "";

            if (temsilci != "Tüm Kişiler")
            {
                temsorgu = string.Format(" AND fat.[Satış Temsilcisi]='{0}' ", temsilci);
            }
            if (bastarih.isNull() || bistarih.isNull())
            {
                bastarih = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                bistarih = DateTime.Now.ToString("yyyy-MM-dd");
            }

            dynamic model = string.Format(@"SELECT fat.[Satış Temsilcisi] SORUMLU, cari.[Cari Kodu] KODU, cari.[Cari Ünvanı] UNVAN, ISNULL(SUM([Satır Miktarı]),0) AS ADET, COUNT(DISTINCT fat.[Fatura No]) AS SIPARIS, ROUND(ISNULL(SUM(fat.[Satır Net Tutarı KDV Dahil]),0), 0) AS CIRO, COUNT(DISTINCT fat.[Model]) AS CESIT, ISNULL(FORMAT((SELECT MIN([Fatura Tarihi]) FROM ARY_XXX_AYRINTILI_SATIS_RAPORU WHERE [Cari Kodu]=cari.[Cari Kodu] and [Fatura Tarihi] BETWEEN '{0}' AND '{1}'),'yyyy-MM-dd'),'') ILK_SIPARIS,
 ISNULL(FORMAT((SELECT MAX([Fatura Tarihi]) FROM ARY_XXX_AYRINTILI_SATIS_RAPORU WHERE [Cari Kodu]=cari.[Cari Kodu] and [Fatura Tarihi] BETWEEN '{0}' AND '{1}'),'yyyy-MM-dd'),'') SON_SIPARIS FROM ARY_017_CARI_MUSTER_TEMSILCI cari LEFT JOIN ARY_XXX_AYRINTILI_SATIS_RAPORU fat ON fat.[Cari Kodu]=cari.[Cari Kodu] AND 
 fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' WHERE cari.[Cari Kodu]<>'' {2}  GROUP BY fat.[Satış Temsilcisi], cari.[Cari Kodu], cari.[Cari Ünvanı] Having  COUNT(DISTINCT fat.[Fatura No])>0 ORDER BY cari.[Cari Ünvanı]", bastarih, bistarih, temsorgu).GetDynamicQuery("SCSlogo");



            return new SuccessDataResult<dynamic>(model);
        }
    }
}
