using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.SendEmail;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //[HttpPost]
        //public async Task<ActionResult> TeamInsertPortalLoginByContact()
        //{

        //}

    }
}