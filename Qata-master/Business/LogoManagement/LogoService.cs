using Core.Helpers;
using Core.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Entities.Services.Logo;
using Entities.Concrete;
using Core.Extensions;

namespace Business.LogoManagement
{
    public  class LogoService: ILogoService
    {




        public IResult AddOrderLogo(Order order,List<OrderItem> orderItems,Customer customer, CustomerConsignment consignment )
        {

            string tarih = DateTime.Now.ToString(Formats.dateFormatlogo);

            var TRANSACTION = new TRANSACTION();

            foreach (var item in orderItems)
            {
                var items = new ITEM
                {
                    TYPE = item.ItemType.ToString(),//1
                    MASTER_CODE = item.LogoKod,//1
                    QUANTITY = item.UnitsInStock.ToString(),//1
                    TOTAL = Convert.ToString(item.UnitsInStock * item.UnitPrice).Replace(',', '.'),
                    VAT_AMOUNT = Convert.ToString((item.UnitsInStock * item.UnitPrice) / 100).Replace(',', '.'),
                    VAT_BASE = Convert.ToString((item.UnitsInStock * item.UnitPrice) - ((item.UnitsInStock * item.UnitPrice) / 100)).Replace(',', '.'),
                    VAT_RATE = item.Vat.ToString(),
                    UNIT_CONV1 = "1",
                    UNIT_CONV2 = "1",
                    VAT_INCLUDED = "1",
                    UNIT_CODE = "ADET"

                };

                TRANSACTION.items.Add(items);
            }

            var invoice = new INVOICE
            {
                INTERNAL_REFERENCE = "0",
                NUMBER = "~",
                DATE = tarih,
                TIME = "0",
                ARP_CODE = customer.CariKodu,
                PAYMENT_CODE =order.TypeOfPayment,
                AUXIL_CODE = "BAYI",
                DOC_NUMBER = order.Id.ToString(),
                CURRSEL_TOTAL = "1",
                PAYDEFREF = "1",
                SHIPMENT_TYPE = order.DeliveryType,
                DOC_TRACK_NR = order.DeliveryType,
                DOC_TRACKING_NR = order.DeliveryType,
                ORDER_STATUS = "4",
                SALESMAN_CODE =order.Slsman,
                EINVOICE_TYPE = "0",
                EINVOICE = "2",
                EINVOICE_TURETPRICESTR = "Sıfır TL",
                EARCHIVEDETR_SENDMOD = "1",
                EARCHIVEDETR_INTPAYMENTTYPE = "4",
                EARCHIVEDETR_ISPERCURR = "4",
                EARCHIVEDETR_TCKNO = "tc",
                EARCHIVEDETR_NAME =customer.Name,
                EARCHIVEDETR_SURNAME = "",
                EARCHIVEDETR_ADDR1 = consignment.Address,
                EARCHIVEDETR_CITY = consignment.CityId,
                EARCHIVEDETR_CITYCODE = "",
                EARCHIVEDETR_COUNTRYCODE = "TR",
                EARCHIVEDETR_COUNTRY = "TÜRKİYE",
                EARCHIVEDETR_TOWN = consignment.TownId,
                EARCHIVEDETR_TELNRS1 = customer.Phone,
                TRANSACTIONS = TRANSACTION
            };
            var resultt = JsonConvert.SerializeObject(invoice);

            string resp = HttpPost("http://78.188.219.81:32001/api/v1/salesOrders", resultt, getAccessToken());

            INVOICE respobj = JsonConvert.DeserializeObject<INVOICE>(resp);
            if (respobj.INTERNAL_REFERENCE != null)
            {
                string salesOrderjson = HttpGet(string.Format("http://78.188.219.81:32001/api/v1/salesOrders/{0}",respobj.INTERNAL_REFERENCE), getAccessToken());

                INVOICE salesOrder = JsonConvert.DeserializeObject<INVOICE>(salesOrderjson);

                return new SuccessResult(salesOrder.NUMBER);

            }

            return new ErrorResult(resp);
        }

