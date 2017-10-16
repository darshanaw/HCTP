using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models;
using HonanClaimsWebApi.Models.Contact;
using HonanClaimsWebApiAccess1.LoginServices;
using HonanClaimsWebApiAccess1.Models.TeamGetPortalRegistration;
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
        public async Task<ActionResult> Index(string ContactName,string Phone,string Email,string portalRegRequestId, bool ajax = false)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            ContactAccountModel model = new ContactAccountModel();
            model.PickTitle = await GetPickListData("Title");
            model.PickTypes = await GetPickListData("Account Type");
            model.AccountManagerId = UserId;
            ViewBag.Message = "";

            if(ContactName!=null && ContactName!="null")
            {
                model.Contact = ContactName;
            }
            if(Phone!=null && Phone !="null")
            {
                model.Phone = Phone;
            }
            if(Email!=null && Email!="null")
            {
                model.Email = Email;
            }
            if(portalRegRequestId!=null && portalRegRequestId!="null")
            {
                ViewBag.portalRegRequestId = portalRegRequestId;
            }

            if (ajax) return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            return View(model);
        }



        [HttpPost]
        public async Task<ActionResult> Index(ContactAccountModel model)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            ContactAccountRepo conrepo = new ContactAccountRepo();
            var result = await conrepo.AddContactAccount(model, UserId);

            ContactAccountModel rmodel = new ContactAccountModel();
            rmodel.PickTitle = await GetPickListData("Title");
            rmodel.PickTypes = await GetPickListData("Account Type");
            rmodel.AccountManagerId = UserId;

            if (result)
            {
                ViewBag.Message = "Success: Save successful";
            }
            else
            {
                ViewBag.Message = "Error: Save unsuccessful";
            }
            ModelState.Clear();
            return View(rmodel);
            //return RedirectToAction("Index");
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