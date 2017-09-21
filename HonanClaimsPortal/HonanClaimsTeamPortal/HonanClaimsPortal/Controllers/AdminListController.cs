using HonanClaimsPortal.Models.LookupModel;
using HonanClaimsPortal.Models.ProtalLoginAccounts;
using HonanClaimsPortal.Models.ProtalLogingRequest;
using HonanClaimsWebApiAccess1.Models.ProtalLogingRequest;
using HonanClaimsWebApiAccess1.Models.TeamGetPortalRegistration;
using HonanClaimsWebApiAccess1.Models.AdminLoginDetail;
using HonanClaimsWebApiAccess1.Models.LookupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HonanClaimsPortal.Helpers;

namespace HonanClaimsPortal.Controllers
{
    [AuthorizeUser]
    public class AdminListController : Controller
    {
        // GET: AdminList
        public ActionResult AdminList()
        {
            return View();
        }

        public async Task<ActionResult> AdminDetail(string adminId = null)
        {
            var model = new CustomerPortalAdminModel();
            model.IsNew = true;
            if (adminId != null)
            {
               model =  await AdminPortalRecord(adminId);
                model.IsNew = false;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetProtalLogingAccount()
        {
            try
            {
                List<ProtalLoginAccountsModel> list = new List<ProtalLoginAccountsModel>();
                ProtalLoginAccountsRepo protalLoginAccountsRepo = new ProtalLoginAccountsRepo();
                list = await protalLoginAccountsRepo.GetProtalLogingAccount();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpGet]
        public async Task<ActionResult> GetNewProtalLogingRequest()
        {
            try
            {
                List<ProtalLogingRequestModel> list = new List<ProtalLogingRequestModel>();
                ProtalLogingRequestRepo protalLogingRequestRepo = new ProtalLogingRequestRepo();
                list = await protalLogingRequestRepo.GetProtalLogingAccount();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<ActionResult> PortalCutomerAdminDetail()
        {
            try
            {
                List<ProtalLogingRequestModel> list = new List<ProtalLogingRequestModel>();
                ProtalLogingRequestRepo protalLogingRequestRepo = new ProtalLogingRequestRepo();
                list = await protalLogingRequestRepo.GetProtalLogingAccount();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public async Task<ActionResult> TeamGetPortalRegistration(string portalRegRequestId)
        {
            TeamGetPortalRegistrationModel obj = new TeamGetPortalRegistrationModel();
            TeamGetPortalRegistrationRepo teamGetPortalRegistrationRepo = new TeamGetPortalRegistrationRepo();
            obj = await teamGetPortalRegistrationRepo.GetTeamGetPortalRegistration(portalRegRequestId);
            ViewBag.portalRegRequestId = portalRegRequestId.ToString();
            return View(obj);
            //return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> TeamInsertPortalLoginByContact(string portalRegRequestId, string matchingContactId, string userId)
        {
            TeamGetPortalRegistrationRepo teamGetPortalRegistrationRepo = new TeamGetPortalRegistrationRepo();
            var result = await teamGetPortalRegistrationRepo.TeamInsertPortalLoginByContact(portalRegRequestId, matchingContactId, userId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetLookupAccounts()
        {
            List<AccountLookup> list = new List<AccountLookup>();
            AccountLookupRepo accountLookupRepo = new AccountLookupRepo();
            list = await accountLookupRepo.GetProtalLogingAccount();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetOnlyBilling(string Name)
        {
            List<AccountLookup> list = new List<AccountLookup>();
            AccountLookupRepo accountLookupRepo = new AccountLookupRepo();
            list = await accountLookupRepo.GetProtalLogingAccount();
            var filterList = list.Where(s => s.AccountName == Name).Select(x => new { billing = x.BillingMethod }).ToList();
            return Json(filterList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetAdminLogins()
        {
            List<AccountLookup> list = new List<AccountLookup>();
            AccountLookupRepo accountLookupRepo = new AccountLookupRepo();
            list = await accountLookupRepo.GetProtalLogingAccount();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetAdminLoginAccounts(string adminId)
        {
            List<AdminLoginsModel> list = new List<AdminLoginsModel>();
            AdminLogindetailRepo accountLookupRepo = new AdminLogindetailRepo();
            list = await accountLookupRepo.GetAdminLogins(adminId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private async Task<CustomerPortalAdminModel> AdminPortalRecord(string adminId)
        {
            CustomerPortalAdminModel list = new CustomerPortalAdminModel();
            AdminLogindetailRepo accountLookupRepo = new AdminLogindetailRepo();
            list = await accountLookupRepo.GetAdminRecord(adminId);
            return list;
        }

        [HttpPost]
        public async Task<ActionResult> portalRegRequestId(string portalRegRequestId)
        {
            TeamGetPortalRegistrationRepo teamGetPortalRegistrationRepo = new TeamGetPortalRegistrationRepo();
            var result = await teamGetPortalRegistrationRepo.TeamDiscardLoginRequest(portalRegRequestId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddPortalAdminRecord(HttpPostedFileBase claimupload, HttpPostedFileBase logoimgupload, CustomerPortalAdminModel model)
        {
            AdminLogindetailRepo loginrepo = new AdminLogindetailRepo();
            var result =  await loginrepo.SaveAdminRecord(claimupload, logoimgupload, model);
            if (result == true)
            {
                return RedirectToAction("AdminList");
            }
            else
            {
                return RedirectToAction("AdminDetail");
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddAdminLogin(AdminLoginsModel model)
        {
            string UserId = "U6UJ9A000009";//Session["UserId"];
            AdminLogindetailRepo loginrepo = new AdminLogindetailRepo();
            var result = await loginrepo.SaveAdminLoginRecord(model, UserId);
            if (result == true)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

    }
}