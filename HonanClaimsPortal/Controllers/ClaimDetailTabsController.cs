using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models;
using HonanClaimsWebApi.Models.Billing;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Models.Common;
using HonanClaimsWebApi.Models.TimeslipCheck;
using HonanClaimsWebApi.Services;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    [AuthorizeUser]
    public class ClaimDetailTabsController : Controller
    {
        DocumentService documentService;
        List<ClaimAttachmentSimple> attachmentList;
        List<BillingSimpleModel> billableTimesList;
        BillingTabModel billingTab;
        BillableServices billableServices;
        ClaimServices claimServices;
        KeyContactDateTabModel keyContactTab;
        PicklistServicecs pickListServices;
        ExecutionResult exeResult;

        // GET: ClaimDetailTabs
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadAttachment(IEnumerable<HttpPostedFileBase> files, string claimId, string userId)
        {
            // The Name of the Upload component is "attachments" 
            foreach (var file in files)
            {
                string fileCreateId = "";
                documentService = new DocumentService();

                ClaimAttachmentSimple attachment = new ClaimAttachmentSimple()
                {
                    AttachmentName = Path.GetFileName(file.FileName),
                    AttachmentDescription = Path.GetFileName(file.FileName),
                    AttachmentType = file.ContentType,
                    UserId = userId,
                    ClaimId = claimId,
                    IsCustomerDoc = "F",
                    //LastUpdated = DateTime.Now,
                    Size = Convert.ToUInt64(file.ContentLength)

                };

                documentService.CreateClaimAttachmentCustomerDoc(attachment, out fileCreateId);

                FileHelper.SaveFile(file, claimId);
            }
            // Return an empty string to signify success
            return Content("");
        }

        [HttpPost]
        public ActionResult UploadCustomerDoc(IEnumerable<HttpPostedFileBase> files, string claimId, string userId)
        {
            // The Name of the Upload component is "attachments" 
            foreach (var file in files)
            {
                string fileCreateId = "";
                documentService = new DocumentService();

                ClaimAttachmentSimple attachment = new ClaimAttachmentSimple()
                {
                    AttachmentName = Path.GetFileName(file.FileName),
                    AttachmentDescription = Path.GetFileName(file.FileName),
                    AttachmentType = file.ContentType,
                    UserId = userId,
                    ClaimId = claimId,
                    IsCustomerDoc = "T",
                    //LastUpdated = DateTime.Now,
                    Size = Convert.ToUInt64(file.ContentLength)

                };

                documentService.CreateClaimAttachmentCustomerDoc(attachment, out fileCreateId);

                FileHelper.SaveFile(file, claimId);
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult AjaxAttachmentsLoad(string claimId, string searchText)
        {
            attachmentList = new List<ClaimAttachmentSimple>();
            attachmentList = GetAttachments(claimId, searchText);
            return Json(attachmentList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxCustomerDocsLoad(string claimId, string CustomerDocssearchTxt)
        {
            attachmentList = new List<ClaimAttachmentSimple>();
            attachmentList = GetCustomerDocs(claimId, CustomerDocssearchTxt);
            return Json(attachmentList, JsonRequestBehavior.AllowGet);
        }

        private List<ClaimAttachmentSimple> GetCustomerDocs(string claimId, string CustomerDocssearchTxt)
        {
            documentService = new DocumentService();
            attachmentList = new List<ClaimAttachmentSimple>();
            if (!string.IsNullOrEmpty(CustomerDocssearchTxt))
                attachmentList = documentService.TeamGetDocs(claimId, "true").Where(o => o.AttachmentDescription.ToUpper().Contains(CustomerDocssearchTxt.ToUpper())).ToList();
            else
                attachmentList = documentService.TeamGetDocs(claimId, "true");
            return attachmentList;

        }

        private List<ClaimAttachmentSimple> GetAttachments(string claimId, string searchText)
        {
            documentService = new DocumentService();
            attachmentList = new List<ClaimAttachmentSimple>();
            if (!string.IsNullOrEmpty(searchText))
                attachmentList = documentService.TeamGetDocs(claimId, "false").Where(o => o.AttachmentDescription.ToUpper().Contains(searchText.ToUpper())).ToList();
            else
                attachmentList = documentService.TeamGetDocs(claimId, "false");
            return attachmentList;

        }

        public FileResult FileDownload(string fileName)
        {
            if (System.IO.File.Exists(ConfigurationManager.AppSettings["FileUploadPath"] + "/" + fileName))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(ConfigurationManager.AppSettings["FileUploadPath"] + "/" + fileName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            // return File(new byte[0],"");
            return null;
        }

        public FileResult FileDownloadByFileName(string fileName,string fileRealName)
        {
            if (System.IO.File.Exists(ConfigurationManager.AppSettings["FileUploadPath"] + "/" + fileName))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(ConfigurationManager.AppSettings["FileUploadPath"] + "/" + fileName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileRealName);
            }
            // return File(new byte[0],"");
            return null;
        }


        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult _Billing(string claimId)
        {
            billableServices = new BillableServices();
            var billingTab = new BillingTabModel();
            billingTab.ServiceByList = GetComboDetails(billableServices, claimId);
            billingTab.YesNoList = new List<string>() { "", "Yes", "No" };
            billingTab.InvoiceNumberList = billableServices.TeamGetInvoiceNumbersPerClaim(claimId);
            return PartialView(billingTab);
        }

        public ActionResult AjaxBillableTimesLoadLoad(string claimId, string serviceUserId, string isBillable, string serviceFromDate, string serviceToDate, string invoiced, string invoiceNo)
        {
            billableTimesList = new List<BillingSimpleModel>();
            billableServices = new BillableServices();

            if (invoiced == "Yes")
                invoiced = "true";
            else if (invoiced == "No")
                invoiced = "false";


            billableTimesList = billableServices.GetBillableTimes(claimId, serviceUserId, isBillable, serviceFromDate, serviceToDate, invoiced, invoiceNo);
            return Json(billableTimesList, JsonRequestBehavior.AllowGet);
        }

        private List<TimeslipDataModel> GetComboDetails(BillableServices billableServices, string claimId)
        {
            try
            {
                List<TimeslipDataModel> list = new List<TimeslipDataModel>();
                list = billableServices.GetServiceByUserList(claimId, "", "", "", "", "");
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult _FileNoteDetail(string claimId, string Claim_Reference_Num)
        {
            FileNoteDetailModal model = new FileNoteDetailModal();
            claimServices = new ClaimServices();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            model.CreatedDate_Fn = DateTime.Now;
            model.CreatedBy_Id_Fn = client.UserId;
            model.CreatedBy_Fn = client.FirstName + " " + client.LastName;
            model.ClaimId_Fn = claimId;
            model.ClaimRefNo_Fn = Claim_Reference_Num;
            model.RefnuberList_Fn = claimServices.GetClaimsForUser(client.UserId);
            model.FileNoteDate_Fn = DateTime.Now;
            ViewBag.CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
            return PartialView(model);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult _FileNoteDetail(FileNoteDetailModal model)
        {

            documentService = new DocumentService();
            if (!string.IsNullOrEmpty(model.H_FileNotesId_Fn))
            {
                documentService.UpdateFileNoteRecord(model.CreatedBy_Id_Fn, model.ShortDescription_Fn, model.Detail_Fn, model.ClaimId_Fn, model.FileNoteDate_Fn.Value, model.H_FileNotesId_Fn);
            }
            else
                documentService.CreateFileNoteRecord(model.CreatedBy_Id_Fn, model.ShortDescription_Fn, model.Detail_Fn, model.ClaimId_Fn, model.FileNoteDate_Fn.Value);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxFileNotesLoad(string searchText, string claimId)
        {
            documentService = new DocumentService();
            List<FileNote> fileNotes = new List<FileNote>();
            fileNotes = documentService.GetFileNotes(searchText, claimId);
            return Json(fileNotes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getFuleNoteById(string fileNoteId)
        {
            documentService = new DocumentService();
            FileNote fileNote = new FileNote();
            fileNote = documentService.GetFileNoteById(fileNoteId);
            return Json(fileNote, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _KeyContactsDates(string claimId,string claimRefNo)
        {
            KeyContactDateTabModel model = new KeyContactDateTabModel();
            ViewData["claimId"] = claimId;
            model.ClaimId_KCD_Tab = claimId;
            model.Claim_Ref_No_KCD_Tab = claimRefNo;
            return PartialView(model);
        }

        public ActionResult AjaxGetKeyContacts(string claimId)
        {
            KeyContactDateServices services = new KeyContactDateServices();
            List<KeyContact> keyContacts = services.GetKeyContacts(claimId);
            return Json(keyContacts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxGetKeyDates(string claimId)
        {
            KeyContactDateServices services = new KeyContactDateServices();
            List<KeyDate> keyDates = services.GetKeyDates(claimId);
            return Json(keyDates, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxUpdateKeyDates(KeyDate KeyDate)
        {
            KeyContactDateServices services = new KeyContactDateServices();
            //List<KeyDate> keyDates = services.GetKeyDates(claimId);
            return Json(KeyDate, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _ActivityTasks(string claimId)
        {
            //ActivityTask model = new ActivityTask();
            pickListServices = new PicklistServicecs();
            ViewData["OwnerType"] = pickListServices.GetPickListItems("Honan Task Owner Type");
            ViewData["claimId"] = claimId;

            return PartialView();
        }

        public ActionResult _ActivityTaskDetail(string claimId)
        {
            claimServices = new ClaimServices();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            ActivityTaskDetail model = new ActivityTaskDetail();
            documentService = new DocumentService();
            pickListServices = new PicklistServicecs();

            ViewBag.OwnerType = pickListServices.GetPickListItems("Honan Task Owner Type");
            ViewBag.CurrentUser = client.FirstName + " " + client.LastName;
            ViewBag.CurrentUserId = client.UserId;
            ViewBag.Sequence = documentService.GetActivitySequences(claimId, true)
                 .Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() })
                 .ToList();

            ViewBag.Stage = documentService.GetStages();
            model.H_Claimsid_Dtl = claimId;

            List<ActivityTask> activityTasks = new List<ActivityTask>();
            activityTasks = documentService.GetActivityTasks(claimId, false, false, false, "");
            ViewBag.MaxActDate = activityTasks.Max(o => o.CompletedDate_Act.HasValue ? o.CompletedDate_Act.Value.ToString("dd/MM/yyyy") : "");
            model.Last_Task_Completed_Dtl_String = !string.IsNullOrEmpty(ViewBag.MaxActDate) ? ViewBag.MaxActDate.ToString() : null;
            model.Last_Task_Completed_Dtl =!string.IsNullOrEmpty(ViewBag.MaxActDate) ? DateTime.ParseExact(ViewBag.MaxActDate, "dd/MM/yyyy", null)  : null;


            model.H_Claimsid_Dtl_List = claimServices.GetClaimsForUser(client.UserId);


            return PartialView(model);
        }

        public ActionResult GetSequencyList(string claimId,bool isNew)
        {
            documentService = new DocumentService();
           var list =  documentService.GetActivitySequences(claimId, isNew)
                .Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() })
                .ToList();

            return Json(list , JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLast_Task_CompletedDate(string claimId)
        {
            documentService = new DocumentService();
            List<ActivityTask> activityTasks = new List<ActivityTask>();
            activityTasks = documentService.GetActivityTasks(claimId, false, false, false, "");
            string maxDate = activityTasks.Max(o => o.CompletedDate_Act.HasValue ? o.CompletedDate_Act.Value.ToString("dd/MM/yyyy") : "");

            return Json(maxDate, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateNextActivityWorkFlow(string activityTaskId, string actionName, int actionSeq, int nextSeq,
            string nextActivityId, string nextActivityDue, string claimId)
        {
            //DateTime nextActivityDue_ = Convert.ToDateTime(nextActivityDue); //DateTime.Now;
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            documentService = new DocumentService();
            bool res = documentService.CompleteActivityTaskWithNextAction(activityTaskId, actionName, actionSeq, nextSeq,
             nextActivityId, nextActivityDue, claimId, client.UserId);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteActivity(string claimId, string activityId, int seq)
        {
            documentService = new DocumentService();
            bool res = documentService.DeleteActivity(claimId, activityId, seq);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult getThisTaskDueDate(string date,int slaCount)
        {
            DateTime res = DateTime.Now;
            if(!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out res))
            {
                if(slaCount < 1)
                    return Json("", JsonRequestBehavior.AllowGet);

                while (slaCount > 0)
                {
                    res = res.AddDays(1);

                    if (res.DayOfWeek != DayOfWeek.Saturday && res.DayOfWeek != DayOfWeek.Sunday)
                    {
                        slaCount -= 1;
                    }
                }

                return Json(res.ToString("dd/MM/yyyy"), JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public async Task<ActionResult> _ActivityTasksDetail(ActivityTaskDetail model)
        {
            
            if(ModelState.IsValid)
            {
                exeResult = new ExecutionResult();

                if (!string.IsNullOrEmpty(model.Last_Task_Completed_Dtl_String))
                    model.Last_Task_Completed_Dtl = Convert.ToDateTime(model.Last_Task_Completed_Dtl_String);

                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];

                documentService = new DocumentService();
                exeResult = await documentService.SaveActivityDetail(model, client.UserId, true);
                
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxActivityTasksLoad(string claimId, string incompleteOnly, string completedOnly, string showOverDue, string owner)
        {
            documentService = new DocumentService();
            List<ActivityTask> activityTasks = new List<ActivityTask>();
            activityTasks = documentService.GetActivityTasks(claimId, incompleteOnly == "true" ? true : false, completedOnly == "true" ? true : false, showOverDue == "true" ? true : false, owner);
            var maxDate = activityTasks.Max(o => o.CompletedDate_Act.HasValue? o.CompletedDate_Act.Value.ToString("dd/MM/yyyy") : "");
            return Json(activityTasks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxActivityWorkFlowLoad(string claimId, int completingActionSeq)
        {
            documentService = new DocumentService();
            List<ActivityTask> activityTasks = new List<ActivityTask>();
            activityTasks = documentService.GetActivitiesForWorkFlow(claimId, completingActionSeq);
            return Json(activityTasks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNextTaskDueDate(string nextTaskId)
        {
            documentService = new DocumentService();
            string dueDate = documentService.GetNextTaskDueDate(nextTaskId);
            return Json(dueDate, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGetActivityTaskDetailById(string activityId)
        {
            documentService = new DocumentService();
            ActivityTaskDetail activityTask = new ActivityTaskDetail();
            activityTask = documentService.GetGetActivityTaskDetailById(activityId);
            activityTask.Last_Task_Completed_Dtl_String = activityTask.Last_Task_Completed_Dtl.HasValue ? activityTask.Last_Task_Completed_Dtl.Value.ToString("dd/MM/yyyy") : "";
            return Json(activityTask, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _PaymentDetail(string claimId, string Claim_Reference_Num)
        {
            Session[SessionHelper.PaymentAttachment] = null;

            Payment model = new Payment();
            claimServices = new ClaimServices();
            pickListServices = new PicklistServicecs();

            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];

            model.ClaimRefNo_Payment = Claim_Reference_Num;
            model.ClaimRefNo_Payment_List = claimServices.GetClaimsForUser(client.UserId);
            model.Payee_Type_List = pickListServices.GetPickListItems("Honan Payee type");
            model.Gst = "10";
            model.Gst_Included = true;
            model.Payment_Status_List = pickListServices.GetPickListItems("Honan Payment Status");
            model.Payment_Type_List = pickListServices.GetPickListItems("Honan Payment Type");
            model.Payment_Method_List = pickListServices.GetPickListItems("Honan Payment Method");
            model.H_Claimsid = claimId;
            model.IsNew = true;

            return PartialView(model);
        }

        [HttpPost]
        [AjaxOnly]
        public async Task<ActionResult> _PaymentDetail(Payment model, IEnumerable<HttpPostedFileBase> files)
        {
            if(model.Is_Settlement)
            {
                if (ModelState.ContainsKey("Payee_Type"))
                    ModelState["Payee_Type"].Errors.Clear();

                if (ModelState.ContainsKey("Payee_Contact_Name"))
                    ModelState["Payee_Contact_Name"].Errors.Clear();

                if (ModelState.ContainsKey("Date_Invoice_Received"))
                    ModelState["Date_Invoice_Received"].Errors.Clear();

                if (ModelState.ContainsKey("Payee_Account_Name"))
                    ModelState["Payee_Account_Name"].Errors.Clear();

            }
            else
            {
                if (ModelState.ContainsKey("Payment_Amount"))
                    ModelState["Payment_Amount"].Errors.Clear();

                if (ModelState.ContainsKey("Payment_Date"))
                    ModelState["Payment_Date"].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                documentService = new DocumentService();
                exeResult = new ExecutionResult();

                if (Session[SessionHelper.PaymentAttachment] != null)
                    files = (IEnumerable<HttpPostedFileBase>)Session[SessionHelper.PaymentAttachment];

                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];

                if (model.Is_Settlement && model.Payment_Amount.HasValue)
                {
                    model.Total_Gross = (decimal)model.Payment_Amount;
                    model.Total_Net = (decimal)model.Payment_Amount;
                }

                //  if (model.IsNew)
                //  {
                exeResult = await documentService.CreatePaymentDetailRecord(model, client.UserId, files);
                //}
                //else
                //documentService.CreateFileNoteRecord(model.CreatedBy_Id_Fn, model.ShortDescription_Fn, model.Detail_Fn, model.ClaimId_Fn, model.FileNoteDate_Fn.Value);
                Session[SessionHelper.PaymentAttachment] = null;
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPaymentById(string paymentId)
        {
            documentService = new DocumentService();
            Payment payment = new Payment();
            payment = documentService.getPaymentById(paymentId);

            if (payment.Is_Settlement)
            {
                payment.Payment_Amount = Convert.ToDouble(payment.Total_Gross.Value);
            }

            return Json(payment, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxPaymentDetailsLoad(string claimId, string payeeId, string invoicedDate, string status, string invoiceNo)
        {
            documentService = new DocumentService();
            List<PaymentSimple> payments = new List<PaymentSimple>();
            payments = documentService.GetPaymentDetails(claimId, payeeId, invoicedDate, status, invoiceNo);
            return Json(payments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxPickListPayeesLoad(string claimId, string payeeId, string invoicedDate, string status, string invoiceNo)
        {
            documentService = new DocumentService();
            List<PaymentSimple> payments = new List<PaymentSimple>();
            payments = documentService.GetPaymentDetails(claimId, payeeId, invoicedDate, status, invoiceNo);
            List<PicklistItem> payee = new List<PicklistItem>();

            var obj = (from i in payments
                       select new { i.Payee_Account_Id, i.Payee_Account_Name })
                       .Distinct()
                       .OrderBy(i => i.Payee_Account_Name);

            foreach (var item in obj)
            {
                payee.Add(new PicklistItem() { Code = item.Payee_Account_Id, Text = item.Payee_Account_Name });
            }

            return Json(payee, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DeleteKeyContact(string keyContactId)
        {
            KeyContactDateServices service = new KeyContactDateServices();
            return Json(service.DeleteKeyContact(keyContactId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteKeyDate(string keyDateId)
        {
            KeyContactDateServices service = new KeyContactDateServices();
            return Json(service.DeleteKeyDate(keyDateId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadPaymentFile(IEnumerable<HttpPostedFileBase> files)
        {
            Session[SessionHelper.PaymentAttachment] = files;
            return Content("");
        }

        public ActionResult _KeyContactDetail(string claimId, string keyContactId)
        {
            KeyContact model = new KeyContact();

            pickListServices = new PicklistServicecs();
            model.DescriptionList = pickListServices.GetPickListItems("Key Contact Description");

            return PartialView(model);
        }


        public ActionResult GetKeyContactById(string keyContactId)
        {
            KeyContactDateServices service = new KeyContactDateServices();
            KeyContact model = service.GetKeyContact(keyContactId);

            pickListServices = new PicklistServicecs();
            model.DescriptionList = pickListServices.GetPickListItems("Key Contact Description");

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public async Task<ActionResult> _KeyContactDetail(KeyContact model)
        {

            if (ModelState.IsValid)
            {
                ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
                KeyContactDateServices service = new KeyContactDateServices();

                bool result; 

                if (string.IsNullOrEmpty(model.H_Keycontactsid))
                    result = await service.InsertKeyContact(model, login.UserId);
                else
                    result = await service.UpdateKeyContact(model, login.UserId);

                if (result)
                    return Json("success", JsonRequestBehavior.AllowGet);

            }
            pickListServices = new PicklistServicecs();
            model.DescriptionList = pickListServices.GetPickListItems("Key Contact Description");

            return PartialView(model);
        }

        public ActionResult AjaxGetAssignedClaimNos(string claimNo)
        {
            ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            claimServices = new ClaimServices();
            List<CRMPicklistItem> items = claimServices.GetClaimsForUser(login.UserId).Where(x => x.Text.ToLower().Contains(claimNo.ToLower())).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AjaxGetAssignedClaimNosLookUp(jQueryDataTableParamModel param)
        {
            ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            claimServices = new ClaimServices();
            List<CRMPicklistItem> items = new List<CRMPicklistItem>();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                items = claimServices.GetClaimsForUser(login.UserId).Where(x => x.Text.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }

            IEnumerable<CRMPicklistItem> filteredRecords = items;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<CRMPicklistItem, string> orderingFunction = (c => sortColumnIndex == 1 ? c.Code :
                                                                sortColumnIndex == 2 ? c.Text :
                                                                sortColumnIndex == 3 ? c.Order :
                                                                c.Text);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            //if (!string.IsNullOrEmpty(param.sSearch))
            //{
            //    filteredRecords = filteredRecords
            //                .Where(c => c.Text.ToUpper().Contains(param.sSearch.ToUpper()));
            //    //           ||
            //    //           c.Town.Contains(param.sSearch));
            //}



            List<string[]> aData = new List<string[]>();

            foreach (CRMPicklistItem item in filteredRecords)
            {
                string[] arry = new string[] { item.Code, item.Text };
                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);

        }


        public ActionResult AjaxGetAllClaimNosLookUp(jQueryDataTableParamModel param)
        {
            ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            claimServices = new ClaimServices();
            List<CRMPicklistItem> items = new List<CRMPicklistItem>();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                items = claimServices.GetAllOpenClaims(param.sSearch, login.Teams);
                //if (items != null)
                //    items = items.Where(x => x.Text.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }

            IEnumerable<CRMPicklistItem> filteredRecords = items;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<CRMPicklistItem, string> orderingFunction = (c => sortColumnIndex == 1 ? c.Code :
                                                                sortColumnIndex == 2 ? c.Text :
                                                                sortColumnIndex == 3 ? c.Order :
                                                                c.Text);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            //if (!string.IsNullOrEmpty(param.sSearch))
            //{
            //    filteredRecords = filteredRecords
            //                .Where(c => c.Text.ToUpper().Contains(param.sSearch.ToUpper()));
            //    //           ||
            //    //           c.Town.Contains(param.sSearch));
            //}



            List<string[]> aData = new List<string[]>();

            foreach (CRMPicklistItem item in filteredRecords)
            {
                string[] arry = new string[] { item.Code, item.Text };
                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);

        }


        public ActionResult _KeyDateDetail(string claimId)
        {
            KeyDate model = new KeyDate();
            model.H_Claimsid = claimId;
            pickListServices = new PicklistServicecs();
            model.Key_Date_Description_List = pickListServices.GetPickListItems("Key Date Description");

            return PartialView(model);
        }

        [HttpPost]
        [AjaxOnly]
        public async Task<ActionResult> _KeyDateDetail(KeyDate model)
        {

            if (ModelState.IsValid)
            {
                ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
                KeyContactDateServices service = new KeyContactDateServices();

                bool result;

                if (string.IsNullOrEmpty(model.H_Keydatesid))
                    result = await service.InsertKeyDate(model, login.UserId);
                else
                    result = await service.UpdateKeyDate(model, login.UserId);

                if (result)
                    return Json("success", JsonRequestBehavior.AllowGet);

            }
            pickListServices = new PicklistServicecs();
            model.Key_Date_Description_List = pickListServices.GetPickListItems("Key Date Description");

            return PartialView(model);
        }

        public ActionResult AjaxGetKeyDateNextSequence(string claimId)
        {
            KeyContactDateServices service = new KeyContactDateServices();
            return Json(service.GetKeyDateNextSequnce(claimId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetKeyDateById(string keyDateId)
        {
            KeyContactDateServices service = new KeyContactDateServices();
            KeyDate model = service.GetKeyDate(keyDateId);

            pickListServices = new PicklistServicecs();
            model.Key_Date_Description_List = pickListServices.GetPickListItems("Key Date Description");

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLiabilityResSourceForClaim(string claimId)
        {
            claimServices = new ClaimServices();
            var liability = claimServices.GetLiabilityResSourceForClaim(claimId);
            return Json(liability, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult UpdateFinancials(decimal liabilityResSource, string claimId)
        {
            ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;

            claimServices = new ClaimServices();
            var liability = claimServices.UpdateFinancials(liabilityResSource, 0, claimId, login.UserId);

            return Json(liability, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CloseClaim(string claimId,bool skipAllActivities)
        {
            ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            claimServices = new ClaimServices();
            var response = claimServices.CloseClaim(claimId, login.UserId, skipAllActivities);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UploadEmails(IEnumerable<HttpPostedFileBase> files, string claimId, string userId)
        {
            DocumentService service = new DocumentService();
            var result = await service.UploadEmails(claimId, userId, files);
            return Json(result.IsSuccess, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SkipActivity(string activityTaskId,string stage,bool skip)
        {
            ClaimServices service = new ClaimServices();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];

            return Json(service.SkipActivity(activityTaskId, client.UserId, stage,skip), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateActivityDueDate(string dueDate,string activityTaskId)
        {
            ClaimServices service = new ClaimServices();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];

            return Json(service.UpdateActivityDueDate(dueDate,activityTaskId, client.UserId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CompleteActivity(string activityTaskId, string stage,bool complete)
        {
            ClaimServices service = new ClaimServices();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];

            return Json(service.CompleteActivity(activityTaskId, client.UserId, stage, complete), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPendingActionCount(string claimsId)
        {
            ClaimServices service = new ClaimServices();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            var count = service.GetPendingActionCount(claimsId);
            return Json(count, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApplyClaimTemplate(string claimId, string teamName)
        {
            ClaimServices service = new ClaimServices();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            var count = service.ApplyClaimTemplate(claimId, teamName);
            return Json(count, JsonRequestBehavior.AllowGet);
        }
    }

}