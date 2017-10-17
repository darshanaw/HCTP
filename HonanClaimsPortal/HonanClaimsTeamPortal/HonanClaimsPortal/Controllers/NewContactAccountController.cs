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
        public async Task<ActionResult> Index(string FirstName,string LastName, string Phone, string EmailAddress, string portalRegRequestId, bool ajax = false, bool fromProtal = false)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;
            ContactAccountModel model = new ContactAccountModel();
            model.PickTitle = await GetPickListData("Title");
            model.PickTypes = await GetPickListData("Account Type");
            model.AccountManagerId = UserId;
            ViewBag.Message = "";

            if (fromProtal)
            {
                model.Contact = FirstName+" "+ LastName;
                
                if (Phone != null && Phone != "null")
                {
                    model.Phone = Phone;
                }
                if (EmailAddress != null && EmailAddress != "null")
                {
                    model.Email = EmailAddress;
                }
                if (portalRegRequestId != null && portalRegRequestId != "null")
                {
                    model.portalRegRequestId = portalRegRequestId;
                }
                model.FromProtal = true;
            }
            else
            {
                model.FromProtal = false;
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

            if (model.FromProtal)
            {
                return RedirectToAction("TeamGetPortalRegistration", "AdminList", new { portalRegRequestId = model.portalRegRequestId });
            }
            else
            {
                return View(rmodel);
            }
           
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