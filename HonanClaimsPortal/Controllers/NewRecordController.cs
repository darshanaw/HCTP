using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Services;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    [AuthorizeUser]
    public class NewRecordController : Controller
    {
        public ActionResult NewPayment()
        {
            TempData["FromURL"] = Request.UrlReferrer;
            Payment model = new Payment();
            ClaimServices claimServices = new ClaimServices();
            PicklistServicecs pickListServices = new PicklistServicecs();

            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];

            //model.ClaimRefNo_Payment_List = claimServices.GetClaimsForUser(client.UserId);
            model.Payee_Type_List = pickListServices.GetPickListItems("Honan Payee type");
            model.Gst = "10";
            model.Gst_Included = true;
            model.Payment_Status_List = pickListServices.GetPickListItems("Honan Payment Status");
            model.Payment_Type_List = pickListServices.GetPickListItems("Honan Payment Type");
            model.Payment_Method_List = pickListServices.GetPickListItems("Honan Payment Method");
            model.IsNew = true;

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> NewPayment(Payment model, IEnumerable<System.Web.HttpPostedFileBase> files)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];

            if (ModelState.IsValid)
            {
                DocumentService documentService = new DocumentService();

                if (Session[SessionHelper.PaymentAttachment] != null)
                    files = (IEnumerable<HttpPostedFileBase>)Session[SessionHelper.PaymentAttachment];

                if (model.Is_Settlement && model.Payment_Amount.HasValue)
                {
                    model.Total_Gross = (decimal)model.Payment_Amount;
                    model.Total_Net = (decimal)model.Payment_Amount;
                }

                var t = await documentService.CreatePaymentDetailRecord(model, client.UserId, files);
                if(t.IsSuccess)
                {
                    Session[SessionHelper.PaymentAttachment] = null;
                    return Redirect(TempData["FromURL"].ToString());
                }
            }

            ClaimServices claimServices = new ClaimServices();
            PicklistServicecs pickListServices = new PicklistServicecs();
            model.Payee_Type_List = pickListServices.GetPickListItems("Honan Payee type");
            model.Gst = "10";
            model.Gst_Included = true;
            model.Payment_Status_List = pickListServices.GetPickListItems("Honan Payment Status");
            model.Payment_Type_List = pickListServices.GetPickListItems("Honan Payment Type");
            model.Payment_Method_List = pickListServices.GetPickListItems("Honan Payment Method");
            //model.ClaimRefNo_Payment_List = claimServices.GetClaimsForUser(client.UserId);
            return View(model);
            //return Json(new { success = true });
        }
    }
}