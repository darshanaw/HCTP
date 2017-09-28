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

namespace HonanClaimsWebApi.Services
{
    public class ClaimServices
    {
        private const string claimNotificationApiGet = "api/claim/GetClaimNotification?id=";
        private const string getUsers = "api/Activity/GetUsers?teamNames=";
        private const string getClaimApiGet1 = "api/claim/GetClaims?openClaims=";
        private const string getClaimApiGet2 = "&claimNo=&userId=";
        private const string getNotificationsApiGet = "api/claim/GetNotifications?userId=";
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
