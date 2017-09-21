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
        public async Task<List<ClaimListModel>> GetAdminLogins(string userId, bool myclaimsOnly, bool isopenClaim, string claimType, string searchText, string cutomerId)
        {
            List<ClaimListModel> list = new List<ClaimListModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetMyClaimList?assignedToId=" + userId + "& myClaimsOnly=" + myclaimsOnly + "& isOpenClaims=" + isopenClaim + "& claimType=" + claimType + "& searchText" + searchText + "= &customerId=" + cutomerId;
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
