using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.TimeslipCheck;
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
    public class TimeslipCheckController : Controller
    {
        // GET: TimeslipCheck
        public async Task<ActionResult> Index()
        {
            var returnModel = new TimeslipReturnModel();
            returnModel.ClaimTeam = await GetComboDetails(arealist.ClaimTeam);
            returnModel.Account = await GetComboDetails(arealist.Account);
            returnModel.Claim = await GetComboDetails(arealist.Claim);
            returnModel.ServiceBy = await GetComboDetails(arealist.ServiceBy);
            return View(returnModel);
        }


        private async Task<List<TimeslipDataModel>> GetComboDetails(arealist area)
        {
            try
            {
                List<TimeslipDataModel> list = new List<TimeslipDataModel>();
                TimeSlipCheckRepo timelistcheckrepo = new TimeSlipCheckRepo();
                list = await timelistcheckrepo.GetComboList(area);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetMyTimeslipCheck(string claimTeam, string accountId, string serviceBy, string claimId, string fromDate, string toDate)
        {
            try
            {
                List<TimeSlipGridDetailModel> list = new List<TimeSlipGridDetailModel>();
                TimeSlipCheckRepo timelistcheckrepo = new TimeSlipCheckRepo();
                list = await timelistcheckrepo.GetTimeSlipGridData(claimTeam, accountId, serviceBy, claimId, fromDate, toDate);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetBillingRecords(string billingId)
        {
            try
            {
                BillingTimeRecordModel list = new BillingTimeRecordModel();
                TimeSlipCheckRepo timelistcheckrepo = new TimeSlipCheckRepo();
                list = await timelistcheckrepo.GetBillingRecords(billingId);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<ActionResult> MarkAsChecked(string billingId, string serviceDate, string startTimeHH, string startTimeMM, string endTimeHH, string endTimeMM, string claimId)
        {
            try
            {
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                string assignId = client.UserId;

                TimeSlipCheckRepo timelistcheckrepo = new TimeSlipCheckRepo();
                var res = await timelistcheckrepo.MarkAsChecked(billingId, assignId, serviceDate, startTimeHH, startTimeMM, endTimeHH, endTimeMM, claimId);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<ActionResult> MarkAsNonBillable(string billingId, string serviceDate, string startTimeHH, string startTimeMM, string endTimeHH, string endTimeMM, string claimId)
        {
            try
            {
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                string assignId = client.UserId;

                TimeSlipCheckRepo timelistcheckrepo = new TimeSlipCheckRepo();
                var res = await timelistcheckrepo.MarkAsNonBillable(billingId, assignId, serviceDate, startTimeHH, startTimeMM, endTimeHH, endTimeMM, claimId);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<ActionResult> MarkAsCheckedPost(string billingIds)
        {
            try
            {
                List<string> billingIdList = new List<string>();
                billingIdList = billingIds.Split(',').ToList();
                billingIdList = billingIdList.Where(x => !string.IsNullOrEmpty(x)).ToList();

                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                string assignId = client.UserId;

                TimeSlipCheckRepo timelistcheckrepo = new TimeSlipCheckRepo();
                var res = await timelistcheckrepo.MarkAsCheckedPost(billingIdList, assignId);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<ActionResult> MarkAsNonBillablePost(string billingIds)
        {
            try
            {
                List<string> billingIdList = new List<string>();
                billingIdList = billingIds.Split(',').ToList();
                billingIdList = billingIdList.Where(x => !string.IsNullOrEmpty(x)).ToList();

                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                string assignId = client.UserId;

                TimeSlipCheckRepo timelistcheckrepo = new TimeSlipCheckRepo();
                var res = await timelistcheckrepo.MarkAsNonBillablePost(billingIdList,assignId);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}