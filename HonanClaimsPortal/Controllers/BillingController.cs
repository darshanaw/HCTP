using HonanClaimsPortal.Helpers;
using HonanClaimsPortal.Models.LookupModel;
using HonanClaimsWebApi.Models;
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
            if (BillingId == null)
            {
                model.IsNew_Billable = true;
                model.Is_Billable = true;
                return View(model);
            }
            model.IsNew_Billable = false;
            BillingRepo billingRepo = new BillingRepo();
            model = await billingRepo.GetTeamGetBillableTimeRecord(BillingId);


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
            list = await accountLookupRepo.GetProtalLogingAccount("");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> TeamInsertTimeslip(BillingModel model)
        {
            BillingRepo billingRepo = new BillingRepo();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            if (model.IsNew_Billable)
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

                var removeitem = ServeiceUserList.SingleOrDefault(r => r.Code == UserId);
                if (removeitem != null)
                {
                    ServeiceUserList.Remove(removeitem);
                }

                ServeiceUserList.Insert(0, removeitem);

                CustomerList.Insert(0, new CustomerUserModel());
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
        public async Task<ActionResult> TeamGetMyBillableTimes(string showMe, string customerId, string serviceUserId, string serviceFromDate, string serviceToDate)
        {
            try
            {
                List<BillingSimpleModel> list = new List<BillingSimpleModel>();
                BillingSimpleRepo billingSimpleRepo = new BillingSimpleRepo();
                list = await billingSimpleRepo.TeamGetMyBillableTimes(showMe, customerId, serviceUserId, serviceFromDate, serviceToDate);
                return Json(new { d = list, total = list.Count }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Partial view
        [HttpGet]
        public ActionResult _TimeslipDetail(string BillingId, string page)
        {
            var model = new BillingModel();
            if (BillingId == null)
            {
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                string UserId = client.UserId;

                model.IsNew_Billable = true;
                model.Is_Billable = true;
                model.Service_By = UserId;
                model.Service_By_Name = client.FirstName + " " + client.LastName;
                model.Service_Date = DateTime.Today;

                if (!string.IsNullOrEmpty(page))
                    model.PageType = page;

                return PartialView(model);
            }
            model.IsNew_Billable = false;
            BillingRepo billingRepo = new BillingRepo();
            model =  billingRepo.GetTeamGetBillableTimeRecord(BillingId).Result;


            model.Billable = (decimal.Round(model.Billable, 2));
            model.Rate = (decimal.Round(model.Rate, 2));
            model.Rate_Per_Unit = (decimal.Round(model.Rate_Per_Unit, 2));
            return PartialView(model);
        }

        public async Task<ActionResult> GetTimeslipDetail(string BillingId)
        {
            var model = new BillingModel();
            if (BillingId == null || BillingId == "null" || BillingId == "undefined" || BillingId == "")
            {
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                string UserId = client.UserId;

                model.IsNew_Billable = true;
                model.Is_Billable = true;
                model.Service_By = UserId;
                model.Service_By_Name = client.FirstName + " " + client.LastName;
                model.Service_Date = DateTime.Today;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            model.IsNew_Billable = false;
            BillingRepo billingRepo = new BillingRepo();
            model = await billingRepo.GetTeamGetBillableTimeRecord(BillingId);


            model.Billable = (decimal.Round(model.Billable, 2));
            model.Rate = (decimal.Round(model.Rate, 2));
            model.Rate_Per_Unit = (decimal.Round(model.Rate_Per_Unit, 2));
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult _TimeslipDetail(BillingModel model)
        {
            try
            {
                //if(model.H_Billingsid==null)
                //{
                //    return PartialView(model);
                //}

                if(model.sStart_Time!=null)
                {
                    model.Start_Time = Convert.ToDateTime(model.sStart_Time);
                }

                if(model.sEnd_Time!=null)
                {
                    model.End_Time = Convert.ToDateTime(model.sEnd_Time);
                }

                BillingRepo billingRepo = new BillingRepo();
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                if (model.IsNew_Billable)
                {

                    var result = billingRepo.TeamInsertTimeslip(model, client.UserId).Result;

                    if (model.PageType == "claimDetail")
                    {
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //if (result)
                        //{
                        //    return RedirectToAction("MyBillableTime", "Billing");
                        //}
                        //return RedirectToAction("MyBillableTime", "Billing");
                        return JavaScript("location.reload(true)");
                    }
                }
                else
                {
                    var result = billingRepo.TeamUpdateTimeslip(model, client.UserId).Result;

                    if (model.PageType == "claimDetail")
                    {
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (result)
                        {
                            //if (Request.IsAjaxRequest())
                            //return PartialView("_TimeslipDetail", model);
                            //return View(model);
                            //return Json(new { error = true, message = RenderViewToString(PartialView("_TimeslipDetail", model)) });
                            //return PartialView(model);
                           // return RedirectToAction("MyBillableTime", "Billing");
                        }
                        //return RedirectToAction("MyBillableTime", "Billing");

                        return JavaScript("location.reload(true)");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }


        //[HttpPost]
        //public ActionResult _TimeslipDetail123(BillingModel model)
        //{
        //    try
        //    {
        //        if (model.sStart_Time != null)
        //        {
        //            model.Start_Time = Convert.ToDateTime(model.sStart_Time);
        //        }

        //        if (model.sEnd_Time != null)
        //        {
        //            model.End_Time = Convert.ToDateTime(model.sEnd_Time);
        //        }

        //        BillingRepo billingRepo = new BillingRepo();
        //        ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
        //        if (model.IsNew)
        //        {

        //            var result = billingRepo.TeamInsertTimeslip(model, client.UserId).Result;
        //            if (result)
        //            {
        //                return null;
        //                //return PartialView("~/Views/Shared/_TimeslipDetail", model);
        //            }
        //            return null;
        //            //return PartialView("~/Views/Shared/_TimeslipDetail", model);
        //        }
        //        else
        //        {
        //            var result = billingRepo.TeamUpdateTimeslip(model, client.UserId).Result;
        //            if (result)
        //            {
        //                return null;
        //                //return PartialView("~/Views/Shared/_TimeslipDetail", model);
        //            }
        //            return null;
        //            //return PartialView("~/Views/Shared/_TimeslipDetail", model);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<ActionResult> GetCurrentUser()
        {
            var model = new CurrentLoggedUserModel();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            model.Name = client.FirstName + "" + client.LastName;
            model.Date = DateTime.Now;
            model.UserId = UserId;
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}