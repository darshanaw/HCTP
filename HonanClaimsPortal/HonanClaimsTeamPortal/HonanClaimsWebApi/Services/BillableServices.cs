using HonanClaimsWebApi.Models.Billing;
using HonanClaimsWebApi.Models.Common;
using HonanClaimsWebApi.Models.TimeslipCheck;
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
    public class BillableServices
    {
        private const string getInvoiceNumbersUrl = "api/Billing/TeamGetInvoiceNumbersPerClaim?ClaimId=";
        private const string getServiceByUserUrl = "api/Billing/TeamGetClaimBillableTimesServiceByUsers?ClaimId=";
        private const string getBillableTimesUrl = "api/Billing/TeamGetClaimBillableTimes?ClaimId=";
        private const string Params_Billable = "&isBillable=";
        private const string Params_ServiceFromDate = "&serviceFromDate=";
        private const string Params_ServiceToDate = "&serviceToDate=";
        private const string Params_Invoiced = "&invoiced=";
        private const string Params_InvoiceNo = "&invoiceNo=";
        private const string params_serviceUserId = "&serviceUserId=";

        public List<TimeslipDataModel> GetServiceByUserList(string claimId,string isBillable,string serviceFromDate, string serviceToDate, string invoiced, string invoiceNo)
        {
            List<TimeslipDataModel> list = new List<TimeslipDataModel>();
            string apiUrl = ConfigurationManager.AppSettings["apiurl"] + getServiceByUserUrl + claimId + Params_Billable + isBillable + Params_ServiceFromDate + serviceFromDate +
                Params_ServiceToDate + serviceToDate + Params_Invoiced + invoiced + Params_InvoiceNo + invoiceNo;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                      apiUrl);
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
                        return new JavaScriptSerializer().Deserialize<List<TimeslipDataModel>>(response);
                    }
                }
            }

            return null;
        }

        public List<CRMPicklistItem> TeamGetInvoiceNumbersPerClaim(string claimId)
        {
            List<TimeslipDataModel> list = new List<TimeslipDataModel>();
            string SiteUrl = ConfigurationManager.AppSettings["apiurl"];
            string apiUrl = apiUrl = SiteUrl + getInvoiceNumbersUrl + claimId;


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                      apiUrl);
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
                        return new JavaScriptSerializer().Deserialize<List<CRMPicklistItem>>(response);
                    }
                }
            }

            return null;
        }


        public List<BillingSimpleModel> GetBillableTimes(string claimId, string serviceUserId, string isBillable, string serviceFromDate, string serviceToDate, string invoiced, string invoiceNo)
        {
            List<TimeslipDataModel> list = new List<TimeslipDataModel>();
            string apiUrl = ConfigurationManager.AppSettings["apiurl"] + getBillableTimesUrl + claimId + params_serviceUserId + serviceUserId  + Params_Billable + isBillable + Params_ServiceFromDate + serviceFromDate +
                Params_ServiceToDate + serviceToDate + Params_Invoiced + invoiced + Params_InvoiceNo + invoiceNo;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                      apiUrl);
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
                        return new JavaScriptSerializer().Deserialize<List<BillingSimpleModel>>(response);
                    }
                }
            }

            return null;
        }
    }
}
