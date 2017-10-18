using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.ClaimList
{
    public class ClaimListRepo
    {
        public async Task<List<ClaimListModel>> getClaimList(string userId, bool myclaimsOnly, bool isopenClaim, string claimType, string searchText, string cutomerId)
        {
            List<ClaimListModel> list = new List<ClaimListModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            searchText = searchText == "null" ? string.Empty : searchText;
            cutomerId = cutomerId == "null" ? string.Empty : cutomerId;
            string apiUrl = SiteUrl + "api/Claim/TeamGetMyClaimList?assignedToId=" + userId + "&myClaimsOnly=" + myclaimsOnly + "&isOpenClaims=" + isopenClaim + "&claimType=" + claimType + "&searchText=" + searchText + "&customerId=" + cutomerId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClaimListModel>>(data);

                }
            }
            return list;
        }

        public async Task<List<CustomerModel>> GetCustomerList(string userId)
        {
            List<CustomerModel> list = new List<CustomerModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetClaimCustomersByUser?userId=" + userId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CustomerModel>>(data);

                }
            }
            return list;
        }

        public async Task<List<ClaimListModel>> getClaimListSearchTextOnly(string userId,string searchText)
        {
            List<ClaimListModel> list = new List<ClaimListModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetMyClaimListSearchAllOnly?assignedToId=" + userId + "&searchText=" + searchText;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClaimListModel>>(data);

                }
            }
            return list;
        }

    }
}
