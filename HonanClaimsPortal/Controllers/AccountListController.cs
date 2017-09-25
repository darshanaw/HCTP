using HonanClaimsWebApi.Models.AccountList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    public class AccountListController : Controller
    {
        // GET: AccountList
        public async Task<ActionResult>  Index()
        {
            AccountListRepo accountListRepo = new AccountListRepo();
            AccountModel model = new AccountModel();
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
                //return new JsonResult() { Data = list, MaxJsonLength = 86753090, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}