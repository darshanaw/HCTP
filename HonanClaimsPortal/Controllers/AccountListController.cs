using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.AccountList;
using HonanClaimsWebApi.Models.Common;
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
    public class AccountListController : Controller
    {
        PicklistServicecs pickListServices;
        PicklistServicecs PropertyStateList;

        // GET: AccountList
        public async Task<ActionResult>  Index()
        {
            AccountListRepo accountListRepo = new AccountListRepo();
            AccountsModel model = new AccountsModel();
            model.AccountType =await accountListRepo.GetPickListData();
            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> GetAccounts(string AccountName,string  Type)
        {
            try
            {
                List<AccountListModel> list = new List<AccountListModel>();
                AccountListRepo accountListRepo = new AccountListRepo();
                list = await accountListRepo.GetAccounts(AccountName,Type);
                return new JsonResult() { Data = list, MaxJsonLength = 86753090, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                //return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> AccountDetail(string AccountId)
        {
            AccountListRepo accountListRepo = new AccountListRepo();
            AccountModel model = new AccountModel();
            model = await accountListRepo.GetAccountDetail(AccountId);

            //Get Suburbs
            pickListServices = new PicklistServicecs();
            model.PropertySuburbList = pickListServices.GetPickListItems("H_Suburbs");
            model.PropertySuburbList.Insert(0, new PicklistItem());


            //Get Suburbs
            PropertyStateList = new PicklistServicecs();
            //model.BillingMethods = pickListServices.GetPickListItems("Honan Billing Method");
            model.BillingMethods = new List<PicklistItem>();
            model.BillingMethods.Add(new PicklistItem() { Text = "Per Hour", Code = "Per Hour" });
            model.BillingMethods.Add(new PicklistItem() { Text = "Per Claim", Code = "Per Claim" });
            model.BillingMethods.Add(new PicklistItem() { Text = "Per Annum", Code = "Per Annum" });
            model.BillingMethods.Add(new PicklistItem() { Text = "No Billing", Code = "No Billing" });
            //model.BillingMethods.Insert(0, new PicklistItem());


            //Get Billing Methods
            PropertyStateList = new PicklistServicecs();
            model.AccountType = pickListServices.GetPickListItems("Account Type");
            model.AccountType.Insert(0, new PicklistItem());


            ViewBag.AccountId = AccountId;
            return View(model);
        }


        public async Task<ActionResult> UpdateAccountDetail(string AccountId, string Mainphone, string Fax, string Tollfree, string Webaddress, string Type, string Address1, string Address2,
            string Suburb, string State, string PostalCode, string Billing_Method, string Billable_Rate, string Service_Rage)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            AccountUpdateModel model = new AccountUpdateModel();
            model.AccountId = AccountId;
            model.Mainphone = Mainphone;
            model.Fax = Fax;
            model.Tollfree = Tollfree;
            model.Webaddress = Webaddress;
            model.Type = Type;
            model.Address1 = Address1;
            model.Address2 = Address2;
            model.Suburb = Suburb;
            model.State = State;
            model.PostalCode = PostalCode;
            model.Billing_Method = Billing_Method;
            model.Billable_Rate = Billable_Rate;
            model.Service_Rage = Service_Rage;
            AccountListRepo accountListRepo = new AccountListRepo();
            bool result = await accountListRepo.UpdateAccount(model,client.UserId);
            return RedirectToAction("Index");
       }

    }

}