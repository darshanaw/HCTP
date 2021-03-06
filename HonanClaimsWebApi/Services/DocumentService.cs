﻿using HonanClaimsWebApi.Models;
using HonanClaimsWebApi.Models.Claim;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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

        private const string getActivityTasks = "api/Activity/GetActivities?claimId=";
        private const string param_incompleteOnly = "&incompleteOnly=";
        private const string param_completedOnly = "&completedOnly=";
        private const string param_showOverDue = "&showOverDue=";
        private const string param_owner = "&owner=";

        private const string getSequense = "api/Activity/TeamGetActivitySequences?claimId=";
        private const string param_state = "&isInsert=";

        private const string getActivitiesForWorkFlow = "api/Activity/TeamGetActivitiesForWorkFlow?claimId=";
        private const string param_completingActionSeq = "&completingActionSeq=";

        private const string insertActivityTask = "api/Activity/InsertActivityDetail";
        private const string updateActivityTask = "api/Activity/UpdtateActivityDetail";

        private const string getGetActivityTaskDetailById = "api/Activity/TeamGetActivityTaskDetail?activityId=";
        private const string deleteRecord = "api/Activity/DeleteActivity?claimId=";
        private const string param_activityId = "&activityId=";
        private const string param_seq = "&seq=";

        private const string completeActivityTask = "api/Activity/CompleteActivityTaskWithNextAction?activityTaskId=";
        private const string param_action = "&action=";
        private const string param_actionSeq = "&actionSeq=";
        private const string param_nextSeq = "&nextSeq=";
        private const string param_nextActivity = "&nextActivityId=";
        private const string param_nextDue = "&nextActivityDue=";
        private const string param_user = "&userId=";
        private const string param_claim = "&claimId=";


        private const string getNextTaskDueDate_ = "api/Activity/TeamGetNextActivityDueDate?nextTaskId=";


        ExecutionResult exeReult;

        public bool CreateClaimAttachmentCustomerDoc(ClaimAttachmentSimple attachment, out string fileCreateId)
        {
            string _fileCreateId = "";

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + createClaimAttachApiGet1 + attachment.ClaimId + createClaimAttachApiGet2 + attachment.AttachmentType
                    + createClaimAttachApiGet3 + HttpUtility.UrlEncode(attachment.AttachmentName, Encoding.UTF8) + createClaimAttachApiGet4 + HttpUtility.UrlEncode(attachment.AttachmentDescription, Encoding.UTF8)
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

        //public ExecutionResult CreateFileNoteRecord(string userId, string shortDescription, string details, string claimsId, DateTime fileNoteDate)
        //{
        //    exeReult = new ExecutionResult();
        //    string result = "";
        //    try
        //    {
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
        //            ConfigurationManager.AppSettings["apiurl"] + createFileNote + userId + param_shortDes + shortDescription + param_detail + details
        //            + param_claimId + claimsId + param_fileNoteDate + fileNoteDate);

        //        request.Method = "GET";
        //        request.ContentType = "application/json";

        //        WebResponse webResponse = request.GetResponse();
        //        using (Stream webStream = webResponse.GetResponseStream())
        //        {
        //            if (webStream != null)
        //            {
        //                using (StreamReader responseReader = new StreamReader(webStream))
        //                {
        //                    string response = responseReader.ReadToEnd();
        //                    result = new JavaScriptSerializer().Deserialize<string>(response);
        //                }
        //            }
        //        }

        //        exeReult.ResultObject = result;
        //        exeReult.IsSuccess = true;
        //        exeReult.IsFailure = false;
        //        return exeReult;
        //    }
        //    catch (Exception e)
        //    {
        //        exeReult.IsFailure = true;
        //        exeReult.IsSuccess = false;

        //        throw e;
        //    }

        //}



        public async Task<ExecutionResult> CreateFileNoteRecord(string userId, string shortDescription, string details, string claimsId, DateTime fileNoteDate)
        {
            exeReult = new ExecutionResult();
            string result = "";

            try
            {
                string apiUrl = ConfigurationManager.AppSettings["apiurl"] + createFileNote + userId + param_shortDes + shortDescription + param_claimId + claimsId + param_fileNoteDate + fileNoteDate;

                using (var client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {

                        var jsonString = JsonConvert.SerializeObject(details);

                        var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                        formData.Add(content, "detail");

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

        //public ExecutionResult UpdateFileNoteRecord(string userId, string shortDescription, string details, string claimsId, DateTime fileNoteDate, string fileNoteId)
        //{
        //    exeReult = new ExecutionResult();
        //    string result = "";
        //    try
        //    {
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
        //            ConfigurationManager.AppSettings["apiurl"] + updateFileNote + userId + param_shortDes + shortDescription + param_detail + details
        //            + param_fileNoteDate + fileNoteDate + param_fileNoteId + fileNoteId);

        //        request.Method = "GET";
        //        request.ContentType = "application/json";

        //        WebResponse webResponse = request.GetResponse();
        //        using (Stream webStream = webResponse.GetResponseStream())
        //        {
        //            if (webStream != null)
        //            {
        //                using (StreamReader responseReader = new StreamReader(webStream))
        //                {
        //                    string response = responseReader.ReadToEnd();
        //                    result = new JavaScriptSerializer().Deserialize<string>(response);
        //                }
        //            }
        //        }

        //        exeReult.ResultObject = result;
        //        exeReult.IsSuccess = true;
        //        exeReult.IsFailure = false;
        //        return exeReult;
        //    }
        //    catch (Exception e)
        //    {
        //        exeReult.IsFailure = true;
        //        exeReult.IsSuccess = false;

        //        throw e;
        //    }

        //}

        public async Task<ExecutionResult> UpdateFileNoteRecord(string userId, string shortDescription, string details, string claimsId, DateTime fileNoteDate, string fileNoteId)
        {
            exeReult = new ExecutionResult();
            string result = "";

            try
            {
                string apiUrl = ConfigurationManager.AppSettings["apiurl"] + updateFileNote + userId + param_shortDes + shortDescription
                    + param_fileNoteDate + fileNoteDate + param_fileNoteId + fileNoteId;

                using (var client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {

                        var jsonString = JsonConvert.SerializeObject(details);

                        var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                        formData.Add(content, "detail");

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

                if (payment.IsNew && string.IsNullOrEmpty(payment.H_Paymentsid))
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
                            var t = invoice.FirstOrDefault().InputStream;
                            formData.Add(new StreamContent(invoice.FirstOrDefault().InputStream), "invoice", invoice.ToList()[0].FileName);
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


        public List<ActivityTask> GetActivityTasks(string claimId, bool incompleteOnly, bool completedOnly, bool showOverDue, string owner)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getActivityTasks + claimId + param_incompleteOnly + incompleteOnly + param_completedOnly + completedOnly + param_showOverDue + showOverDue + param_owner + owner);

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
                            return new JavaScriptSerializer().Deserialize<List<ActivityTask>>(response);
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


        public List<ActivityTask> GetActivitiesForWorkFlow(string claimId, int completingActionSeq)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getActivitiesForWorkFlow + claimId + param_completingActionSeq + completingActionSeq);

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
                            return new JavaScriptSerializer().Deserialize<List<ActivityTask>>(response);
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

        public List<int> GetActivitySequences(string claimId, bool isInsert)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getSequense + claimId + param_state + isInsert);

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
                            return new JavaScriptSerializer().Deserialize<List<int>>(response);
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

        public List<SelectListItem> GetStages()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Received", Value = "Received" },
                new SelectListItem() { Text = "Acknowledged", Value = "Acknowledged" },
                new SelectListItem() { Text = "Review", Value = "Review" },
                new SelectListItem() { Text = "Settlement", Value = "Settlement" },
                new SelectListItem() { Text = "Declined", Value = "Declined" },
                new SelectListItem() { Text = "Approved", Value = "Approved" },
                new SelectListItem() { Text = "Not Lodged", Value = "Not Lodged" },
                new SelectListItem() { Text = "Lodged", Value = "Lodged" },
                new SelectListItem() { Text = "Assessor Appointed", Value = "Assessor Appointed" },
                new SelectListItem() { Text = "Expert Appointed", Value = "Expert Appointed" },
                new SelectListItem() { Text = "Indemnity Granted", Value = "Indemnity Granted" },
                new SelectListItem() { Text = "Partial Indemnity Granted", Value = "Partial Indemnity Granted" }

            };
        }


        public async Task<ExecutionResult> SaveActivityDetail(ActivityTaskDetail activityTask, string userId, bool isNew)
        {
            exeReult = new ExecutionResult();
            string result = "";

            try
            {
                string apiUrl = string.Empty;

                if (activityTask.IsNew)
                {
                    apiUrl = ConfigurationManager.AppSettings["apiurl"] + insertActivityTask;
                }
                else
                    apiUrl = ConfigurationManager.AppSettings["apiurl"] + updateActivityTask;

                using (var client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {

                        var jsonString = JsonConvert.SerializeObject(activityTask);
                        var jsonString_userId = JsonConvert.SerializeObject(userId);

                        var json_NextActivityId = JsonConvert.SerializeObject(activityTask.NextActivityId);
                        var json_NextActivityDue = JsonConvert.SerializeObject(activityTask.NextActivityDue);

                        var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                        var content_userId = new StringContent(jsonString_userId, System.Text.Encoding.UTF8, "application/json");

                        var content_NextActivityId = new StringContent(json_NextActivityId, System.Text.Encoding.UTF8, "application/json");
                        var content_NextActivityDue = new StringContent(json_NextActivityDue, System.Text.Encoding.UTF8, "application/json");

                        formData.Add(content, "activityTask");
                        formData.Add(content_userId, "userId");

                        formData.Add(content_NextActivityId, "nextActivityId");
                        formData.Add(content_NextActivityDue, "nextActivityDue");


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


        public ActivityTaskDetail GetGetActivityTaskDetailById(string activityId)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getGetActivityTaskDetailById + activityId);

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
                            return new JavaScriptSerializer().Deserialize<ActivityTaskDetail>(response);
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

       
        public bool DeleteActivity(string claimId, string activityId, int seq)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + deleteRecord + claimId + param_activityId + activityId + param_seq + seq);

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
                            return new JavaScriptSerializer().Deserialize<bool>(response);
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
        

        public bool CompleteActivityTaskWithNextAction(string activityTaskId, string action, int actionSeq, int nextSeq,
            string nextActivityId, string nextActivityDue, string claimId, string userId)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + completeActivityTask + activityTaskId + param_action + action + param_actionSeq + actionSeq 
                    + param_nextSeq + nextSeq + param_nextActivity + nextActivityId + param_nextDue + nextActivityDue + param_user + userId + param_claim + claimId);

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
                            return new JavaScriptSerializer().Deserialize<bool>(response);
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

        public string GetNextTaskDueDate(string nextTaskId)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + getNextTaskDueDate_ + nextTaskId);

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
                            return new JavaScriptSerializer().Deserialize<string>(response);
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


        public async Task<ExecutionResult> UploadEmails(string claimId, string userId, IEnumerable<HttpPostedFileBase> upfiles, string claimNo, string userName)
        {
            string responseClaimId = "";
            exeReult = new ExecutionResult();
            try
            {
                using (var client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {

                        if (upfiles != null)
                        {
                            foreach (HttpPostedFileBase item in upfiles)
                            {
                                if (item != null)
                                {
                                    //formData.Add(new StreamContent(item.InputStream), "Attachment", item.FileName);
                                    formData.Add(CreateFileContent(item.InputStream, HttpUtility.UrlEncode(item.FileName, Encoding.UTF8), item.ContentType));
                                }
                            }
                        }

                        var postResult = await client.PostAsync(ConfigurationManager.AppSettings["apiurl"] + "api/General/UploadEmails?claimId=" + claimId +
                    "&userId=" + userId + "&claimNo=" + claimNo + "&userName=" + userName, formData);
                        string resultContent = await postResult.Content.ReadAsStringAsync();
                    }
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

        private StreamContent CreateFileContent(Stream stream, string fileName, string contentType)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"files\"",
                FileName = "\"" + fileName + "\""
            }; // the extra quotes are key here
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return fileContent;
        }

    }
}