        public IResult CLCARDAdd(Customer customer)
        {

            if (customer.Phone1.isNull())
            {
                customer.Phone1 = customer.Phone;
            }

            if (customer.Type == 2)
            {
                customer.Type = 1;
            }
            var carics = new CLCARD
            {
                ACCOUNT_TYPE = "3",
                RECORD_STATUS = customer.Status.ToString(),
            //    CODE = "120.01.0000",
                GL_CODE = "",
                TITLE = customer.Name,
                ADDRESS1 = customer.Address,
                ADDRESS2 = customer.Town+"/"+customer.City,
                TOWN_CODE = "0",
                TOWN = customer.Town,
                CITY_CODE = "0",
                CITY = customer.City,
                COUNTRY_CODE = "TR",
                COUNTRY = "TÜRKİYE",
                E_MAIL = customer.Email,
                TELEPHONE1 = customer.Phone,
                TELEPHONE2 = customer.Phone1,
                CONTACT=customer.PersonInCharge,
                TAX_ID=customer.TaxAdministration,
                TAX_OFFICE=customer.TaxInfo,
                TCKNO = customer.TcIdentityNumber,
                AUXIL_CODE=customer.KindOf,
                AUXIL_CODE2 = customer.Region,
                AUXIL_CODE3=customer.CustomerClass,
                POSTAL_CODE=customer.PostCode,
                EXT_ACC_FLAGS = "0",
                ORD_DAY = "0",
                INVOICE_PRNT_CNT = "0",
                PURCHBRWS = "1",
                SALESBRWS = "1",
                IMPBRWS = "1",
                EXPBRWS = "1",
                FINBRWS = "1",
                PERSCOMPANY = customer.Type.ToString(),
                ACCEPT_EINV = "1",
                PROFILE_ID = "1",
                NAME = customer.PersonInCharge,
                SURNAME = customer.PersonInChargeSurName,
                FACTORY_DIV_NR = "1",
                EINV_CUSTOMS = "1"
            };


            var resultt = JsonConvert.SerializeObject(carics);




            string resp = HttpPost("http://78.188.219.81:32001//api/v1/ARPs", resultt, getAccessToken());

            CLCARD respobj = JsonConvert.DeserializeObject<CLCARD>(resp);
            if (respobj.INTERNAL_REFERENCE != null)
            {


                return new SuccessResult(respobj.INTERNAL_REFERENCE);


            }

            return new ErrorResult(resp);
        }  
        
        public IResult ArpShipmentLocationsPost(CustomerConsignment consignment )
        {

            var Location = new ArpShipmentLocation()
            {
                ARP_CODE = consignment.CariKodu,
                CITY = consignment.CityId,
                TOWN = consignment.TownId,
                CLIENTREF = consignment.CLIENTREF.ToString(),
                ADDRESS1 = consignment.Address,
                ADDRESS2 = consignment.TownId + "/" + consignment.CityId,
                CODE=consignment.Code,
                TITLE=consignment.Name
            
            
            };


            var resultt = JsonConvert.SerializeObject(Location);




            string resp = HttpPost("http://78.188.219.81:32001//api/v1/ArpShipmentLocations", resultt, getAccessToken());

            ArpShipmentLocation respobj = JsonConvert.DeserializeObject<ArpShipmentLocation>(resp);
            if (respobj.INTERNAL_REFERENCE != null)
            {
                return new SuccessResult(respobj.INTERNAL_REFERENCE);

            }

            return new ErrorResult();
        }
        
        public IResult Getsalesmen()
        {
            string car = HttpGet("http://78.188.219.81:32001//api/v1/ARPs/2", getAccessToken());


            string salesmen = HttpGet("http://78.188.219.81:32001//api/v1/salesmen", getAccessToken());

            // string customersOfSalesmen = HttpGet("http://78.188.219.81:32001//api/v1/customersOfSalesmen", getAccessToken());




            string CL_LIST = HttpGet("http://78.188.219.81:32001/api/v1/salesmen/1/CL_LIST", getAccessToken());
      




            //try
            //{
            //    dynamic json = JsonConvert.DeserializeObject(resp);
            //    result.message = json.INTERNAL_REFERENCE;

            //    if (json.INTERNAL_REFERENCE == null)
            //    {
            //        result.message = resp;
            //        result.status = false;
            //    }
            //    else
            //    {
            //        result.status = true;
            //    }


            //}
            //catch (Exception e)
            //{
            //    result.message = e.Message;
            //    result.status = false;
            //}

            return new SuccessResult();
        }
        
        
        
