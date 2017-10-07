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
    [AuthorizeUser]
    public class BillingController : Controller
    {
        // GET: Billing
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> TimeslipDetail(string BillingId)
        {
            var model = new BillingModel();
            if (BillingId==null)
            {
                model.IsNew = true;
                model.Is_Billable = true;
                return View(model);
            }
            model.IsNew = false;
            BillingRepo billingRepo = new BillingRepo();           
            model =await billingRepo.GetTeamGetBillableTimeRecord(BillingId);
            

           model.Billable = (decimal.Round(model.Billable, 2));
           model.Rate = (decimal.Round(model.Rate, 2));
           model.Rate_Per_Unit = (decimal.Round(model.Rate_Per_Unit, 2));
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
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            if (model.IsNew)
            {
                
                var result = await billingRepo.TeamInsertTimeslip(model, client.UserId);
                if (result)
                {
                    return RedirectToAction("MyBillableTime");
                }
                return RedirectToAction("TimeslipDetail");
            }
            else
            {
                var result = await billingRepo.TeamUpdateTimeslip(model, client.UserId);
                if (result)
                {
                    return RedirectToAction("MyBillableTime");
                }
                return RedirectToAction("TimeslipDetail");
            }

        }


        //My Biling
        public async Task<ActionResult> MyBillableTime()
        {
            try
            {
                MyBillingModel returnModel = new MyBillingModel();
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                string UserId = client.UserId;
                BillingSimpleRepo billingSimpleRepo = new BillingSimpleRepo();

                var CustomerList = await billingSimpleRepo.GetCustomerList(UserId);
                var ServeiceUserList = await billingSimpleRepo.GetServeiceUserList();
                returnModel.CustomerUserModel = CustomerList;
                returnModel.ServicesUserModel = ServeiceUserList;
                return View(returnModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> TeamGetMyBillableTimes(string showMe,string customerId,string serviceUserId,string serviceFromDate, string serviceToDate)
        {
            try
            {
                List<BillingSimpleModel> list = new List<BillingSimpleModel>();
                BillingSimpleRepo billingSimpleRepo = new BillingSimpleRepo();
                list = await billingSimpleRepo.TeamGetMyBillableTimes(showMe, customerId, serviceUserId, serviceFromDate, serviceToDate);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}