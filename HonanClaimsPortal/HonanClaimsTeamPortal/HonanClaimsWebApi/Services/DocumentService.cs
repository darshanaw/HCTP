using HonanClaimsWebApi.Models;
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
using System.Web;
using System.Web.Script.Serialization;

namespace HonanClaimsWebApi.Services
{
    public class DocumentService
    {
        private const string createClaimAttachApiGet1 = "api/claim/CreateClaimAttachmentCustomerDoc?ClaimId=";
        private const string createClaimAttachApiGet2 = "&AttachmentType=";
        private const string createClaimAttachApiGet3 = "&AttachmentName=";
        private const string createClaimAttachApiGet4 = "&AttachmentDescription=";
        private const string createClaimAttachApiGet5 = "&Size=";
        private const string createClaimAttachApiGet6 = "&UserId=";
        private const string createClaimAttachApiGet7 = "&IsCustomerDoc=";
        private const string createClaimAttachApiGet8 = "api/claim/TeamGetDocs?ClaimId=";
        private const string createClaimAttachApiGet9 = "&isCustomerDocs=";
     
        private const string createFileNote = "api/FileNote/CreateFileNoteRecord?userId=";
        private const string updateFileNote = "api/FileNote/TeamUpdateFileNoteRecord?userId=";
        private const string param_shortDes = "&shortDescription=";
        private const string param_detail = "&details=";
        private const string param_claimId = "&claimId=";
        private const string param_fileNoteDate = "&fileNoteDate=";
        private const string param_fileNoteId = "&fileNoteId=";

        private const string getFileNotes = "api/FileNote/GetFileNotes?searchText=";
        private const string getFileNoteById = "api/FileNote/TeamGetFileNote?fileNoteId=";

        private const string getmInsertPayment = "api/Payment/TeamInsertPayment";
        private const string getmUpdatePayment = "api/Payment/TeamUpdatePayment";

        private const string getPaymentDetails = "api/Payment/GetPayments?claimId=";
        private const string getPayee = "&payeeId=";
        private const string getInvoiceDate = "&invoicedDate=";
        private const string getStatus = "&status=";
        private const string getInvoiceNo = "&invoiceNo=";
        private const string getPaymentDetailById = "api/Payment/GetPayment?paymentId=";
        private const string param_paymentId = "&paymentId=";

        ExecutionResult exeReult;

        public bool CreateClaimAttachmentCustomerDoc(ClaimAttachmentSimple attachment, out string fileCreateId)
        {
            string _fileCreateId = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + createClaimAttachApiGet1 + attachment.ClaimId + createClaimAttachApiGet2 + attachment.AttachmentType
                    + createClaimAttachApiGet3 + attachment.AttachmentName + createClaimAttachApiGet4 + attachment.AttachmentDescription
                    + createClaimAttachApiGet5 + attachment.Size + createClaimAttachApiGet6 + attachment.UserId + createClaimAttachApiGet7 + attachment.IsCustomerDoc);

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
                            _fileCreateId = new JavaScriptSerializer().Deserialize<string>(response);
                        }
                    }
                }

                fileCreateId = _fileCreateId;

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<ClaimAttachmentSimple> TeamGetDocs(string claimId, string isCustomerDocs)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + createClaimAttachApiGet8 + claimId + createClaimAttachApiGet9 + isCustomerDocs);

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
                            return new JavaScriptSerializer().Deserialize<List<ClaimAttachmentSimple>>(response);
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

        public ExecutionResult CreateFileNoteRecord(string userId, string shortDescription, string details, string claimsId, DateTime fileNoteDate)
        {
            exeReult = new ExecutionResult();
            string result = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + createFileNote + userId + param_shortDes + shortDescription + param_detail + details
                    + param_claimId + claimsId + param_fileNoteDate + fileNoteDate);

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
                            result = new JavaScriptSerializer().Deserialize<string>(response);
                        }
                    }
                }

                exeReult.ResultObject = result;
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

        public ExecutionResult UpdateFileNoteRecord(string userId, string shortDescription, string details, string claimsId, DateTime fileNoteDate, string fileNoteId)
        {
            exeReult = new ExecutionResult();
            string result = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + updateFileNote + userId + param_shortDes + shortDescription + param_detail + details
                    + param_fileNoteDate + fileNoteDate + param_fileNoteId + fileNoteId);

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
                            result = new JavaScriptSerializer().Deserialize<string>(response);
                        }
                    }
                }

                exeReult.ResultObject = result;
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

        public List<FileNote> GetFileNotes(string searchText, string claimId)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getFileNotes + searchText + param_claimId + claimId);

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
                            return new JavaScriptSerializer().Deserialize<List<FileNote>>(response);
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

        public FileNote GetFileNoteById(string fileNoteId)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getFileNoteById + fileNoteId);

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
                            return new JavaScriptSerializer().Deserialize<List<FileNote>>(response).FirstOrDefault();
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


        // Payment Detail

        public async Task<ExecutionResult> CreatePaymentDetailRecord(Payment payment, string userId, IEnumerable<HttpPostedFileBase> invoice)
        {
            exeReult = new ExecutionResult();
            string result = "";

            try
            {              
                string apiUrl = string.Empty;

                if (payment.IsNew)
                {
                    apiUrl = ConfigurationManager.AppSettings["apiurl"] + getmInsertPayment;
                }
                else
                    apiUrl = ConfigurationManager.AppSettings["apiurl"] + getmUpdatePayment;

                using (var client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        if (invoice != null && invoice.Count() > 0)
                        {
                            formData.Add(new StreamContent(invoice.FirstOrDefault().InputStream), "invoice", invoice.FirstOrDefault().FileName);
                        }
                     
                        var jsonString = JsonConvert.SerializeObject(payment);
                        var jsonString_userId = JsonConvert.SerializeObject(userId);
                        var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                        var content_userId = new StringContent(jsonString_userId, System.Text.Encoding.UTF8, "application/json");
                        formData.Add(content, "payment");
                        formData.Add(content_userId, "userId");


                        var postResult = await client.PostAsync(apiUrl, formData);
                        string resultContent = await postResult.Content.ReadAsStringAsync();
                        exeReult.IsSuccess = Convert.ToBoolean(resultContent);
                    }
                }

                exeReult.ResultObject = result;
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

       

        public List<PaymentSimple> GetPaymentDetails(string claimId, string payeeId, string invoicedDate, string status, string invoiceNo)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getPaymentDetails + claimId + getPayee + payeeId + getInvoiceDate + invoicedDate + getStatus + status + getInvoiceNo + invoiceNo);

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
                            return new JavaScriptSerializer().Deserialize<List<PaymentSimple>>(response);
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
        

        public Payment getPaymentById(string paymentId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getPaymentDetailById + paymentId);

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
                            return new JavaScriptSerializer().Deserialize<Payment>(response);
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
