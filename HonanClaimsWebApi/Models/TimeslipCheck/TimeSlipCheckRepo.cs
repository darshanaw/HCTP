using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.TimeslipCheck
{
    public class TimeSlipCheckRepo
    {
        public async Task<List<TimeslipDataModel>> GetComboList(arealist area)
        {
            List<TimeslipDataModel> list = new List<TimeslipDataModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = string.Empty;
            if (area == arealist.ClaimTeam)
            {
                apiUrl = SiteUrl + "api/Billing/TeamGetCheckBillableTimeClaimTeams";
            }
            else if(area == arealist.Account)
            {
                apiUrl = SiteUrl + "api/Billing/TeamGetCheckBillableTimeAccounts";
               
            }
            else if (area == arealist.Claim)
            {
                apiUrl = SiteUrl + "api/Billing/TeamGetCheckBillableTimeClaimNos";

            }
            else
            {
                apiUrl = SiteUrl + "api/Billing/TeamGetCheckBillableTimeServiceByUsers";
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TimeslipDataModel>>(data);

                }
            }
            return list;
        }
    }
}
