﻿
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        public async Task<bool> SaveAdminRecord(HttpPostedFileBase file1, HttpPostedFileBase file2, CustomerPortalAdminModel model)
        {

            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/AccountAndReg/TeamInsertCustomerPortalAdmin";
            var json = JsonConvert.SerializeObject(model);

            using (var client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    if (file2 != null)
                    {
                        formData.Add(new StreamContent(file2.InputStream), "logo", file2.FileName);
                    }
                    if (file1 != null)
                    {
                        formData.Add(new StreamContent(file1.InputStream), "manualClaimForm", file1.FileName);
                    }

                    var jsonString = JsonConvert.SerializeObject(model);
                    var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                    formData.Add(content, "PortalAdminDetail", "PortalAdminDetail");

                    var result = await client.PostAsync(apiUrl, formData);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    return Convert.ToBoolean(resultContent);
                }
            }
        }

        public async Task<bool> SaveAdminLoginRecord(AdminLoginsModel model, string userId)
        {

            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = string.Empty;
            if (model.IsNew)
            {
                apiUrl = SiteUrl + "/api/AccountAndReg/TeamInsertCustomerPortalLogin?portalLogin=&userId=" + userId;
            }
            else
            {
                apiUrl = SiteUrl + "/api/AccountAndReg/TeamUpdateCustomerPortalLogin?portalLogin=" + model.H_PortalLoginId + "&userId=" + userId;
            }
            var json = JsonConvert.SerializeObject(model);

            using (var client = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                var result = await client.PostAsync(apiUrl, content);
                string resultContent = await result.Content.ReadAsStringAsync();
                return Convert.ToBoolean(resultContent);

            }
        }

    }
}
