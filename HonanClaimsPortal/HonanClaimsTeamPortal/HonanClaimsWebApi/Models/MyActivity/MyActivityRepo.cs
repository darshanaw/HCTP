using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HonanClaimsWebApi.Models.MyActivity
{
    public class MyActivityRepo
    {
        public async Task<List<ActivityOwnerModel>> GetOwnerList()
        {
            List<ActivityOwnerModel> list = new List<ActivityOwnerModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Activity/GetActivityOwners";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ActivityOwnerModel>>(data);

                }
            }
            return list;
        }

        public async Task<List<ActivityClaimModel>> GetActivityClaims(string userId)
        {
            List<ActivityClaimModel> list = new List<ActivityClaimModel>();
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
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ActivityClaimModel>>(data);

                }
            }
            return list;
        }

        public async Task<List<ActivityUserModel>> GetUsers(List<string> teamName)
        {
            List<ActivityUserModel> list = new List<ActivityUserModel>();

            var filteredlist = FilterTeamList(teamName);
            if (filteredlist.Count > 0)
            {
                string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(filteredlist);

                string apiUrl = SiteUrl + "api/Activity/GetUsers?teamNames=" + json;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ActivityUserModel>>(data);

                    }
                }
            }
            return list;
        }

        public async Task<List<MyActivityModels>> GetMyActivity(bool overdue, bool nextfiveday, bool showwithdate, string claimId, string assingedId, string owner, string customerId, string searchtext)
        {
            List<MyActivityModels> list = new List<MyActivityModels>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            claimId = claimId == "null" ? string.Empty : claimId;
            assingedId = assingedId == "null" ? string.Empty : assingedId;
            owner = owner == "null" ? string.Empty : owner;
            customerId = customerId == "null" ? string.Empty : customerId;
            searchtext = searchtext == "null" ? string.Empty : searchtext;
            string apiUrl = SiteUrl + "api/Activity/GetMyActivities?isOverDue=" + overdue + "&dueNext5Days=" + nextfiveday + "&showWithDueDate=" + showwithdate + "&claimId=" + claimId + "&assignedToId=" + assingedId + "&activityOwner=" + owner + "&customerId=" + customerId + "&searchText=" + searchtext;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyActivityModels>>(data);

                }
            }
            return list;
        }

        private List<string> FilterTeamList(List<string> teamnames)
        {

            List<string> newlist = new List<string>();
            if (teamnames.Count > 0)
            {
                if (teamnames.Contains("GCC Claims Manager")) { newlist.Add("GCC Claims Manager"); }
                if (teamnames.Contains("Property Claims Manager")) { newlist.Add("Property Claims Manager"); }
                if (teamnames.Contains("Risksmart GCC Manager")) { newlist.Add("Risksmart GCC Manager"); }
                if (teamnames.Contains("Risksmart Property Manager")) { newlist.Add("Risksmart Property Manager"); }
            }
            return newlist;
        }
    }
}
