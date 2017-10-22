using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Models.ClaimList;
using HonanClaimsWebApi.Models.MyActivity;
using HonanClaimsWebApi.Services;
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
                tem.Text = client.FirstName + " " + client.LastName;
                tem.Order = "";
                activityuser.Add(tem);
            }
            else
            {
                var removeitem = activityuser.SingleOrDefault(r => r.Code == UserId);
                if (removeitem != null)
                {
                    activityuser.Remove(removeitem);
                }
                activityuser.Insert(0, removeitem);

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


        public ActionResult _MyActivityTaskDetail()
        {
            ClaimServices claimServices = new ClaimServices();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            ActivityTaskDetail model = new ActivityTaskDetail();
            DocumentService documentService = new DocumentService();
            PicklistServicecs pickListServices = new PicklistServicecs();

            ViewBag.OwnerType = pickListServices.GetPickListItems("Honan Task Owner Type");
            ViewBag.CurrentUser = client.FirstName + " " + client.LastName;
            ViewBag.CurrentUserId = client.UserId;
            //ViewBag.Sequence = documentService.GetActivitySequences(claimId, true)
            //     .Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() })
            //     .ToList();

            //ViewBag.Stage = documentService.GetStages();
            //model.H_Claimsid_Dtl = claimId;

            List<ActivityTask> activityTasks = new List<ActivityTask>();
            //activityTasks = documentService.GetActivityTasks(claimId, false, false, false, "");
            //ViewBag.MaxActDate = activityTasks.Max(o => o.CompletedDate_Act.HasValue ? o.CompletedDate_Act.Value.ToString("dd/MM/yyyy") : "");
            model.Last_Task_Completed_Dtl_String = !string.IsNullOrEmpty(ViewBag.MaxActDate) ? ViewBag.MaxActDate.ToString() : null;
            model.Last_Task_Completed_Dtl = !string.IsNullOrEmpty(ViewBag.MaxActDate) ? DateTime.ParseExact(ViewBag.MaxActDate, "dd/MM/yyyy", null) : null;


            model.H_Claimsid_Dtl_List = claimServices.GetClaimsForUser(client.UserId);


            return PartialView(model);
        }


        [HttpPost]
        [AjaxOnly]
        public async Task<ActionResult> _MyActivityTaskDetail(ActivityTaskDetail model)
        {

            if (ModelState.IsValid)
            {
                ExecutionResult exeResult = new ExecutionResult();

                if (!string.IsNullOrEmpty(model.Last_Task_Completed_Dtl_String))
                    model.Last_Task_Completed_Dtl = Convert.ToDateTime(model.Last_Task_Completed_Dtl_String);

                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];

                DocumentService documentService = new DocumentService();
                exeResult = await documentService.SaveActivityDetail(model, client.UserId, true);

            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

    }
}