using HonanClaimsWebApiAccess1.Models.ProtalLogingRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HonanClaimsWebApi.Models.Contact
{
    public class ContactAccountRepo
    {
        public async Task<bool> AddContactAccount(ContactAccountModel model, string userId)
        {
            var result = false;
            if (model != null)
            {
                string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

                var json = JsonConvert.SerializeObject(model);

                string apiUrl = SiteUrl + "api/Account/CreateContactAccount";
                using (HttpClient client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        var content2 = new StringContent(userId, System.Text.Encoding.UTF8, "application/json");
                        formData.Add(content, "contactAccountObjStr");
                        formData.Add(content2, "userId");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, formData);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            result = Convert.ToBoolean(data);

                        }

                    }

                }

                if(model.FromProtal && model.portalRegRequestId != null)
                {
                    TeamGetPortalRegistrationRepo teamGetPortalRegistrationRepo = new TeamGetPortalRegistrationRepo();
                   var rs = await teamGetPortalRegistrationRepo.TeamDiscardLoginRequest(model.portalRegRequestId);
                }

            }
            return result;
        }

        public async Task<List<Account>> GetAccount()
        {

            List<Account> list = new List<Account>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/General/GetAccountLookup?accountName=&type=";


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Account>>(data);

                }
            }
            return list;

        }

        public async Task<List<PickListData>> GetPickListData(string Name)
        {

            List<PickListData> list = new List<PickListData>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/General/GetPickListData?pickListName=" + Name;


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

        public async Task<List<PickListData>> GetManagerList(string accountManagerClick)
        {

            List<PickListData> list = new List<PickListData>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/General/GetAllUsers?userName="+ accountManagerClick;


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

    }
}
