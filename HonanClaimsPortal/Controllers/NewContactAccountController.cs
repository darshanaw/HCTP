using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models;
using HonanClaimsWebApi.Models.Contact;
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
    public class NewContactAccountController : Controller
    {
        // GET: NewContactAccount
        public async Task<ActionResult> Index()
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            ContactAccountModel model = new ContactAccountModel();
            model.PickTitle = await GetPickListData("Title");
            model.PickTypes = await GetPickListData("Account Type");
            model.AccountManagerId = UserId;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewContact(ContactAccountModel model)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            ContactAccountRepo conrepo = new ContactAccountRepo();
            var result = await conrepo.AddContactAccount(model, UserId);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetAccount()
        {
            ContactAccountRepo conrepo = new ContactAccountRepo();
            var result = await conrepo.GetAccount();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<List<PickListData>> GetPickListData(string name)
        {
            ContactAccountRepo conrepo = new ContactAccountRepo();
            var result = await conrepo.GetPickListData(name);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult> GetManagerList()
        {
            ContactAccountRepo conrepo = new ContactAccountRepo();
            var result = await conrepo.GetManagerList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}