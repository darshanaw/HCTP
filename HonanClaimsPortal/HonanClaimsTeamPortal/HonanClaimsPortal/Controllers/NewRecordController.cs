using AutoMapper;
using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Billing;
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
        //    Uri uri = new Uri(Request.Url.AbsoluteUri, UriKind.Relative);
        //    string url = uri.ToString();
            Session[SessionHelper.FromURL] = Request.UrlReferrer == null ? Request.Url.AbsoluteUri.ToString() : Request.UrlReferrer.ToString();
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

            if (model.Is_Settlement)
            {
                if (ModelState.ContainsKey("Payee_Type"))
                    ModelState["Payee_Type"].Errors.Clear();

                if (ModelState.ContainsKey("Payee_Contact_Name"))
                    ModelState["Payee_Contact_Name"].Errors.Clear();

                if (ModelState.ContainsKey("Date_Invoice_Received"))
                    ModelState["Date_Invoice_Received"].Errors.Clear();

                if (ModelState.ContainsKey("Payee_Account_Name"))
                    ModelState["Payee_Account_Name"].Errors.Clear();
            }
            else
            {
                if (ModelState.ContainsKey("Payment_Amount"))
                    ModelState["Payment_Amount"].Errors.Clear();

                if (ModelState.ContainsKey("Payment_Date"))
                    ModelState["Payment_Date"].Errors.Clear();
            }


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
                if (t.IsSuccess)
                {
                    Session[SessionHelper.PaymentAttachment] = null;
                    //return Redirect(Session[SessionHelper.FromURL].ToString());
                    string claimNoType = model.ClaimRefNo_Payment;
                    string redirectUrl = GetRedirectUrl(claimNoType, model.H_Claimsid,"payment");
                    return Redirect(redirectUrl);
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

        public ActionResult NewKeyContact()
        {
            Session[SessionHelper.FromURL] = Request.UrlReferrer == null ? Request.Url.AbsoluteUri.ToString() : Request.UrlReferrer.ToString();
            KeyContact model = new KeyContact();
            PicklistServicecs pickListServices = new PicklistServicecs();
            model.DescriptionList = pickListServices.GetPickListItems("Key Contact Description");

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> NewKeyContact(KeyContact model)
        {
            if (ModelState.IsValid)
            {
                ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
                KeyContactDateServices service = new KeyContactDateServices();

                bool result;

                if (string.IsNullOrEmpty(model.H_Keycontactsid))
                    result = await service.InsertKeyContact(model, login.UserId);
                else
                    result = await service.UpdateKeyContact(model, login.UserId);

                if (result)
                {
                    string claimNoType = model.Claim_Ref_No;
                    string redirectUrl = GetRedirectUrl(claimNoType, model.H_Claimsid, "keycontact");
                    return Redirect(redirectUrl);
                }

            }
            PicklistServicecs pickListServices = new PicklistServicecs();
            model.DescriptionList = pickListServices.GetPickListItems("Key Contact Description");

            return View(model);
        }


        public ActionResult NewFileNote()
        {
            Session[SessionHelper.FromURL] = Request.UrlReferrer == null ? Request.Url.AbsoluteUri.ToString() : Request.UrlReferrer.ToString();
            FileNoteDetailModal model = new FileNoteDetailModal();
            ClaimServices claimServices = new ClaimServices();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            model.CreatedDate_Fn = DateTime.Now;
            model.CreatedBy_Id_Fn = client.UserId;
            model.CreatedBy_Fn = client.FirstName + " " + client.LastName;
            model.RefnuberList_Fn = claimServices.GetClaimsForUser(client.UserId);

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult NewFileNote(FileNoteDetailModal model)
        {
            DocumentService documentService = new DocumentService();
            documentService.CreateFileNoteRecord(model.CreatedBy_Id_Fn, model.ShortDescription_Fn, model.Detail_Fn, model.ClaimId_Fn, model.FileNoteDate_Fn.Value);
            //return Redirect(Session[SessionHelper.FromURL].ToString());
            string claimNoType = model.ClaimRefNo_Fn;
            string redirectUrl = GetRedirectUrl(claimNoType, model.ClaimId_Fn, "filenote");
            return Redirect(redirectUrl);
        }

        public ActionResult NewKeyDate()
        {
            Session[SessionHelper.FromURL] = Request.UrlReferrer == null ? Request.Url.AbsoluteUri.ToString() : Request.UrlReferrer.ToString();
            KeyDate model = new KeyDate();
            PicklistServicecs pickListServices = new PicklistServicecs();
            model.Key_Date_Description_List = pickListServices.GetPickListItems("Key Date Description");

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> NewKeyDate(KeyDate model)
        {

            if (ModelState.IsValid)
            {
                ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
                KeyContactDateServices service = new KeyContactDateServices();

                bool result;

                if (string.IsNullOrEmpty(model.H_Keydatesid))
                    result = await service.InsertKeyDate(model, login.UserId);
                else
                    result = await service.UpdateKeyDate(model, login.UserId);

                if (result)
                    return Redirect(Session[SessionHelper.FromURL].ToString());

            }
            PicklistServicecs pickListServices = new PicklistServicecs();
            model.Key_Date_Description_List = pickListServices.GetPickListItems("Key Date Description");

            return View(model);
        }


        public ActionResult NewTimeslipDetail(string BillingId, string page)
        {
            var modelOld = new BillingModel();
            var model = new BillingModelNew();

            if (BillingId == null)
            {
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                string UserId = client.UserId;

                model.IsNew_Billable_New = true;
                model.Is_Billable_New = true;
                model.Service_By_New = UserId;
                model.Service_By_Name_New = client.FirstName + " " + client.LastName;
                model.Service_Date_New = DateTime.Today;

                if (!string.IsNullOrEmpty(page))
                    model.PageType_New = page;

                return PartialView(model);
            }
            model.IsNew_Billable_New = false;
            BillingRepo billingRepo = new BillingRepo();
            modelOld = billingRepo.GetTeamGetBillableTimeRecord(BillingId).Result;

            Mapper.Initialize(cfg => cfg.CreateMap<BillingModel, BillingModelNew>());
            model = Mapper.Map<BillingModelNew>(modelOld);

            model.Billable_New = (decimal.Round(model.Billable_New, 2));
            model.Rate_New = (decimal.Round(model.Rate_New, 2));
            model.Rate_Per_Unit_New = (decimal.Round(model.Rate_Per_Unit_New, 2));
            return View(model);
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult NewTimeslipDetail(BillingModelNew model)
        {
            try
            {
                //if(model.H_Billingsid==null)
                //{
                //    return PartialView(model);
                //}

                if (model.sStart_Time_New != null)
                {
                    model.Start_Time_New = Convert.ToDateTime(model.sStart_Time_New);
                }

                if (model.sEnd_Time_New != null)
                {
                    model.End_Time_New = Convert.ToDateTime(model.sEnd_Time_New);
                }

                BillingRepo billingRepo = new BillingRepo();
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];

                BillingModel modelOld = MapBillinOld_New(model);

                var result = billingRepo.TeamInsertTimeslip(modelOld, client.UserId).Result;

                string claimNoType = model.Claim_No_New;
                string redirectUrl = GetRedirectUrl(claimNoType,model.H_Claimsid_Billing_New,"billing");                

                return Redirect(redirectUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetRedirectUrl(string claimNoType, string H_Claimsid, string tab)
        {
            string redirectUrl = "";
            if (claimNoType.ToUpper().Contains("RSG"))
            {
                redirectUrl = "/RisksmartGccClaim/DetailRisksmartGccClaim/" + H_Claimsid + "?tab="+ tab;
            }
            else if (claimNoType.ToUpper().Contains("RSP"))
            {
                redirectUrl = "/RisksmartPropertyClaim/DetailRisksmartPropertyClaim/" + H_Claimsid + "?tab=" + tab;
            }
            else if (claimNoType.ToUpper().Contains("PRC"))
            {
                redirectUrl = "/PropertyClaim/DetailPropertyClaim/" + H_Claimsid + "?tab=" + tab;
            }
            else if (claimNoType.ToUpper().Contains("GGC"))
            {
                redirectUrl = "/GccClaim/DetailGccClaim/" + H_Claimsid + "?tab=" + tab;
            }
            else
            {
                redirectUrl = "/claimlist/index";
            }

            return redirectUrl;
        }

        private BillingModel MapBillinOld_New(BillingModelNew model)
        {
            BillingModel modelNew = new BillingModel();
            modelNew.H_Billingsid = model.H_Billingsid_New;
            modelNew.H_Claimsid_Billing = model.H_Claimsid_Billing_New;
            modelNew.Service_By = model.Service_By_New;
            modelNew.Is_Billable = model.Is_Billable_New;
            modelNew.Claim_No = model.Claim_No_New;
            modelNew.ActivityId = model.ActivityId_New;
            modelNew.Activity_Name = model.Activity_Name_New;
            modelNew.Policyid_Billing = model.Policyid_Billing_New;
            modelNew.Policy_Name_Billing = model.Policy_Name_Billing_New;
            modelNew.ClientId = model.ClientId_New;
            modelNew.Client_Name = model.Client_Name_New;
            modelNew.Service_Date = model.Service_Date_New;
            modelNew.Start_Time_HH = model.Start_Time_HH_New;
            modelNew.Start_Time_MM = model.Start_Time_MM_New;
            modelNew.End_Time_HH = model.End_Time_HH_New;
            modelNew.End_Time_MM = model.End_Time_MM_New;
            modelNew.Qty_Mins = model.Qty_Mins_New;
            modelNew.Rate = model.Rate_New;
            modelNew.Billable = model.Billable_New;
            modelNew.Timeslip_Checked = model.Timeslip_Checked_New;
            modelNew.Checked_By = model.Checked_By_New;
            modelNew.Checked_Date = model.Checked_Date_New;
            modelNew.Invoice_Processed = model.Invoice_Processed_New;
            modelNew.Invoice_Processed_By = model.Invoice_Processed_By_New;
            modelNew.Invoice_Processed_Date = model.Invoice_Processed_Date_New;
            modelNew.Invoice_No = model.Invoice_No_New;
            modelNew.Invoice_Date = model.Invoice_Date_New;
            modelNew.Rate_Per_Unit = model.Rate_Per_Unit_New;
            modelNew.Units = model.Units_New;
            modelNew.Work_Done = model.Work_Done_New;
            modelNew.Service_By_Name = model.Service_By_Name_New;
            modelNew.Quarter = model.Quarter_New;
            modelNew.Work_Done_Short = model.Work_Done_Short_New;
            modelNew.IsNew_Billable = model.IsNew_Billable_New;
            modelNew.PageType = model.PageType_New;
            modelNew.Start_Time = model.Start_Time_New;
            modelNew.End_Time = model.End_Time_New;
            modelNew.sStart_Time = model.sStart_Time_New;
            modelNew.sEnd_Time = model.sEnd_Time_New;
            modelNew.Checked_By_Name = model.Checked_By_Name_New;
            modelNew.Invoice_Processed_By_Name = model.Invoice_Processed_By_Name_New;

            return modelNew;

        }
    }
}