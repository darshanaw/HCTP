using HonanClaimsWebApi.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            ContactListModel contactListModel = new ContactListModel();
            return View(contactListModel);
        }


        [HttpGet]
        public async Task<ActionResult> GetContacts(string AccountName, string ConatctName)
        {
            try
            {
                List<ContactModel> list = new List<ContactModel>();
                ContactRepo contactRepo = new ContactRepo();
                list = await contactRepo.GetAccounts(AccountName, ConatctName);
                //return new JsonResult() { Data = list, MaxJsonLength = 86753090, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async  Task<ActionResult> ContactDetail(string ContactId)
        {
            ContactRepo contactRepo = new ContactRepo();
            ContactModel contactModel = new ContactModel();
            contactModel = await contactRepo.GetAccount(ContactId);
            return View(contactModel);
        }

    }
}