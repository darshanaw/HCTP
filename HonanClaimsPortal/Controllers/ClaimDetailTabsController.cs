using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    public class ClaimDetailTabsController : Controller
    {
        DocumentService documentService;
        List<ClaimAttachmentSimple> attachmentList;
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

        private List<ClaimAttachmentSimple> GetAttachments(string claimId, string searchText)
        {
            documentService = new DocumentService();
            attachmentList = new List<ClaimAttachmentSimple>();
            if (!string.IsNullOrEmpty(searchText))
                attachmentList = documentService.GetCustomerDocs(claimId).Where(o => o.AttachmentDescription.Contains(searchText)).ToList();
            else
                attachmentList = documentService.GetCustomerDocs(claimId);
            return attachmentList;

        }
    }
}