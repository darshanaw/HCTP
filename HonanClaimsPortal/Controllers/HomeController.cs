using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.SendEmail;
using HonanClaimsWebApi.Services;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    [AuthorizeUser]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetContactList(List<string> claimList)
        {
            SendEmailRepo repo = new SendEmailRepo();
            var result = await repo.GetContactList(claimList);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetClaimList()
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            SendEmailRepo rep = new SendEmailRepo();
            var data = await rep.GetActivityClaims(UserId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> SendEmail()
        {
            var model = Request["model"];
            var files = Request.Files;
            var dicimod = Newtonsoft.Json.JsonConvert.DeserializeObject<EmailModel>(model);

            byte[] data = Convert.FromBase64String(Request["html"]);
            string decodedString = Encoding.UTF8.GetString(data);

            dicimod.emailBody = decodedString;
            List<HttpPostedFileBase> filee = new List<HttpPostedFileBase>();
            for (int i = 0; i < files.Count; i++)
            {
                filee.Add(Request.Files[i]);
            }

            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            SendEmailRepo rep = new SendEmailRepo();
            var result = await rep.SendEmail(filee, UserId, dicimod);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPortalRegistrationCount()
        {
            ClaimServices services = new ClaimServices();
            return Json(services.GetPortalRegistrationCount(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetClaimAssigmentCount()
        {
            ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            ClaimServices services = new ClaimServices();
            return Json(services.GetClaimAssigmentCount(login.Teams), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAssignedClaimCount()
        {
            ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            ClaimServices services = new ClaimServices();
            return Json(services.TeamGetAssignedClaimCount(login.UserId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMyActivityCount()
        {
            ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            ClaimServices services = new ClaimServices();
            return Json(services.TeamGetMyActivityCount(login.UserId), JsonRequestBehavior.AllowGet);
        }


    }
}