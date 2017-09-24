using HonanClaimsPortal.Helpers;
using HonanClaimsPortal.Models.LookupModel;
using HonanClaimsWebApi.Models.Billing;
using HonanClaimsWebApi.Models.Common;
using HonanClaimsWebApiAccess1.LoginServices;
using HonanClaimsWebApiAccess1.Models.LookupModel;
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
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string Service_By_Name = client.FirstName+" "+client.LastName;
            string Service_By = client.UserId;


            var model = new BillingModel();
            model.Service_By_Name = Service_By_Name;
            model.Service_By = Service_By;
            model.Billable = true;
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetTeamGetBillableLawyers(string filter)
       {
            List<CommonModel> list = new List<CommonModel>();
            BillingRepo billingRepo = new BillingRepo();
            list = await billingRepo.GetTeamGetBillableLawyers(filter);
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

        [HttpGet]
        public async Task<ActionResult> TeamGetPolicyNoForClaim(string Claimsid)
        {
            try
            {
                List<CommonModel> list = new List<CommonModel>();
                BillingRepo billingRepo = new BillingRepo();
                list = await billingRepo.TeamGetPolicyNoForClaim(Claimsid);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }


        [HttpGet]
        public async Task<ActionResult> TeamGetCustomerForClaim(string Claimsid)
        {
            CommonModel list = new CommonModel();
            BillingRepo billingRepo = new BillingRepo();
            list = await billingRepo.TeamGetCustomerForClaim(Claimsid);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetLookupAccounts()
        {
            List<AccountLookup> list = new List<AccountLookup>();
            AccountLookupRepo accountLookupRepo = new AccountLookupRepo();
            list = await accountLookupRepo.GetProtalLogingAccount();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> TeamInsertTimeslip(BillingModel model)
        {
            BillingRepo billingRepo = new BillingRepo();
            var result =await billingRepo.TeamInsertTimeslip(model);
            return RedirectToAction("TimeslipDetail");
        }
    }
}