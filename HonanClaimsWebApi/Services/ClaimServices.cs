using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Models.Common;
using Newtonsoft.Json;
using HonanClaimsWebApi.Models.Views;
using HonanClaimsWebApi.Models;

namespace HonanClaimsWebApi.Services
{
    public class ClaimServices
    {
        private const string claimNotificationApiGet = "api/claim/TeamGetClaimNotification?id=";
        private const string getUsers = "api/Activity/GetUsers?teamNames=";
        private const string getClaimApiGet1 = "api/claim/GetClaims?openClaims=";
        private const string getClaimApiGet2 = "&claimNo=&userId=";
        private const string getNotificationsApiGet = "api/claim/GetNotifications?userId=";
        private const string getClaimsPolicyNoApiGet = "api/Claim/GenerateClaimRefNo?teamName=";

        private const string insertClaimNotificationApiGet1 = "api/Claim/TeamInsertClaimNotification?claim=";
        private const string insertClaimNotificationApiGet2 = "&userId=";
        private const string insertHistoryRecord = "api/General/CreateHistoryRecord?userId=";
        private const string updateClaimNotificationApiGet1 = "api/Claim/TeamUpdateClaimNotification?claim=";

        private const string getUserRefNumbers = "api/Claim/GetClaimsForUser?assignedToId=";
        private const string convertToClaim = "api/Claim/ConvertToClaim?claimId=";


        ExecutionResult exeReult;

        public List<SelectListItem> GetClaimTeams()
        {
            List<SelectListItem> str = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Test1",Value="Test1"},
                new SelectListItem(){Text="Test2",Value="Test2"},
                new SelectListItem(){Text="Test3",Value="Test3"}
            };

            return str;
        }

        public string GenerateClaimRefNo(string teamName)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getClaimsPolicyNoApiGet + teamName);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            return responseReader.ReadToEnd();
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

        public ExecutionResult TeamInsertClaimNotification(ClaimGeneral claim, string userId)
        {
            string responseClaimId = "";
            exeReult = new ExecutionResult();
            try
            {
                string jsonClaim = new JavaScriptSerializer().Serialize(claim);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + insertClaimNotificationApiGet1 + jsonClaim +
                    insertClaimNotificationApiGet2 + userId);

                claim.UserId = userId;
                var dataString = JsonConvert.SerializeObject(claim);

                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    string response = client.UploadString(new Uri(ConfigurationManager.AppSettings["apiurl"] + insertClaimNotificationApiGet1), "POST", dataString);
                    responseClaimId = response;
                }
                
                exeReult.ResultObject = responseClaimId;
                exeReult.IsSuccess = true;
                exeReult.IsFailure = false;
                return exeReult;

            }
            catch (Exception e)
            {
                exeReult.IsFailure = true;
                exeReult.IsSuccess = false;
                throw e;
            }


        }

        /// <summary>
        /// Get Claim/Notification by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClaimGeneral GetClaimNotification(string id)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + claimNotificationApiGet + id);
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
                                return new JavaScriptSerializer().Deserialize<ClaimGeneral>(response);
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

        /// <summary>
        /// Get Users of Teams
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public List<PicklistItem> GetUsers(List<string> teamName)
        {
            try
            {
                ServiceHelper helper = new ServiceHelper();
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getUsers +
                    JsonConvert.SerializeObject(teamName));
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
                                return new JavaScriptSerializer().Deserialize<List<PicklistItem>>(response);
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

        public List<ClaimNotificationSimple> GetClaimsObjectList(bool isOpenClaims, string userId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getClaimApiGet1 + isOpenClaims + getClaimApiGet2 + userId);
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
                            return new JavaScriptSerializer().Deserialize<List<ClaimNotificationSimple>>(response);
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

        public List<ClaimNotificationSimple> GetNotificationsObjectList(string userId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getNotificationsApiGet + userId);
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
                                return new JavaScriptSerializer().Deserialize<List<ClaimNotificationSimple>>(response);
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

        /// <summary>
        /// Create Hitory Record
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="claimId"></param>
        /// <param name="fieldName"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public string CreateHistoryRecord(string userId,string userName, string claimId, string fieldName, string newValue)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + insertHistoryRecord + userId + "&userName=" + userName + "&claimId=" + claimId + "&fieldName=" + fieldName + "&newValue=" + newValue);
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
                                return response;
                            }
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


        //public bool TeamUpdateClaimNotification(ClaimGeneral claim, string userId)
        //{
        //    string responseClaimId = "";

        //    try
        //    {
        //        string jsonClaim = new JavaScriptSerializer().Serialize(claim);
        //        claim.UserId = userId;
        //        var dataString = JsonConvert.SerializeObject(claim);

        //        using (var client = new WebClient())
        //        {
        //            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
        //            string response = client.UploadString(new Uri(ConfigurationManager.AppSettings["apiurl"] + updateClaimNotificationApiGet1), "POST", dataString);
        //            return bool.Parse(response);
        //        }

        //        return true;

        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //        throw e;
        //    }


        //}

        public async Task<bool> TeamUpdateClaimNotification(ClaimGeneral claim, string userId)
        {
            string responseClaimId = "";

            try
            {
                string jsonClaim = new JavaScriptSerializer().Serialize(claim);
                claim.UserId = userId;
                var dataString = JsonConvert.SerializeObject(claim);

                using (var client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        //client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                        //string response = client.UploadString(new Uri(ConfigurationManager.AppSettings["apiurl"] + updateClaimNotificationApiGet1), "POST", dataString);
                        //return bool.Parse(response);

                        var jsonString = JsonConvert.SerializeObject(claim);
                        var jsonString_userId = JsonConvert.SerializeObject(userId);
                        var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                        var content_userId = new StringContent(jsonString_userId, System.Text.Encoding.UTF8, "application/json");
                        formData.Add(content, "claim");
                        //formData.Add(content_userId, "userId");


                        var postResult = await client.PostAsync(ConfigurationManager.AppSettings["apiurl"] + updateClaimNotificationApiGet1, formData);
                        string resultContent = await postResult.Content.ReadAsStringAsync();
                    }
                }

                return true;

            }
            catch (Exception e)
            {
                return false;
                throw e;
            }


        }

        public List<CRMPicklistItem> GetClaimsForUser(string assignedToId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getUserRefNumbers + assignedToId);
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
                                return new JavaScriptSerializer().Deserialize<List<CRMPicklistItem>>(response);
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

        public bool ConvertNotificationToClaim(string userId,string claimId,string policyNo,string assignedUserId,string teamName)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + convertToClaim + claimId + "&teamName=" + teamName + "&assignedUserId=" + assignedUserId + "&policyNo=" + policyNo + "&userid=" + userId);
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

        public bool ShowClaimWarningBanner(string claimId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/Claim/TeamShowClaimWarningBanner?claimId=" + claimId);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            return JsonConvert.DeserializeObject<bool>(responseReader.ReadToEnd());
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

        public bool HideClaimWarningBanner(string claimId,string userId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/Claim/TeamHideClaimWarningBanner?claimId=" + claimId + "&userId=" + userId);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            return JsonConvert.DeserializeObject<bool>(responseReader.ReadToEnd());
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
