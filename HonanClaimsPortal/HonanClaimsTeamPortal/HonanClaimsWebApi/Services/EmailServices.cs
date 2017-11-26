using HonanClaimsWebApi.Models.SendEmail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Services
{
    public class EmailServices
    {
        public async Task<List<EmailSimple>> GetAllClaimEmails(string claimId)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/General/GetClaimEmails?claimId=" + claimId);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            return JsonConvert.DeserializeObject<List<EmailSimple>>(responseReader.ReadToEnd());
                        }
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<EmailSimple> GetClaimEmail(string emailId,bool withAttachments)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/General/GetClaimEmail?emailId=" + emailId + "&withAttachments=" + withAttachments);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            return JsonConvert.DeserializeObject<EmailSimple>(responseReader.ReadToEnd());
                        }
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
