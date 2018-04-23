using HonanClaimsWebApi.Models;
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

namespace HonanClaimsWebApiAccess1.LoginServices
{
    public class LoginService
    {

        private const string loginApi1 = "api/AccountAndReg/TeamPortalLogin?userCode=";
        private const string loginApi_password = "&password=";
        private const string loginApi_loginAttempts = "&loginAttempts=";
        private const string logoutApi = "AccountAndReg/LogoutUser?userId=";

        /// <summary>
        /// System Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ClaimTeamLoginModel Login(string userCode, string password, string loginAttempt)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + loginApi1 + userCode + loginApi_password + password + loginApi_loginAttempts + loginAttempt);
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
                                return new JavaScriptSerializer().Deserialize<ClaimTeamLoginModel>(response);
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
        /// Logout User
        /// </summary>
        /// <param name="userId"></param>
        public void LogoutUser(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + logoutApi + userId);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            responseReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Change User Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> TeamChangeUserPassword(PasswordResetModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        var jsonString = JsonConvert.SerializeObject(model);
                        var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                        formData.Add(content, "changePassword");
                        var postResult = await client.PostAsync(ConfigurationManager.AppSettings["apiurl"] + "api/AccountAndReg/TeamChangeUserPassword", formData);
                        string resultContent = await postResult.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<bool>(resultContent);
                    }
                }
            }
            catch (Exception e)
            {
                return false;

            }
        }
    }
}
