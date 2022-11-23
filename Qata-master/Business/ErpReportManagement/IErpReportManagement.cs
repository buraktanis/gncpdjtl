using Core.Results;
using Entities.Erp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ErpReportManagement
{
    public  interface IErpReportManagement
    {
        public DataResult<CiroGunHaftaAyModel> GetCiroGunHaftaAy(string tempsorgu);

        public DataResult<List<Personelperformas>> Getdashboardrapor(string bastarih, string bistarih, string markaselect, string turselect);
        public DataResult<List<Personelperformas>> Getdashboardrapor2(string bastarih, string bistarih, string markaselect, string turselect);
        public DataResult<dynamic> bayiPerformans(string bastarih, string bistarih, string temsilci = "Tüm Kişiler", string tips = "");
        public DataResult<dynamic> personelPerformans(string bastarih, string bistarih, string temsilci = "Tüm Kişiler", string tips = "");
    }
}