        public IDataResult<CLCARD> GetCLCARDByLogoId(int id)
        {
            string resp = HttpGet(string.Format("http://78.188.219.81:32001//api/v1/ARPs/{0}",id), getAccessToken());

            CLCARD respobj = JsonConvert.DeserializeObject<CLCARD>(resp);
            if (respobj.INTERNAL_REFERENCE != null)
            {
                return new SuccessDataResult<CLCARD>(respobj);

            }

            return new ErrorDataResult<CLCARD>();
        } 
        public IDataResult<List<CL_LIST>> GetCL_LISTById(int id)
        {
            dynamic resp = HttpGet(string.Format("http://78.188.219.81:32001/api/v1/salesmen/{0}/CL_LIST", id), getAccessToken());

            List<CL_LIST> respobj = JsonConvert.DeserializeObject<List<CL_LIST>>(resp.items);
            if (respobj.Count != 0)
            {
                return new SuccessDataResult<List<CL_LIST>>(respobj);

            }

            return new ErrorDataResult<List<CL_LIST>>();
        }
       public IDataResult<CL_LIST> GetCL_LISTByPost(int id)
          {
            var model = new CL_LIST()
            {
                SALESMANREF= id.ToString(),
                CLIENTREF= "7399"
            };
            var modeljson = JsonConvert.SerializeObject(model);


            dynamic resp = HttpPost(string.Format("http://78.188.219.81:32001/api/v1/salesmen/{0}/CL_LIST", id), modeljson, getAccessToken());

            CL_LIST respobj = JsonConvert.DeserializeObject<CL_LIST>(resp);
            if (respobj != null)
            {
                return new SuccessDataResult<CL_LIST>(respobj);

            }

            return new ErrorDataResult<CL_LIST>();
        }




        public static long ConvertToUnixTimeStamp(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public static string getAccessToken(string url = "http://78.188.219.81:32001/api/v1/token", string userName = "GENCPA", string password = "Ny2erd14", string firmNr = "17")
        {
            string accessToken = "";
            try
            {
                HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                req.Method = "POST";
                req.ContentType = "application/json";
                req.Accept = "application/json";
                req.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("GUNESELEKTRONIK" + ":" + "/CLskRmWMvSUlpR8YfiGYew0W4UfBuaJ0oTev6BCvhs=")));
                byte[] formData = UTF8Encoding.UTF8.GetBytes("grant_type=password"
                    + "&username=" + HttpUtility.UrlEncode(userName)   // bu satır 04.05.2020 tarihinde değiştirildi
                    + "&firmno=" + firmNr
                    + "&password=" + HttpUtility.UrlEncode(password)); // bu satır 04.05.2020 tarihinde değiştirildi
                req.ContentLength = formData.Length;
                using (Stream post = req.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }
                string result = null;
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }
                dynamic j = JsonConvert.DeserializeObject(result);
                accessToken = j.access_token;
            }
            catch (Exception e)
            {
                accessToken = e.Message;
            }

            return accessToken;
        }

        public static string HttpPost(string url, string param, string accessToken)
        {
            string result = null;

            try
            {
                HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                req.Method = "POST";
                req.ContentType = "application/json";
                req.Accept = "application/json";
                req.Headers.Add("Authorization", "Bearer  " + accessToken);

                byte[] formData = UTF8Encoding.UTF8.GetBytes(param.ToString());
                req.ContentLength = formData.Length;

                using (Stream post = req.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }

                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }
            }
            catch (WebException webEx)
            {
                var response = ((HttpWebResponse)webEx.Response);
                StreamReader content = new StreamReader(response.GetResponseStream());
                result = content.ReadToEnd();
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }

        public static string HttpGet(string url, string accessToken)
        {
            string result = null;

            try
            {
                HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                req.Method = "GET";
                req.ContentType = "application/json";
                req.Accept = "application/json";
                req.Headers.Add("Authorization", "Bearer  " + accessToken);



                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }
            }
            catch (WebException webEx)
            {
                var response = ((HttpWebResponse)webEx.Response);
                StreamReader content = new StreamReader(response.GetResponseStream());
                result = content.ReadToEnd();
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }

        public IResult GetCLCARD()
        {
            throw new NotImplementedException();
        }
    }
}
