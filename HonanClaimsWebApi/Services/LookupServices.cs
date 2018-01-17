using HonanClaimsWebApi.Models;
using HonanClaimsWebApi.Models.LookupModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HonanClaimsWebApi.Services
{
    public class LookupServices
    {
        private const string accountApiGet1 = "api/General/GetAccountLookup?accountName=";
        private const string accountApiGet2 = "&type=";

        private const string ocApiGet1 = "api/General/GetOCNumLookup?ocNum=";
        private const string ocApiGet2 = "&policyId=";

        private const string policyApiGet1 = "api/General/GetPolicyLookup?policyNo=";
        private const string policyApiGet2 = "&accountId=";

        private const string storeApiGet1 = "api/General/GetStoreLookup?storeName=";
        private const string storeApiGet2 = "&accountId=";

        private const string contactApiGet1 = "api/General/GetContactLookup?contactName=";
        private const string contactApiGet2 = "&accountId=";


        public List<AccountSimpleModel> GetAccounts(string accountName, string accountType)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + accountApiGet1 + accountName + accountApiGet2 + accountType);
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
                            return new JavaScriptSerializer().Deserialize<List<AccountSimpleModel>>(response);
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

        public List<CRMOCNumSimple> GetOCNumLookup(string ocNum, string policyId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + ocApiGet1 + ocNum + ocApiGet2 + policyId);
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
                            return new JavaScriptSerializer().Deserialize<List<CRMOCNumSimple>>(response);
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

        public List<PolicySimple> GetPolicies(string policyNo, string accountId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + policyApiGet1 + policyNo + policyApiGet2 + accountId);
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
                            return new JavaScriptSerializer().Deserialize<List<PolicySimple>>(response);
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

        public List<StoreSimple> GetStores(string storeName, string policyId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + storeApiGet1 + storeName + storeApiGet2 + policyId);
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
                            return new JavaScriptSerializer().Deserialize<List<StoreSimple>>(response);
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

        public List<CRMContactSimple> GetContactLookup(string contactName, string accountId)
        {
            try
            {
                accountId = accountId ?? "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + contactApiGet1 + contactName + contactApiGet2 + accountId);
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
                            return new JavaScriptSerializer().Deserialize<List<CRMContactSimple>>(response);
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

        public List<CRMOCNumSimple> TeamGetOcNumLookupByPolicy(string ocNum, string policyId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/General/TeamGetOcNumLookupByPolicy?ocNum=" + ocNum + "&policyNo=" + policyId);
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
                            return new JavaScriptSerializer().Deserialize<List<CRMOCNumSimple>>(response);
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

        public async Task<List<string>> GetPolicyClassesOfAccount(string accountId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/Policy/GetPolicyClassesOfAccount?accountId=" + accountId);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            return JsonConvert.DeserializeObject<List<string>>(responseReader.ReadToEnd());
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

        public async Task<List<AccountSimpleModel>> GetPolicyInsurersOfAccount(string accountId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/Policy/GetPolicyInsurersOfAccount?accountId=" + accountId);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            return JsonConvert.DeserializeObject<List<AccountSimpleModel>>(responseReader.ReadToEnd());
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

        public List<PolicySimple> GetDetailPolicies(string dateOfLoss, string policyNo, string policyClass, string associate,
        string insuredName, string address, string insurer, string accountId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + "api/General/GetPolicyLookupDetail?dateOfLoss="
                    + dateOfLoss + "&policyNo=" + policyNo + "&policyClass=" + policyClass + "&associate=" + associate 
                    + "&insuredName=" + insuredName + "&address=" + address + "&insurer=" + insurer + "&accountId=" + accountId);
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
                            return new JavaScriptSerializer().Deserialize<List<PolicySimple>>(response);
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
