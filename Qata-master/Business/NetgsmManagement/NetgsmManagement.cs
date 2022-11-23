using Core.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace Business.NetgsmManagement
{
   public class NetgsmManagement: INetgsmManagement
    {
        public IDataResult<dynamic> test(int id)
        {
            //string resp = HttpGet(string.Format("http://78.188.219.81:32001//api/v1/ARPs/{0}", id), getAccessToken());

            //CLCARD respobj = JsonConvert.DeserializeObject<CLCARD>(resp);
            //if (respobj.INTERNAL_REFERENCE != null)
            //{
            //    return new SuccessDataResult<CLCARD>(respobj);

            //}
            string html = string.Empty;
            string url = @"https://api.netgsm.com.tr/sms/report/?usercode=5322126669&password=535D34&bulkid=8503050900&type=0&status=100&version=2";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Console.WriteLine(html);


                            XmlDocument soapEnvelopeXml = new XmlDocument();
            var xmlStr = string.Format(@"@<?xml version=""1.0""?>
    <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
                 xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
      xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
        <SOAP-ENV:Body>
            <ns3:raporV3 xmlns:ns3=""http://sms/"">
            <username>Kullanıcıadı</username>
            <password>Şifre</password>
            <bulkid>883557487</bulkid>
        </ns3:raporV3>
        </SOAP-ENV:Body>
    </SOAP-ENV:Envelope>");
            /*
            Servisten dönen yanıt (version=2)

                53545	0505550000000	0	10	1	01.05.2014 22:24:00	102

            Servisten dönen yanıt (version=0 veya version=1)

                53545	0505550000000	0

            Servisten dönen yanıt (version=3)

                53546	0	103

            açıklamalar :  

            53545 -> GörevID  
            0505550000000 -> Cep Telefon   
            0 -> Mesaj Durumu   
            10 -> Operatör Kodu   
            1 -> Mesaj Boyu  
            01.05.2014 22:24:00 -> İletim Tarihi   
            102 -> Dönen Hata Kodu           
            */
            XMLPOST("https://api.netgsm.com.tr/sms/edit", xmlStr);
            return new ErrorDataResult<dynamic>();
        }

        private string XMLPOST(string PostAddress, string xmlData)
        {
            try
            {
                WebClient wUpload = new WebClient();
                HttpWebRequest request = WebRequest.Create(PostAddress) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
            catch
            {
                return "-1";
            }
        }
    }
}
