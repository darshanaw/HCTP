using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Models.TimeslipCheck;
using HonanClaimsWebApi.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        BillingTabModel billingTab;
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
                    AttachmentName = file.FileName,
                    AttachmentDescription = file.FileName,
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
                    AttachmentName = file.FileName,
                    AttachmentDescription = file.FileName,
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


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public  ActionResult _Billing()
        {
            var billingTab = new BillingTabModel();
            //billingTab.ServiceByList = await GetComboDetails(arealist.ServiceBy);
            return PartialView(billingTab);
        }

        private async Task<List<TimeslipDataModel>> GetComboDetails(arealist area)
        {
            try
            {
                List<TimeslipDataModel> list = new List<TimeslipDataModel>();
                TimeSlipCheckRepo timelistcheckrepo = new TimeSlipCheckRepo();
                list = await timelistcheckrepo.GetComboList(area);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}