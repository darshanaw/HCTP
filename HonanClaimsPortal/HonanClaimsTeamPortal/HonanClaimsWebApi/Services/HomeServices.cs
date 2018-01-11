using HonanClaimsWebApi.Models.Claim;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Services
{
    public class HomeServices
    {
        public async Task<List<DashboardObject>> TeamGenerateDashboard(string userId)
        {
            try
            {
                string teamListJson = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/General/TeamGenerateDashboard?userId=" + userId);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            return JsonConvert.DeserializeObject<List<DashboardObject>>(responseReader.ReadToEnd());
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
