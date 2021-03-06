﻿using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.AccountList;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Models.SendEmail;
using HonanClaimsWebApi.Services;
using HonanClaimsWebApiAccess1.LoginServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    [AuthorizeUser]
    public class HomeController : Controller
    {
        List<DashboardObject> dashboardItems;
        public async Task<ActionResult> Index()
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            HomeServices service = new HomeServices();
            dashboardItems = new List<DashboardObject>();
            dashboardItems = await service.TeamGenerateDashboard(client.UserId);
            ViewBag.DashboardItems = JsonConvert.SerializeObject(dashboardItems);
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
            //var model = Request["model"]; // Errored
            HttpRequestBase request = HttpContext.Request;
            var model = request.Unvalidated.Form.Get("model");
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
            var result = await rep.SendEmail(filee, UserId, dicimod, client.Email, client.FirstName + " " + client.LastName);
            //var result = await rep.SendEmail(filee, UserId, dicimod, "darshana.w@customersystems.com.au", client.FirstName + " " + client.LastName);
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
            var result = await rep.SendPaymentEmail(UserId, dicimod, filee,client.FirstName + " " + client.LastName);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> GetAllClaimsOfTeams(string text_para,string claimRefNo)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            ClaimServices services = new ClaimServices();

            if(string.IsNullOrEmpty(text_para) && !string.IsNullOrEmpty(claimRefNo))
            {
                var d = await services.GetAllClaimsOfTeams(claimRefNo, client.Teams);
                return Json(d.Take(10), JsonRequestBehavior.AllowGet);
            }

            var data = await services.GetAllClaimsOfTeams(text_para, client.Teams);
            return Json(data.Take(10), JsonRequestBehavior.AllowGet);

        }


        public async Task<ActionResult> GetDashboardNumbers()
        {
            dashboardItems = new List<DashboardObject>();
            dashboardItems = (List<DashboardObject>)ViewBag.DashboardItems;
            var data = dashboardItems.Where(x => x.Category == "General").ToList();
            //return DownloadEmail();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadEmail()
        {
            var message = new MailMessage();

            message.From = new MailAddress("from@example.com");
            message.To.Add("someone@example.com");
            message.Subject = "This is the subject";
            message.Body = "This is the body";

            using (var client = new SmtpClient())
            {
                var id = Guid.NewGuid();

                var tempFolder = Path.Combine(Path.GetTempPath(), Assembly.GetExecutingAssembly().GetName().Name);

                tempFolder = Path.Combine(tempFolder, "MailMessageToEMLTemp");

                // create a temp folder to hold just this .eml file so that we can find it easily.
                tempFolder = Path.Combine(tempFolder, id.ToString());

                if (!Directory.Exists(tempFolder))
                {
                    Directory.CreateDirectory(tempFolder);
                }

                client.UseDefaultCredentials = true;
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = tempFolder;
                client.Send(message);

                // tempFolder should contain 1 eml file

                var filePath = Directory.GetFiles(tempFolder).Single();

                // stream out the contents - don't need to dispose because File() does it for you
                var fs = new FileStream(filePath, FileMode.Open);
                return File(fs, System.Net.Mime.MediaTypeNames.Application.Octet, "email.eml");
            }
        }

        public async Task<ActionResult> GetAccountDetail(string accountId)
        {
            AccountListRepo accountListRepo = new AccountListRepo();
            AccountModel model = new AccountModel();
            var data = await accountListRepo.GetAccountDetail(accountId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}