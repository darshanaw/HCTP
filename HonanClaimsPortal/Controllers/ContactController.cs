using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Common;
using HonanClaimsWebApi.Models.Contact;
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
    public class ContactController : Controller
    {

        PicklistServicecs pickListServices;
        PicklistServicecs PropertyStateList;
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
            ContactModel model = new ContactModel();
            model = await contactRepo.GetAccount(ContactId);
            //Get Suburbs
            pickListServices = new PicklistServicecs();
            model.PropertySuburbList = pickListServices.GetPickListItems("H_Suburbs");
            model.PropertySuburbList.Insert(0, new PicklistItem());


            //Get Suburbs
            PropertyStateList = new PicklistServicecs();
            model.PropertyStateList = pickListServices.GetPickListItems("H_State");
            ViewBag.ContactId = ContactId;
           
            return View(model);
        }

        public async Task<ActionResult> UpdateContactDetail(string ContactId , string ContactName, string Title , string Webaddress , string Address1 ,
           string Address2 , string Suburb , string State , string PostalCode , string Workphone, string Mobile , string Fax, string Homephone , string Email)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            ContactUpdateModel model = new ContactUpdateModel();
            model.ContactId = ContactId;
            model.ContactName = ContactName;
            model.Title = Title;
            model.Webaddress = Webaddress;
            model.Address1 = Address1;
            model.Address2 = Address2;
            model.Suburb = Suburb;
            model.State = State;
            model.PostalCode = PostalCode;
            model.Workphone = Workphone;
            model.Mobile = Mobile;
            model.Fax = Fax;
            model.Homephone = Homephone;
            model.Email = Email;
            ContactRepo contactRepo = new ContactRepo();
            bool result = await contactRepo.UpdateContact(model,client.UserId);
            return RedirectToAction("Index");
        }

    }
}