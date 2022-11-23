using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Core.Services.CagriSms
{
    public  class CagrismsService
    {
        //           var a = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", "5322383343", text);
        public string sendmesaj(string username, string password, string title, string phoneNumber, string stringcontent)
        {

            var obj = new smsModel.Root
            {
                header = new smsModel.Header
                {
                    username = username,
                    password = password

                },
                body = new smsModel.Body
                {
                    messages = new List<smsModel.Message>(){new smsModel.Message {
                        phoneNumber = phoneNumber,
                        content=stringcontent
                    }},
                    title = title
                }

            };


            var resultt = JsonConvert.SerializeObject(obj);

            string url = string.Format("http://api.cagrisms.com/api/SMS");

            string result;

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";


                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(resultt);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
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


        public class smsModel
        {
            public class Header
            {
                public string username { get; set; }
                public string password { get; set; }
            }

            public class Message
            {
                public string phoneNumber { get; set; }
                public string content { get; set; }
            }

            public class Body
            {
                public List<Message> messages { get; set; }
                public string title { get; set; }
            }

            public class Root
            {
                public Header header { get; set; }
                public Body body { get; set; }
            }




        }

    }
}
