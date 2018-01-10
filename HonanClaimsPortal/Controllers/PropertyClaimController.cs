using AutoMapper;
using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Models.Common;
using HonanClaimsWebApi.Models.LookupModel;
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
    public class PropertyClaimController : Controller
    {
        ClaimTeamLoginModel client;
        ClaimServices claimServices;
        PicklistServicecs pickListServices;
        LookupServices lookupServices;

        // GET: PropertyClaim
        public ActionResult NewPropertyClaim()
        {
            if (TempData[TempDataHelper.NewClaimModel] == null)
                return RedirectToAction("Index", "NewClaim");

            NewClaimModel newClaimModel = TempData[TempDataHelper.NewClaimModel] as NewClaimModel;

            //login = Session[SessionHelper.LoginClient] as LoginClient;
            //Session[SessionHelper.StoreobjectList] = null;

            PropertyClaim claim = new PropertyClaim();
            pickListServices = new PicklistServicecs();
            claimServices = new ClaimServices();



            claim.Claim_Team = newClaimModel.Claim_Team;
            claim.Claim_Team_Name = newClaimModel.Claim_Team;
            claim.Account_Name = newClaimModel.Account_Name;
            claim.Accountid = newClaimModel.Accountid;
            claim.Claim_Type = newClaimModel.Claim_Type;
            claim.Oc_Id = newClaimModel.Oc_No;
            claim.Oc_Num = newClaimModel.Oc_Name;
            claim.Policy_No = newClaimModel.Policy_No;
            claim.Assigned_User = newClaimModel.Assigned_To_Id;
            claim.Property_Address_1 = newClaimModel.Property_Address_1;
            claim.Property_Address_2 = newClaimModel.Property_Address_2;
            claim.Property_Postalcode = newClaimModel.Property_Postalcode;
            claim.Property_State = newClaimModel.Property_State;
            claim.Property_Suburb = newClaimModel.Property_Suburb;
            claim.Account_Manager_Property = newClaimModel.AccountManager;
            claim.Insurer = newClaimModel.Insurer;
            claim.InsurerName = newClaimModel.InsurerName;
            claim.Insured_Name = newClaimModel.Insured_Name;
            claim.Account_Manager_Property = newClaimModel.Account_Manager_Property;
            claim.Policy_Class = newClaimModel.Policy_Class;
            claim.Policy_Id = newClaimModel.Policy_Id;
            claim.Incident_Date = newClaimModel.Incident_Date;

            //claim.Claim_Team = login.ClaimTeam;
            //claim.Claim_Type = string.IsNullOrEmpty(Request.QueryString[QueryStringHelper.PageType]) ? Session[SessionHelper.Page].ToString() : Request.QueryString[QueryStringHelper.PageType];

            //Get Claim Reference #
            claim.Claim_Reference_Num = claimServices.GenerateClaimRefNo(claim.Claim_Team);
            claim.Claim_Reference_Num = claim.Claim_Reference_Num.Replace("\"", "");


            InitializeModel(claim, claimServices);

            return View(claim);
        }

        [HttpPost]
        public async Task<ActionResult> NewPropertyClaim(PropertyClaim claim, IEnumerable<string> Region, IEnumerable<string> Incident_Category, IEnumerable<HttpPostedFileBase> upfiles)
        {
            try
            {
                pickListServices = new PicklistServicecs();
                claimServices = new ClaimServices();
                client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;

                if (claim.Region != null)
                    claim.Region = String.Join(",", Region.Where(s => !string.IsNullOrEmpty(s)));
                if (claim.Incident_Category != null)
                    claim.Incident_Category = String.Join(", ", Incident_Category.Where(s => !string.IsNullOrEmpty(s)));

                Mapper.Initialize(cfg => cfg.CreateMap<PropertyClaim, ClaimGeneral>());
                ClaimGeneral generalClaim = Mapper.Map<ClaimGeneral>(claim);

                if (ModelState.IsValid)
                {
                    claimServices = new ClaimServices();
                    generalClaim.Claim_Team_Name = claim.Claim_Team;
                    generalClaim.Accountid = claim.Accountid;
                    generalClaim.Account_Name = claim.Account_Name;
                    var result = await claimServices.TeamInsertClaimNotification(generalClaim, client.UserId,upfiles);


                    if (result.IsSuccess)
                    {
                        TempData["SuccessMsg"] = Messages.successMessage;

                        return RedirectToAction("index", "claimlist");
                        //if (claim.Claim_Type == ClaimType.Claim.ToString())
                        //    return RedirectToAction("ViewClaims", "ViewPages");
                        //else
                        //    return RedirectToAction("ViewNotifications", "ViewPages");
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            InitializeModel(claim, claimServices);

            return View(claim);
        }

        private void InitializeModel(PropertyClaim model, ClaimServices claimServices)
        {
            // Add Gender list
            model.GenderList = new List<string>()
            {
                "","Male","Female"
            };

            pickListServices = new PicklistServicecs();

            model.Policy_Section_List = pickListServices.GetPickListItems("Property Claims Policy Section");
            model.Policy_Section_List.Insert(0, new PicklistItem());

            //Get Suburbs
            model.PropertySuburbList = pickListServices.GetPickListItems("H_Suburbs");
            model.PropertySuburbList.Insert(0, new PicklistItem());

            //Get States
            model.PropertyStateList = pickListServices.GetPickListItems("H_State");
            model.PropertyStateList.Insert(0, new PicklistItem());

            model.Policy_Class_List = pickListServices.GetPickListItems("Honan Policy Classes");
            model.Policy_Class_List.Insert(0, new PicklistItem());

            model.Causation_List = pickListServices.GetPickListItems("Property Claims Causation");
            model.Causation_List.Insert(0, new PicklistItem());

            model.YesNoList = new List<string>() { "", "Yes", "No" };

            model.ComplexityList = new List<string>() { "","High", "Med", "Low" };

            model.Liability_Reserve = model.Liability_Res_Source;
            model.Defence_Reserve = model.Defence_Res_Source;

            if (ClaimHelper.IsManager(HonanClaimsPortal.Helpers.ClaimTeamManagers.PropertyClaimsManager))
                model.Assigned_User_List = claimServices.GetUsers(new List<string>() { "Property Claims Manager" });

            model.Outcome_List = pickListServices.GetPickListItems("Property Claims Outcome");
            model.Outcome_List.Insert(0, new PicklistItem());

            model.Claim_Status_List = pickListServices.GetPickListItems("Honan Claim Status");

            model.IncidentCategoryList = pickListServices.GetPickListItems("Property Claims Incident Category");
            model.IncidentCategoryList.Insert(0, new PicklistItem());

            model.Policy_Section_List = pickListServices.GetPickListItems("Property Claims Policy Section");
            model.Policy_Section_List.Insert(0, new PicklistItem());

            model.Notification_Status_List = pickListServices.GetPickListItems("Honan Notification Status");
            model.Notification_Status_List.Insert(0, new PicklistItem());


            model.Claim_Received = model.Claim_Received == null || model.Claim_Received ==  false ? false : true;
            model.Claim_Acknowledged = model.Claim_Acknowledged == null || model.Claim_Acknowledged == false ? false : true;
            model.Review = model.Review == null || model.Review == false ? false : true;
            model.Outcome_Settlement = model.Outcome_Settlement == null || model.Outcome_Settlement == false ? false : true;
            model.Outcome_Declined = model.Outcome_Declined == null || model.Outcome_Declined == false ? false : true;
            model.Claim_Closed = model.Claim_Closed == null || model.Claim_Closed == false ? false : true;
            model.Litigated = model.Litigated == null || model.Litigated == false ? false : true;
            model.Claim_Lodged = model.Claim_Lodged == null || model.Claim_Lodged == false ? false : true;
            model.Claim_Not_Lodged = model.Claim_Not_Lodged == null || model.Claim_Not_Lodged == false ? false : true;
            model.Claim_Approved = model.Claim_Approved == null || model.Claim_Approved == false ? false : true;
            model.Claim_Declined = model.Claim_Declined == null || model.Claim_Declined == false ? false : true;

            //Calculations
            PaymentServices paymentServices = new PaymentServices();
            decimal val, liabilityReserveGross = 0, defenceReserveGross = 0;


            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(model.H_Claimsid, "Liability Reserve", false), out val))
                model.Net_Paid_Liability = val;

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(model.H_Claimsid, "Defence Reserve", false), out val))
                model.Net_Paid_Defence = val;


            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(model.H_Claimsid, "Liability Reserve", true), out val))
            {
                liabilityReserveGross = val;
                model.Liability_Reserve = model.Liability_Reserve - model.Net_Paid_Liability;
            }

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(model.H_Claimsid, "Defence Reserve", true), out val))
            {
                defenceReserveGross = val;
                model.Defence_Reserve = model.Defence_Reserve - model.Net_Paid_Defence;
            }

            model.Total_Reserve = model.Liability_Reserve + model.Defence_Reserve;


            model.Gross_Paid_To_Date = liabilityReserveGross + defenceReserveGross;

            model.Total_Incurred = model.Total_Reserve + model.Net_Paid_Liability + model.Net_Paid_Defence;

            if (model.Total_Reserve < model.Excess)
                model.Current_Exposure = model.Total_Reserve;
            else
                model.Current_Exposure = model.Excess - model.Net_Paid_Liability - model.Net_Paid_Defence;


            decimal totalNet = model.Net_Paid_Liability + model.Net_Paid_Defence;
            if (totalNet > model.Excess)
                model.Over_Excess_Paid = totalNet - model.Excess;
            else
                model.Over_Excess_Paid = 0;


            if (model.Reported_Time != null)
            {
                string time = DateTime.Parse(model.Reported_Time.ToString()).ToString("HH:mm");
                model.Reported_TimeH = time.Split(':')[0].PadLeft(2, '0');
                model.Reported_TimeM = time.Split(':')[1].PadLeft(2, '0');
            }

            if (model.Incident_Time != null)
            {
                string time = DateTime.Parse(model.Incident_Time.ToString()).ToString("HH:mm");
                model.Incident_TimeH = time.Split(':')[0].PadLeft(2, '0');
                model.Incident_TimeM = time.Split(':')[1].PadLeft(2, '0');
            }

            model.IncidentTypeList = pickListServices.GetPickListItems("Property Claims Incident Type");
        }

        public ActionResult DetailPropertyClaim(string id)
        {
            client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;

            claimServices = new ClaimServices();
            //Mapper mapper = new 
            Mapper.Initialize(cfg => cfg.CreateMap<ClaimGeneral,PropertyClaim>());
            PropertyClaim model = Mapper.Map<PropertyClaim>(claimServices.GetClaimNotification(id));

            InitializeModel(model, claimServices);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> DetailPropertyClaim(PropertyClaim model, IEnumerable<string> Incident_Category)
        {
            Session[SessionHelper.StoreobjectList] = null;
            PicklistServicecs picklistService = new PicklistServicecs();
            ClaimServices claims = new ClaimServices();

            if (Incident_Category != null)
                model.Incident_Category = String.Join(", ", Incident_Category.Where(s => !string.IsNullOrEmpty(s)));

            Mapper.Initialize(cfg => cfg.CreateMap<PropertyClaim, ClaimGeneral>());
            ClaimGeneral generalClaim = Mapper.Map<ClaimGeneral>(model);

            generalClaim.Policy_Class = string.IsNullOrEmpty(model.Policy_Class) == true ? model.Policy_Class_Selection : model.Policy_Class;

            ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            if (ModelState.IsValid)
            {
                claims = new ClaimServices();
                var result = await claims.TeamUpdateClaimNotification(generalClaim, login.UserId);
                if (result)
                {
                    TempData["ClaimSaved"] = Messages.ClaimSaved;
                    return Redirect(Request.UrlReferrer.ToString());
                }
                else
                    TempData["ErrorMsg"] = Messages.errorMessage;
            }

            InitializeModel(model, claims);

            return View(model);
        }

    }
}