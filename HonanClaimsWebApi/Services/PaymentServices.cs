using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HonanClaimsWebApi.Services
{
    public class PaymentServices
    {
        private const string claimReserveApi1 = "api/Payment/GetClaimReservePaymentAmount?claimId=";
        private const string claimReserveApi2 = "&reserveType=";
        private const string claimReserveApi3 = "&isGross=";

        public string GetClaimReservePaymentAmount(string claimId, string reserveType,bool isGross)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + claimReserveApi1 + claimId + claimReserveApi2 + reserveType + claimReserveApi3 + isGross);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            if (!string.IsNullOrEmpty(response))
                            {
                                return response;
                            }
                        }
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
