using HonanClaimsWebApi.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Billing
{
    public class BillingRepo
    {
        
       public async Task<List<CommonModel>> GetTeamGetBillableLawyers(string Name)
        {
            List<CommonModel> list = new List<CommonModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Billing/TeamGetBillableLawyers?name="+Name;
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


        
        public async Task<List<CommonModel>> TeamGetClaimNosAssigned(string Name)
        {
            List<CommonModel> list = new List<CommonModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetClaimNosAssigned?userId="+Name+"&claimRefNumber=";
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


        public async Task<List<CommonModel>> TeamGetClaimActivities(string Claimsid)
        {
            List<CommonModel> list = new List<CommonModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetClaimActivities?claimId="+Claimsid+"&activity=";
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


        public async Task<List<CommonModel>> TeamGetPolicyNoForClaim(string Claimsid)
        {
            List<CommonModel> list = new List<CommonModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetPolicyNoForClaim?claimId=" + Claimsid;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var templist = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonModel>(data);
                    list.Add(templist);
                }
            }
            return list;
        }


        public async Task<CommonModel> TeamGetCustomerForClaim(string Claimsid)
        {
            CommonModel list = new CommonModel();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetCustomerForClaim?claimId=" + Claimsid;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonModel>(data);

                }
            }
            return list;
        }

        
        public async Task<bool> TeamInsertTimeslip(BillingModel model)
        {

            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = string.Empty;

            apiUrl = SiteUrl + "api/Billing/TeamInsertTimeslip?billing=&userId=";
            
            var json = JsonConvert.SerializeObject(model);

            using (var client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    var jsonString = JsonConvert.SerializeObject(model);
                    var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                    formData.Add(content, "PortalAdminDetail", "PortalAdminDetail");

                    var result = await client.PostAsync(apiUrl, formData);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    return Convert.ToBoolean(resultContent);
                }
            }
        }
    }
}
