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
using HonanClaimsWebApi.Models.AdminLoginDetail;
using HonanClaimsWebApiAccess1.LoginServices;
using System.Configuration;

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
            model.Manualclaim_Form = "Not set";
            if (adminId != null)
            {
                model = await AdminPortalRecord(adminId);
                model.IsNew = false;
                decimal Fee_Per_Hour;
                if(model.Fee_Per_Hour!=null)
                {
                    if (Decimal.TryParse(model.Fee_Per_Hour, out Fee_Per_Hour))
                        model.Fee_Per_Hour = (decimal.Round(Fee_Per_Hour, 2)).ToString();//string.Format("0:0.00", Fee_Per_Hour);//Fee_Per_Hour.ToString("0.##");
                }

                decimal Fee_Per_Billing_Method;
                if (model.Fee_Per_Billing_Method!=null)
                {
                    if(Decimal.TryParse(model.Fee_Per_Billing_Method,out Fee_Per_Billing_Method))
                    {
                        model.Fee_Per_Billing_Method = (decimal.Round(Fee_Per_Billing_Method, 2)).ToString();//string.Format("0:0.00", Fee_Per_Billing_Method);//Fee_Per_Billing_Method.ToString("0.##");
                    }
                }
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

        [HttpGet]
        public async Task<ActionResult> GetAdminLoginAccount(string portalLoginId)
        {
            AdminLoginsModel list = new AdminLoginsModel();
            AdminLogindetailRepo accountLookupRepo = new AdminLogindetailRepo();
            list = await accountLookupRepo.GetAdminLogin(portalLoginId);
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
            var result = await loginrepo.SaveAdminRecord(claimupload, logoimgupload, model);
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
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;

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


        
       [HttpPost]
        public async Task<ActionResult> UpdateAdminLogin(AdminLoginsModel model)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;

            AdminLogindetailRepo loginrepo = new AdminLogindetailRepo();
            var result = await loginrepo.UpdateAdminLoginRecord(model, UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
        public async Task<ActionResult> GetContactNames(string accountId)
        {
            List<ContactModel> list = new List<ContactModel>();
            AdminLogindetailRepo accountLookupRepo = new AdminLogindetailRepo();
            list = await accountLookupRepo.GetContactNames(accountId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult ManualClaimFormDownload(string fileName, string path)
        {
            if (System.IO.File.Exists(path + "/" + fileName))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(path + "/" + fileName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName.Substring(13, fileName.Length - 13));
            }
            // return File(new byte[0],"");
            return null;
        }

        [HttpGet]
        public async Task<ActionResult> RemoveImage(string adminId)
        {
            AdminLogindetailRepo accountLookupRepo = new AdminLogindetailRepo();
            var data = await accountLookupRepo.RemoveAdminLogo(adminId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}