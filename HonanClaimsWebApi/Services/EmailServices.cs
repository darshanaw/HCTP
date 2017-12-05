using HonanClaimsWebApi.Models.SendEmail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Services
{
    public class EmailServices
    {
        public async Task<List<EmailSimple>> GetAllClaimEmails(string claimId,string filterText)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/General/GetClaimEmails?claimId=" + claimId + "&filterText=" + filterText);
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

        public EmailSignature GetEmailSignature(string userId)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/General/GetEmailSignature?userId=" + userId);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            return JsonConvert.DeserializeObject<EmailSignature>(responseReader.ReadToEnd());
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

        public async Task<bool> InsertUpdateEmailSignature(EmailSignature emailSignature)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        var jsonString = JsonConvert.SerializeObject(emailSignature);
                        var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                        formData.Add(content, "EmailSignature");

                        var postResult = await client.PostAsync(ConfigurationManager.AppSettings["apiurl"] + "api/General/InsertUpdateEmailSignature", formData);
                        string resultContent = await postResult.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<bool>(resultContent);
                    }
                }

            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
