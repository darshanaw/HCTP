using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Billing;
using HonanClaimsWebApi.Models.Common;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    public class BillingController : Controller
    {
        // GET: Billing
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TimeslipDetail()
        {
            ClaimTeamLogin client = (ClaimTeamLogin)Session[SessionHelper.claimTeamLogin];
            string Service_By_Name = client.FirstName+" "+client.LastName;
            string Service_By = client.UserId;


            var model = new BillingModel();
            model.Service_By_Name = Service_By_Name;
            model.Service_By = Service_By;
            model.Billable = true;
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetTeamGetBillableLawyers(string Name)
        {
            List<CommonModel> list = new List<CommonModel>();
            BillingRepo billingRepo = new BillingRepo();
            list = await billingRepo.GetTeamGetBillableLawyers(Name);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> TeamGetClaimNosAssigned(string UserId)
        {
            List<CommonModel> list = new List<CommonModel>();
            BillingRepo billingRepo = new BillingRepo();
            list = await billingRepo.TeamGetClaimNosAssigned(UserId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> TeamGetClaimActivities(string Claimsid)
        {
            List<CommonModel> list = new List<CommonModel>();
            BillingRepo billingRepo = new BillingRepo();
            list = await billingRepo.TeamGetClaimActivities(Claimsid);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}