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
            try
            {
                List<string> returnlist = new List<string>();
                SendEmailRepo repo = new SendEmailRepo();
                var result = await repo.GetContactList(claimList);
                foreach (var item in result)
                {
                    if (!string.IsNullOrEmpty(item.Code))
                    {
                        returnlist.Add(item.Code);
                    }
                }
                return Json(returnlist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> GetClaimList()
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            SendEmailRepo rep = new SendEmailRepo();
            var data = await rep.GetActivityClaims(UserId);
            //ClaimServices services = new ClaimServices();
            //var data = services.GetAllOpenClaims("", client.Teams);
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
            //var result = await rep.SendEmail(filee, UserId, dicimod, client.Email);
            var result = await rep.SendEmail(filee, UserId, dicimod, "darshana.w@customersystems.com.au");
            return Json(result, JsonRequestBehavior.AllowGet);
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


        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> SendPaymentEmail()
        {
            HttpPostedFileBase file = null;
            var model = Request["paymentModel"];
            var files = Request.Files;
            var dicimod = Newtonsoft.Json.JsonConvert.DeserializeObject<PaymentEmailModel>(model);


            byte[] data = Convert.FromBase64String(Request["html"]);
            string decodedString = Encoding.UTF8.GetString(data);
            dicimod.EmailBody = decodedString;

            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            dicimod.BccEmail = client.Email;


            List<HttpPostedFileBase> filee = new List<HttpPostedFileBase>();
            for (int i = 0; i < files.Count; i++)
            {
                filee.Add(Request.Files[i]);
            }

            if (string.IsNullOrEmpty(dicimod.PaymentId) && Session[SessionHelper.PaymentAttachment] != null)
            {
                IEnumerable<HttpPostedFileBase> filesAttached = (IEnumerable<HttpPostedFileBase>)Session[SessionHelper.PaymentAttachment];
                if (filesAttached.Count() > 0)
                    filee.Add(filesAttached.ToList()[0]);
            }

            SendEmailRepo rep = new SendEmailRepo();
            var result = await rep.SendPaymentEmail(UserId, dicimod, filee);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> GetAllClaimsOfTeams()
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            ClaimServices services = new ClaimServices();
            var data = await services.GetAllClaimsOfTeams(client.Teams);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}