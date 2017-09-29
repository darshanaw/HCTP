﻿using System;
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
        private const string claimNotificationApiGet = "api/claim/GetClaimNotification?id=";
        private const string getUsers = "api/Activity/GetUsers?teamNames=";
        private const string getClaimApiGet1 = "api/claim/GetClaims?openClaims=";
        private const string getClaimApiGet2 = "&claimNo=&userId=";
        private const string getNotificationsApiGet = "api/claim/GetNotifications?userId=";
        private const string getClaimsPolicyNoApiGet = "api/Claim/GenerateClaimRefNo?teamName=";

        private const string insertClaimNotificationApiGet1 = "api/Claim/InsertClaimNotification?claim=";
        private const string insertClaimNotificationApiGet2 = "&userId=";

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

        public ExecutionResult InsertClaimNotification(ClaimGeneral claim, string userId)
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
    }
}
