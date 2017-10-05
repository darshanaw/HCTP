﻿using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Billing;
using HonanClaimsWebApi.Models.Claim;
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

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _FileNoteDetail(FileNoteDetailModal model)
        {
            
            documentService = new DocumentService();
            if(!string.IsNullOrEmpty(model.H_FileNotesId_Fn))
            {
                documentService.UpdateFileNoteRecord(model.CreatedBy_Id_Fn, model.ShortDescription_Fn, model.Detail_Fn, model.ClaimId_Fn, model.FileNoteDate_Fn.Value,model.H_FileNotesId_Fn);
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
    }
}