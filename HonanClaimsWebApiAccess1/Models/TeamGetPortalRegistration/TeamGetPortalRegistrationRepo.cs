using HonanClaimsPortal.Models.ProtalLogingRequest;
using HonanClaimsWebApiAccess1.Models.TeamGetPortalRegistration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApiAccess1.Models.ProtalLogingRequest
{
    public class TeamGetPortalRegistrationRepo
    {
        public async Task<TeamGetPortalRegistrationModel> GetTeamGetPortalRegistration(string portalRegRequestId)
        {
            var obj = new TeamGetPortalRegistrationModel();

            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/AccountAndReg/TeamGetPortalRegistration?portalRegRequestId=" + portalRegRequestId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    obj = Newtonsoft.Json.JsonConvert.DeserializeObject<TeamGetPortalRegistrationModel>(data);
                    List<MatchingContactsModel> temp = new List<MatchingContactsModel>();

                    temp.Add(new MatchingContactsModel()
                    {
                        FirstName = "Krishan",
                        LastName = "Manjula",
                        ContactId = "0001",
                        ContactName = "Kris",
                        Phone = "0766828898",
                        AccountName = "Acc",
                    });
                    obj.MatchingContacts = temp;
                }
            }
            return obj;
        }


        public async Task<bool> TeamInsertPortalLoginByContact(string portalRegRequestId, string matchingContactId, string userId)
        {
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/AccountAndReg/TeamInsertPortalLoginByContact?portalRegRequestId=" + portalRegRequestId + "&matchingContactId=" + matchingContactId + "&userId=" + userId;
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


        public async Task<bool> TeamDiscardLoginRequest(string portalRegRequestId)
        {
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/AccountAndReg/TeamDiscardLoginRequest?portalRegRequestId=" + portalRegRequestId;
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
