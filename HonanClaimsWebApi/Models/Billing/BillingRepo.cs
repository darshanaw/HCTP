﻿using HonanClaimsWebApi.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HonanClaimsWebApi.Models.Billing
{
    public class BillingRepo
    {
        public async Task<BillingModel> GetTeamGetBillableTimeRecord(string BillingId)
        {
            BillingModel model = new BillingModel();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Billing/TeamGetBillableTimeRecord?billingId=" + BillingId;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    model = Newtonsoft.Json.JsonConvert.DeserializeObject<BillingModel>(data);

                }
            }
            return model;
        }



        public async Task<List<CommonModel>> GetTeamGetBillableLawyers(string Name)
        {
            List<CommonModel> list = new List<CommonModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Billing/TeamGetBillableLawyers?name="+Name;
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


        
        public async Task<List<CommonModel>> TeamGetClaimNosAssigned(string UserId,string claimRefNumber)
        {
            List<CommonModel> list = new List<CommonModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetClaimNosAssigned?userId="+UserId+"&claimRefNumber="+claimRefNumber;
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


        public async Task<List<CommonModel>> TeamGetClaimActivities(string Claimsid)
        {
            List<CommonModel> list = new List<CommonModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetClaimActivities?claimId="+Claimsid+"&activity=";
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


        public async Task<List<CommonModel>> TeamGetPolicyNoForClaim(string Claimsid)
        {
            List<CommonModel> list = new List<CommonModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetPolicyNoForClaim?claimId=" + Claimsid;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var templist = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonModel>(data);
                    list.Add(templist);
                }
            }
            return list;
        }


        public async Task<CommonModel> TeamGetCustomerForClaim(string Claimsid)
        {
            CommonModel list = new CommonModel();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = SiteUrl + "api/Claim/TeamGetCustomerForClaim?claimId=" + Claimsid;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonModel>(data);

                }
            }
            return list;
        }


        //public async Task<bool> TeamInsertTimeslip(BillingModel model,string UserId)
        //{
        //    string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
        //    string apiUrl = string.Empty;
        //    var json = JsonConvert.SerializeObject(model);
        //    bool result = false;
        //    apiUrl = SiteUrl + "api/Billing/TeamInsertTimeslip?billing=" + json + "&userId="+ UserId;
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(apiUrl);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage response = await client.GetAsync(apiUrl);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            result = true;

        //        }
        //    }
        //    return result;
        //}


        public async Task<bool> TeamInsertTimeslip(BillingModel model, string UserId)
        {
            var result = false;
            var templist = new List<string>();

            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

            string apiUrl = SiteUrl + "api/Billing/TeamInsertTimeslip";
            using (HttpClient client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                    var toList = JsonConvert.SerializeObject(model);
                    var userId = JsonConvert.SerializeObject(UserId);

                    var content = new StringContent(toList, System.Text.Encoding.UTF8, "application/json");
                    var content2 = new StringContent(userId, System.Text.Encoding.UTF8, "application/json");

                    formData.Add(content, "billing");
                    formData.Add(content2, "userId");
                    HttpResponseMessage response =  client.PostAsync(apiUrl, formData).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        result = Convert.ToBoolean(data);
                    }
                }
            }
            return result;
        }

        //public async Task<bool> TeamUpdateTimeslip(BillingModel model, string UserId)
        //{
        //    try
        //    {
        //        string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
        //        string apiUrl = string.Empty;
        //        var json = JsonConvert.SerializeObject(model);
        //        bool result = false;
        //        apiUrl = SiteUrl + "api/Billing/TeamUpdateBilling?billing=" + json + "&userId=" + UserId;
        //        using (HttpClient client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(apiUrl);
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //            HttpResponseMessage response = await client.GetAsync(apiUrl);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                result = true;
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}


        public async Task<bool> TeamUpdateTimeslip(BillingModel model, string userId)
        {
            try
            {
                var result = false;
                var templist = new List<string>();

                string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

                string apiUrl = SiteUrl + "api/Billing/TeamUpdateBilling";
                using (HttpClient client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                   
                        var billing = JsonConvert.SerializeObject(model);
                        var jsonuserId = JsonConvert.SerializeObject(userId);
                        var content = new StringContent(billing, System.Text.Encoding.UTF8, "application/json");
                        var content2 = new StringContent(jsonuserId, System.Text.Encoding.UTF8, "application/json");

                        formData.Add(content, "billing");
                        formData.Add(content2, "userId");
                        HttpResponseMessage response =  client.PostAsync(apiUrl, formData).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            result = Convert.ToBoolean(data);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }          
        }

        private static string insertTimer = "api/ClaimTimer/StartTimer?userId=";
        private static string param_claimId = "&claimId=";

        public string InsertTimerStart(ClaimTimer timer)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + insertTimer + timer.UserId + param_claimId + timer.ClaimId);

                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            return new JavaScriptSerializer().Deserialize<string>(response);
                        }
                    }
                }
                
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        private static string getTimer = "api/ClaimTimer/GetClaimTimer?userId=";
        public ClaimTimer GetTimerStart(string userId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getTimer + userId);

                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            return new JavaScriptSerializer().Deserialize<ClaimTimer>(response);
                        }
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        private static string endTimer = "api/ClaimTimer/EndTimer?userId=";
        private static string param_claimTimerId = "&claimTimerId=";
        public bool endTimerFunc(string userId, string claimTimerId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + endTimer + userId + param_claimTimerId + claimTimerId);

                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            return new JavaScriptSerializer().Deserialize<bool>(response);
                        }
                    }
                }

                return false;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
