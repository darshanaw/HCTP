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
            else if (area == arealist.Account)
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

        public async Task<List<TimeSlipGridDetailModel>> GetTimeSlipGridData(string claimTeam, string accountId, string serviceBy, string claimId, string fromDate, string toDate)
        {
            List<TimeSlipGridDetailModel> list = new List<TimeSlipGridDetailModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            claimTeam = claimTeam == "null" ? string.Empty : claimTeam;
            accountId = accountId == "null" ? string.Empty : accountId;
            serviceBy = serviceBy == "null" ? string.Empty : serviceBy;
            claimId = claimId == "null" ? string.Empty : claimId;
            fromDate = fromDate == "null" ? string.Empty : fromDate;
            toDate = toDate == "null" ? string.Empty : toDate;

            string apiUrl = SiteUrl + "api/Billing/TeamGetCheckBillableTimes?claimTeam=" + claimTeam + "&accountId=" + accountId + "&serviceBy=" + serviceBy + "&claimId=" + claimId + "&fromDate=" + fromDate + "&toDate=" + toDate;


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TimeSlipGridDetailModel>>(data);

                }
            }
            return list;
        }

        public async Task<BillingTimeRecordModel> GetBillingRecords(string billingId)
        {
            BillingTimeRecordModel list = new BillingTimeRecordModel();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Billing/TeamGetBillableTimeRecord?billingId=" + billingId;


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<BillingTimeRecordModel>(data);

                }
            }
            return list;
        }

        public async Task<bool> MarkAsChecked(string billingId, string userId)
        {
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Billing/TeamMarkBillableTimeAsChecked?userId=" + userId + "&billingId=" + billingId;
            bool result = true;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    result = Convert.ToBoolean(data);
                }
            }
            return result;
        }


        public async Task<bool> MarkAsNonBillable(string billingId, string userId)
        {
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Billing/TeamMarkBillableTimeAsNonBillable?userId=" + userId + "&billingId=" + billingId;
            bool result = true;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    result = Convert.ToBoolean(data);
                }
            }
            return result;
        }
    }
}
