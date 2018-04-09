using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.SendEmail;
using HonanClaimsWebApi.Services;
using HonanClaimsWebApiAccess1.LoginServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HonanClaimsPortal.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmailWindow(string emailId, string emailAction, string claimRefNo, string claimId, string toAddress)
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

        public async Task<ActionResult> GetClaimEmails(string claimId, string filterText)
        {
            EmailServices emailService = new EmailServices();
            return Json(await emailService.GetAllClaimEmails(claimId, filterText), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetClaimEmail(string emailId, bool withAttachments)
        {
            EmailServices emailService = new EmailServices();
            EmailSimple emailRec = await emailService.GetClaimEmail(emailId, withAttachments);
            var obj = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };

            return new JsonResult() { Data = emailRec, MaxJsonLength = Int32.MaxValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return Json(obj.Serialize(emailRec), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmailSignature()
        {
            //ClaimTeamLoginModel client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            //EmailServices services = new EmailServices();

            //EmailSignature emailSignature = services.GetEmailSignature(client.UserId);
            return View();
        }

        public ActionResult GetEmailSignature()
        {
            ClaimTeamLoginModel client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            EmailServices services = new EmailServices();
            EmailSignature emailSignature = services.GetEmailSignature(client.UserId);
            return Json(emailSignature, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> InsertUpdateEmailSignature()
        {
            try
            {
                var modelStr = Request["model"];
                EmailSignature model = JsonConvert.DeserializeObject<EmailSignature>(modelStr);

                byte[] data = Convert.FromBase64String(model.Signature);
                model.Signature = Encoding.UTF8.GetString(data);

                ClaimTeamLoginModel client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
                model.UserId = client.UserId;
                EmailServices services = new EmailServices();
                bool response = await services.InsertUpdateEmailSignature(model);
                return Json(response, JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }


    }
}