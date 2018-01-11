using HonanClaimsWebApi.Models.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Policies
{
    public class PoliciesRepo
    {
        public async Task<List<CommonModel>> GetPloicyClass()
        {
            List<CommonModel> list = new List<CommonModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/General/GetPickListData?pickListName=Honan Policy Classes";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CommonModel>>(data);
                }
            }
            return list;
        }

        public async Task<List<CommonModel>> GetPolicyPolicyType()
        {
            List<CommonModel> list = new List<CommonModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/General/GetPickListData?pickListName=Honan Policy Classes";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CommonModel>>(data);
                }
            }
            return list;
        }


        public async Task<List<PolictSimple>> GetPolicyList(bool IsActive, string PolicyNumber, string Associate, string Customer, 
            string Underwriter, string PolicyType, string PolicyClass,string userId)
        {
            List<PolictSimple> list = new List<PolictSimple>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Policy/GetPolicies?isActive="+IsActive+"&policyNo="+PolicyNumber+"&associate="+Associate+
                "&customer="+Customer+"&underwriter="+Underwriter+"&policyType="+PolicyType+"&policyClass="+PolicyClass+ "&userId=" + userId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PolictSimple>>(data);
                }
            }
            return list;
        }


        public async Task<PolicyModel> GetPoliciesDetail(string PolicyId)
        {
            PolicyModel list = new PolicyModel();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Policy/GetPolicy?policyId="+ PolicyId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<PolicyModel>(data);
                }
            }
            return list;
        }

        public async Task<bool> UpdatePolicy(string ocNum, string insuredName, string policyId,string userId)
        {
            PolicyModel list = new PolicyModel();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Policy/UpdatePolicy?ocNum=" + ocNum + "&insuredName=" + insuredName + "&policyId=" + policyId + "&userId=" + userId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(data);
                }
            }
            return false;
        }

    }
}
