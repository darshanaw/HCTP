using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HonanClaimsWebApi.Models.SendEmail
{
    public class SendEmailRepo
    {
        public async Task<bool> SendEmail(List<HttpPostedFileBase> files, string userId, EmailModel model)
        {
            var result = false;
            var templist = new List<string>();

            //emil please remove this part
            templist.Add("ntfsrjsc@gmail.com");

            model.ToEmails = templist;
            model.CcEmails = templist;

            if (model != null)
            {
                string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

                string apiUrl = SiteUrl + "api/Claim/SendClaimEmail";
                using (HttpClient client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                        var toList = JsonConvert.SerializeObject(model.ToEmails);
                        var ccList = JsonConvert.SerializeObject(model.CcEmails);
                        var claimIdList = JsonConvert.SerializeObject(model.ClaimIds);

                        var content = new StringContent(toList, System.Text.Encoding.UTF8, "application/json");
                        var content2 = new StringContent(userId, System.Text.Encoding.UTF8, "application/json");
                        var content3 = new StringContent(model.emailBody, System.Text.Encoding.UTF8, "application/json");
                        var content4 = new StringContent(model.subject, System.Text.Encoding.UTF8, "application/json");
                        var content5 = new StringContent(ccList, System.Text.Encoding.UTF8, "application/json");
                        var content6 = new StringContent(claimIdList, System.Text.Encoding.UTF8, "application/json");

                        if (files.Count() > 0 && files != null)
                        {
                            foreach (var item in files)
                            {
                                formData.Add(CreateFileContent(item.InputStream, item.FileName, item.ContentType));
                            }
                        }


                        formData.Add(content, "toList");
                        formData.Add(content2, "userId");
                        formData.Add(content3, "emailBody");
                        formData.Add(content4, "subject");
                        formData.Add(content3, "ccList");
                        formData.Add(content4, "claimIdList");


                        HttpResponseMessage response = await client.PostAsync(apiUrl, formData);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            result = Convert.ToBoolean(data);

                        }

                    }

                }

            }
            return result;
        }

        private StreamContent CreateFileContent(Stream stream, string fileName, string contentType)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"files\"",
                FileName = "\"" + fileName + "\""
            }; // the extra quotes are key here
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return fileContent;
        }



        public async Task<List<PickListData>> GetActivityClaims(string userId)
        {
            List<PickListData> list = new List<PickListData>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Activity/GetClaimsForMyActivity?assignedToId=" + userId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PickListData>>(data);

                }
            }
            return list;
        }

        public async Task<List<PickListData>> GetContactList(List<string> ClaimList)
        {
            List<PickListData> list = new List<PickListData>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

            using (HttpClient client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    var claimList = JsonConvert.SerializeObject(ClaimList);
                    string apiUrl = SiteUrl + "api/Contact/GetClaimKeyContacts";
                    var content = new StringContent(claimList, System.Text.Encoding.UTF8, "application/json");
                    formData.Add(content, "claimIdList");

                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsync(apiUrl, formData);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PickListData>>(data);
                    }
                }

            }
            return list;
        }

    }
}
