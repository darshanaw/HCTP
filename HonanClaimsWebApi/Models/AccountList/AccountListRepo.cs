using HonanClaimsWebApi.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.AccountList
{
    public class AccountListRepo
    {
        public async Task<List<CommonModel>> GetPickListData()
        {
            List<CommonModel> list = new List<CommonModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/General/GetPickListData?pickListName=Account Type";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CommonModel>>(data);
                }
            }
            return list;
        }


        public async Task<List<AccountListModel>> GetAccounts(string AccountName, string Type)
        {
            List<AccountListModel> list = new List<AccountListModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Account/GetAccounts?accountName="+AccountName+"&type="+Type+"&pageSize="+10000+"&pageNo="+1;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AccountListModel>>(data);
                }
            }
            return list;
        }

        public async Task<AccountModel> GetAccountDetail(string AccountId)
        {
            AccountModel list = new AccountModel();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Account/GetAccount?accountId=" + AccountId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountModel>(data);
                }
            }
            return list;
        }



        public async Task<bool> UpdateAccount(AccountUpdateModel model,string userId)
        {
            var result = false;
            var templist = new List<string>();

            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

            string apiUrl = SiteUrl + "api/Account/UpdateAccount";
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
                    formData.Add(content, "Account");
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
