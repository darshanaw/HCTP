
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApiAccess1.Models.AdminLoginDetail
{
    public class AdminLogindetailRepo
    {
        public async Task<List<AdminLoginsModel>> GetAdminLogins(string adminId)
        {
            List<AdminLoginsModel> list = new List<AdminLoginsModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/AccountAndReg/TeamGetCustomerPortalLogins?portalAdminId=" + adminId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AdminLoginsModel>>(data);

                }
            }
            return list;
        }

        public async Task<CustomerPortalAdminModel> GetAdminRecord(string adminId)
        {
            CustomerPortalAdminModel list = new CustomerPortalAdminModel();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/AccountAndReg/TeamGetCustomerPortalAdminDetail?customerPortalAdminId=" + adminId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerPortalAdminModel>(data);

                }
            }
            return list;
        }

    }
}
