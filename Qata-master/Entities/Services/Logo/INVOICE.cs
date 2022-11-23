using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Services.Logo
{
  public  class INVOICE
    {
        public string INTERNAL_REFERENCE { get; set; }
        public string NUMBER { get; set; }
        public string DATE { get; set; }
        public string TIME { get; set; }
        public string ARP_CODE { get; set; }
        public string PAYMENT_CODE { get; set; }
        public string AUXIL_CODE { get; set; }
        public string DOC_NUMBER { get; set; }
        public string CURRSEL_TOTAL { get; set; }
        public string PAYDEFREF { get; set; }
        public string SHIPMENT_TYPE { get; set; }
        public string DOC_TRACK_NR { get; set; }
        public string DOC_TRACKING_NR { get; set; }
        public string ORDER_STATUS { get; set; }
        public string SALESMAN_CODE { get; set; }
        public string EINVOICE_TYPE { get; set; }
        public string EINVOICE { get; set; }
        public string EINVOICE_TURETPRICESTR { get; set; }
        public string EARCHIVEDETR_SENDMOD { get; set; }
        public string EARCHIVEDETR_INTPAYMENTTYPE { get; set; }
        public string EARCHIVEDETR_ISPERCURR { get; set; }
        public string EARCHIVEDETR_TCKNO { get; set; }
        public string EARCHIVEDETR_NAME { get; set; }
        public string EARCHIVEDETR_SURNAME { get; set; }
        public string EARCHIVEDETR_ADDR1 { get; set; }
        public string EARCHIVEDETR_CITY { get; set; }
        public string EARCHIVEDETR_CITYCODE { get; set; }
        public string EARCHIVEDETR_COUNTRYCODE { get; set; }
        public string EARCHIVEDETR_COUNTRY { get; set; }
        public string EARCHIVEDETR_TOWNCODE { get; set; }
        public string EARCHIVEDETR_TOWN { get; set; }
        public string EARCHIVEDETR_TELNRS1 { get; set; }
        public string EARCHIVEDETR_EMAILADDR { get; set; }
        public TRANSACTION TRANSACTIONS { get; set; }

        public INVOICE()
        {
            TRANSACTIONS = new TRANSACTION();
        }



    }
}
