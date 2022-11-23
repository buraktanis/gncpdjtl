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
    public class GTSController : Controller
    {
        public IActionResult gtstemsilci()
        {
            var model = new sozlesmeModel();

            model.model1 = string.Format(@"SELECT  (SELECT COUNT(1) FROM AOMusteri m JOIN 
ARY_017_CARI_MUSTER_TEMSILCI mt on m.LOGICALREF = mt.LOGICALREF WHERE mt.[Satış Temsilcisi] = temp.Temsilcisi AND m.IsApprove = 1) as toplamonyli,
(SELECT COUNT(1) FROM AOMusteri m JOIN
ARY_017_CARI_MUSTER_TEMSILCI mt on m.LOGICALREF = mt.LOGICALREF WHERE mt.[Satış Temsilcisi]= temp.Temsilcisi AND m.IsApprove = 0)  as toplamret,
temp.Temsilcisi ,COUNT(temp.onayli) toplambayi,SUM(temp.tislem) topalmislem,SUM(temp.red) as red,SUM(temp.onayli) onayli,SUM(temp.beklemekte) beklemekte FROM
                    (SELECT tem.[Satış Temsilcisi] Temsilcisi,
                      (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid = cl.LOGICALREF) as tislem,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid = cl.LOGICALREF AND s.Status = 0) as red,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid = cl.LOGICALREF AND s.Status = 1) as beklemekte,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid = cl.LOGICALREF AND s.Status = 2) as onayli,m.LOGICALREF
                         from LG_017_CLCARD cl  JOIN AOMusteri m ON cl.LOGICALREF = m.LOGICALREF

                     JOIN ARY_017_CARI_MUSTER_TEMSILCI tem ON cl.LOGICALREF = tem.LOGICALREF ) as temp GROUP BY temp.Temsilcisi ").GetDynamicQuery("SCSlogo");


            model.model2 = string.Format(@"SELECT tem.[Satış Temsilcisi] Temsilcisi ,cl.DEFINITION_ Adi,cl.TELNRS1 Telefon , cl.LOGICALREF ,
CASE m.Isactive WHEN 0 THEN 'Pasif' 
			    WHEN 1 THEN 'Aktif' end as active
,
CASE m.IsApprove WHEN 0 THEN 'Red'
WHEN 1 THEN 'Onayli' end as approve
,m.Approvetext ,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF) as tislem,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF AND s.Status=0) as red,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF AND s.Status=1) as beklemekte,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF AND s.Status=2) as onayli
                     from LG_017_CLCARD cl  JOIN AOMusteri m ON cl.LOGICALREF=m.LOGICALREF 
					 JOIN ARY_017_CARI_MUSTER_TEMSILCI tem ON cl.LOGICALREF=tem.LOGICALREF").GetDynamicQuery("SCSlogo");

            return View(model);
        }
        public IActionResult gtsiparis()
        {
            return View();
        }

        [Route("{controller}/datatables")]
        [HttpPost]
        public IActionResult datatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            requestobj.dbtype = "SCSlogo";

            try
            {
                //var results = await _demoService.GetDataAsync(param);
                return new JsonResult(new DatatablesJS.DataTablesObjectResult().getresults(requestobj));
                //return new JsonResult(new { error = "aaa" });
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }

        }
    }
}
