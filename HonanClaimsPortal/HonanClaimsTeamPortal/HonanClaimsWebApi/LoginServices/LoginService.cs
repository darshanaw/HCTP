using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
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
        public ClaimTeamLogin Login(string userCode, string password, string loginAttempt)
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
                                return new JavaScriptSerializer().Deserialize<ClaimTeamLogin>(response);
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
    }
}
