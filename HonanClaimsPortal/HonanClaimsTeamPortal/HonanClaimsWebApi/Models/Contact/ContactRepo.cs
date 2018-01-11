using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Contact
{
    public class ContactRepo
    {
        public async Task<List<ContactModel>> GetAccounts(string AccountName, string ContactName)
        {
            List<ContactModel> list = new List<ContactModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Contact/GetContacts?contactName="+ContactName+"&accountName=" + AccountName ;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ContactModel>>(data);
                }
            }
            return list;
        }

        public async Task<ContactModel> GetAccount(string ContactId)
        {
            ContactModel list = new ContactModel();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Contact/GetContact?contactId=" + ContactId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactModel>(data);
                }
            }
            return list;
        }

        public async Task<bool> UpdateContact(ContactUpdateModel model,string userId)
        {
            var result = false;
            var templist = new List<string>();

            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

            string apiUrl = SiteUrl + "api/Contact/UpdateContact";
            using (HttpClient client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var toList = JsonConvert.SerializeObject(model);
                    var content = new StringContent(toList, System.Text.Encoding.UTF8, "application/json");
                    var content1 = new StringContent(userId, System.Text.Encoding.UTF8, "application/json");
                    formData.Add(content, "Contact");
                    formData.Add(content1, "userId");
                    HttpResponseMessage response = client.PostAsync(apiUrl, formData).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        result = Convert.ToBoolean(data);
                    }
                }
            }
            return result;
        }
    }
}
