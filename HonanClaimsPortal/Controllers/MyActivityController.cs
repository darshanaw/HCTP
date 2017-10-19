using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.ClaimList;
using HonanClaimsWebApi.Models.MyActivity;
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
    public class MyActivityController : Controller
    {
        // GET: MyActivity
        public async Task<ActionResult> Index()
        {
            ActivityReturnModel returnModel = new ActivityReturnModel();

            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];


            string UserId = client.UserId;

            var customerList = await getCustomerList(UserId);
            var ownerlist = await getOwnerList();
            var activityclaim = await GetActivityClaims(UserId);
            var activityuser = await GetUsers(client.Teams);

            if (activityuser.Count == 0)
            {
                ActivityUserModel tem = new ActivityUserModel();
                tem.Code = UserId;
                tem.Text = client.FirstName + "" + client.LastName;
                tem.Order = "";
                activityuser.Add(tem);
            }


            returnModel.CustomerList = customerList;
            returnModel.OwnerList = ownerlist;
            returnModel.ActivityClaim = activityclaim;
            returnModel.Activityusers = activityuser;




            return View(returnModel);
        }

        private async Task<List<CustomerModel>> getCustomerList(string userId)
        {
            try
            {
                List<CustomerModel> list = new List<CustomerModel>();
                ClaimListRepo protalLoginAccountsRepo = new ClaimListRepo();
                list = await protalLoginAccountsRepo.GetCustomerList(userId);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task<List<ActivityOwnerModel>> getOwnerList()
        {
            try
            {

                List<ActivityOwnerModel> list = new List<ActivityOwnerModel>();
                MyActivityRepo protalLoginAccountsRepo = new MyActivityRepo();
                list = await protalLoginAccountsRepo.GetOwnerList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<List<ActivityClaimModel>> GetActivityClaims(string userId)
        {
            try
            {
                List<ActivityClaimModel> list = new List<ActivityClaimModel>();
                MyActivityRepo protalLoginAccountsRepo = new MyActivityRepo();
                list = await protalLoginAccountsRepo.GetActivityClaims(userId);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<List<ActivityUserModel>> GetUsers(List<string> TeamList)
        {
            try
            {
                List<ActivityUserModel> list = new List<ActivityUserModel>();
                MyActivityRepo protalLoginAccountsRepo = new MyActivityRepo();
                list = await protalLoginAccountsRepo.GetUsers(TeamList);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetMyActivity(bool overdue, bool nextfiveday, bool showwithdate, string claimId, string owner, string customerId, string searchtext, string assignId)
        {
            try
            {
                if (assignId == "null")
                {
                    ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                    assignId = client.UserId;
                }
                List<MyActivityModels> list = new List<MyActivityModels>();
                MyActivityRepo activityrep = new MyActivityRepo();
                list = await activityrep.GetMyActivity(overdue, nextfiveday, showwithdate, claimId, assignId, owner, customerId, searchtext);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<bool> UpdateDueDate(string activityId, string dateTime)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;

            MyActivityRepo protalLoginAccountsRepo = new MyActivityRepo();
            return await protalLoginAccountsRepo.UpdateDuedate(activityId, UserId, dateTime);
        }

        [HttpGet]
        public async Task<bool> UpdateasComplete(string activityId, string actionis, string claimId)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;

            MyActivityRepo protalLoginAccountsRepo = new MyActivityRepo();
            return await protalLoginAccountsRepo.UpdateAsComplete(activityId, actionis, claimId, UserId);
        }
    }
}