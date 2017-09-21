using HonanClaimsPortal.Models.ProtalLogingRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace HonanClaimsPortal.Models.ProtalLoginAccounts
{
    public class ProtalLogingRequestRepo
    {
        public async Task<List<ProtalLogingRequestModel>> GetProtalLogingAccount()
        {
            List<ProtalLogingRequestModel> list = new List<ProtalLogingRequestModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/AccountAndReg/GetPortalRegistrations";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProtalLogingRequestModel>>(data);

                }
            }
            return list;
        }
    }
}