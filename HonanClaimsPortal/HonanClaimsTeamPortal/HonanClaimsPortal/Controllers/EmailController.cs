using HonanClaimsWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmailWindow(string emailId,string emailAction,string claimRefNo,string claimId,string toAddress)
        {
            if (!string.IsNullOrEmpty(emailId))
                ViewBag.EmailId = emailId;

            if (!string.IsNullOrEmpty(emailAction))
                ViewBag.Action = emailAction;

            if (!string.IsNullOrEmpty(claimRefNo))
                ViewBag.ClaimNo = claimRefNo;

            if (!string.IsNullOrEmpty(claimId))
                ViewBag.ClaimIdNo = claimId;

            if (!string.IsNullOrEmpty(toAddress))
                ViewBag.ToAddress = toAddress;

            return View();
        }

        public async Task<ActionResult> GetClaimEmails(string claimId,string filterText)
        {
            EmailServices emailService = new EmailServices();
            return Json(await emailService.GetAllClaimEmails(claimId, filterText), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetClaimEmail(string emailId,bool withAttachments)
        {
            EmailServices emailService = new EmailServices();
            return Json(await emailService.GetClaimEmail(emailId, withAttachments), JsonRequestBehavior.AllowGet);
        }

    }
}