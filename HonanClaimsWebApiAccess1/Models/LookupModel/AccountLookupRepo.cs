
using HonanClaimsPortal.Models.LookupModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApiAccess1.Models.LookupModel
{
   public class AccountLookupRepo
    {
        public async Task<List<AccountLookup>> GetProtalLogingAccount()
        {
            List<AccountLookup> list = new List<AccountLookup>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/General/GetAccountLookup?accountName=&type=";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AccountLookup>>(data);

                }
            }
            return list;
        }
    
    }
}
