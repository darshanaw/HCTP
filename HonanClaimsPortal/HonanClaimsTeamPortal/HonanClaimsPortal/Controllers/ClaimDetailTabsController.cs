using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Billing;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Models.TimeslipCheck;
using HonanClaimsWebApi.Services;
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
    public class ClaimDetailTabsController : Controller
    {
        DocumentService documentService;
        List<ClaimAttachmentSimple> attachmentList;
        List<BillingSimpleModel> billableTimesList;
        BillingTabModel billingTab;
        BillableServices billableServices;
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

        public ActionResult AjaxAttachmentsLoad(string claimId,string searchText)
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
                attachmentList = documentService.TeamGetDocs(claimId,"false").Where(o => o.AttachmentDescription.ToUpper().Contains(searchText.ToUpper())).ToList();
            else
                attachmentList = documentService.TeamGetDocs(claimId,"false");
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
            

            billableTimesList = billableServices.GetBillableTimes(claimId, serviceUserId, isBillable,serviceFromDate,serviceToDate,invoiced, invoiceNo);
            return Json(billableTimesList, JsonRequestBehavior.AllowGet);
        }

        private List<TimeslipDataModel> GetComboDetails(BillableServices billableServices, string claimId)
        {
            try
            {
                List<TimeslipDataModel> list = new List<TimeslipDataModel>();              
                list = billableServices.GetServiceByUserList(claimId,"","","","","");
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}