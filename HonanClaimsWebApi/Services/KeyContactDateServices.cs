using HonanClaimsWebApi.Models.Claim;
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

namespace HonanClaimsWebApi.Services
{
    public class KeyContactDateServices
    {
        private const string getKeyContacts = "api/KeyContact/TeamGetKeyContacts?claimId=";
        private const string getKeyDates = "api/KeyDate/TeamGetKeyDates?claimId=";
        private const string deleteKeyContact = "api/KeyContact/TeamDeleteKeyContact?keyContactId=";
        private const string deleteKeyDate = "api/KeyDate/TeamDeleteKeyDate?keyDateId=";

        public List<KeyContact> GetKeyContacts(string claimId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getKeyContacts + claimId);
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
                            if (!string.IsNullOrEmpty(response))
                            {
                                return new JavaScriptSerializer().Deserialize<List<KeyContact>>(response);
                            }
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

        public List<KeyDate> GetKeyDates(string claimId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getKeyDates + claimId);
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
                            if (!string.IsNullOrEmpty(response))
                            {
                                return new JavaScriptSerializer().Deserialize<List<KeyDate>>(response);
                            }
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

        public bool DeleteKeyContact(string keyContactId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + deleteKeyContact + keyContactId);
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
                            if (!string.IsNullOrEmpty(response))
                            {
                                return new JavaScriptSerializer().Deserialize<bool>(response);
                            }
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

        public bool DeleteKeyDate(string keyDateId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + deleteKeyDate + keyDateId);
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
                            if (!string.IsNullOrEmpty(response))
                            {
                                return new JavaScriptSerializer().Deserialize<bool>(response);
                            }
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

        public async Task<bool> InsertKeyContact(KeyContact model, string userId)
        {
            var result = false;
            if (model != null)
            {
                string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

                var json = JsonConvert.SerializeObject(model);

                string apiUrl = SiteUrl + "api/KeyContact/TeamInsertKeyContact";
                using (HttpClient client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        var content2 = new StringContent(userId, System.Text.Encoding.UTF8, "application/json");
                        formData.Add(content, "keyContact");
                        formData.Add(content2, "userId");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, formData);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            result = Convert.ToBoolean(data);

                        }

                    }

                }

            }

            return result;
        }

        public KeyContact GetKeyContact(string keyContactId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/KeyContact/TeamGetKeyContact?keyContactId=" + keyContactId);
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
                            if (!string.IsNullOrEmpty(response))
                            {
                                return new JavaScriptSerializer().Deserialize<KeyContact>(response);
                            }
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

        public async Task<bool> UpdateKeyContact(KeyContact model, string userId)
        {
            var result = false;
            if (model != null)
            {
                string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

                var json = JsonConvert.SerializeObject(model);

                string apiUrl = SiteUrl + "api/KeyContact/TeamUpdateKeyContact";
                using (HttpClient client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        var content2 = new StringContent(userId, System.Text.Encoding.UTF8, "application/json");
                        formData.Add(content, "keyContact");
                        formData.Add(content2, "userId");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, formData);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            result = Convert.ToBoolean(data);

                        }

                    }

                }

            }

            return result;
        }

        public async Task<bool> InsertKeyDate(KeyDate model, string userId)
        {
            var result = false;
            if (model != null)
            {
                string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

                var json = JsonConvert.SerializeObject(model);

                string apiUrl = SiteUrl + "api/KeyDate/TeamInsertKeyDate";
                using (HttpClient client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        var content2 = new StringContent(userId, System.Text.Encoding.UTF8, "application/json");
                        formData.Add(content, "keyDate");
                        formData.Add(content2, "userId");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, formData);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            result = Convert.ToBoolean(data);

                        }

                    }

                }

            }

            return result;
        }

        public async Task<bool> UpdateKeyDate(KeyDate model, string userId)
        {
            var result = false;
            if (model != null)
            {
                string SiteUrl = ConfigurationManager.AppSettings["apiurl"];

                var json = JsonConvert.SerializeObject(model);

                string apiUrl = SiteUrl + "api/KeyDate/TeamUpdateKeyDate";
                using (HttpClient client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        var content2 = new StringContent(userId, System.Text.Encoding.UTF8, "application/json");
                        formData.Add(content, "keyDate");
                        formData.Add(content2, "userId");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, formData);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            result = Convert.ToBoolean(data);

                        }

                    }

                }

            }

            return result;
        }

        public int GetKeyDateNextSequnce(string claimId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/KeyDate/TeamGetNextSequnce?claimId=" + claimId);
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
                            if (!string.IsNullOrEmpty(response))
                            {
                                return new JavaScriptSerializer().Deserialize<int>(response);
                            }
                        }
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public KeyDate GetKeyDate(string keyDateId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/KeyDate/TeamGetKeyDate?keyDateId=" + keyDateId);
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
                            if (!string.IsNullOrEmpty(response))
                            {
                                return new JavaScriptSerializer().Deserialize<KeyDate>(response);
                            }
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
    }


}
