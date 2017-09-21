using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApiAccess1.Models.TeamGetClaimAssigment
{
    public class TeamGetClaimAssigmentRepo
    {
        public async Task<List<CRMPicklistItem>> TeamGetClaimAssigmentList(List<string> TeamList)
        {
            string json = JsonConvert.SerializeObject(TeamList);


            List<CRMPicklistItem> list = new List<CRMPicklistItem>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetClaimAssigmentList?teamList="+ json;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CRMPicklistItem>>(data);

                }
            }
            return list;
        }


       
        public async Task<List<CRMPicklistItemModel>> GetTeamGetUsersofTeam(string TeamName)
        {
            List<CRMPicklistItemModel> list = new List<CRMPicklistItemModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetUsersofTeam?team=" + TeamName;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CRMPicklistItemModel>>(data);

                }
            }
            return list;
        }

        public async Task<bool> TeamAssignUserToClaims(string ClaimIdList,string UserId)
        {
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamAssignUserToClaims?claimIdList="+ ClaimIdList + "&userId=" + UserId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return true;
                }
            }
            return false;
        }
        
    }
}
