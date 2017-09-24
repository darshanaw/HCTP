using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Billing
{
    public class BillingSimpleRepo
    {
        public async Task<List<CustomerUserModel>> GetCustomerList(string UserId)
        {
            List<CustomerUserModel> list = new List<CustomerUserModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Billing/TeamGetBillableCustomers?userId=" + UserId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CustomerUserModel>>(data);
                }
            }
            return list;
        }


        public async Task<List<ServicesUserModel>> GetServeiceUserList()
        {
            List<ServicesUserModel> list = new List<ServicesUserModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Billing/TeamGetBillableLawyers?name=";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServicesUserModel>>(data);
                }
            }
            return list;
        }


        public async Task<List<BillingSimpleModel>> TeamGetMyBillableTimes(string showMe, string customerId, string serviceUserId, string serviceFromDate, string serviceToDate)
        {
            List<BillingSimpleModel> list = new List<BillingSimpleModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Billing/TeamGetMyBillableTimes?showMe="+showMe+"&customerId="+customerId+"&serviceUserId="+serviceUserId+"&serviceFromDate="+serviceFromDate+"&serviceToDate="+serviceToDate;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BillingSimpleModel>>(data);
                }
            }
            return list;
        }
    }
}
