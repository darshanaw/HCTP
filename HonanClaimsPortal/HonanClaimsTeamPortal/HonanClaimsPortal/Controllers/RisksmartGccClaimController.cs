using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Models.Common;
using HonanClaimsWebApi.Services;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HonanClaimsWebApi.Models.MyActivity;
using System.Threading.Tasks;

namespace HonanClaimsPortal.Controllers
{
    [AuthorizeUser]
    public class RisksmartGccClaimController : Controller
    {
        ClaimTeamLoginModel client;
        ClaimServices claimServices;
        PicklistServicecs pickListServices;

        public ActionResult NewRisksmartGccClaim()
        {
            client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;

            if (TempData[TempDataHelper.NewClaimModel] == null)
                return RedirectToAction("Index", "NewClaim");

            NewClaimModel newClaimModel = TempData[TempDataHelper.NewClaimModel] as NewClaimModel;

            claimServices = new ClaimServices();

            RisksmartGccClaim model = new RisksmartGccClaim();
            model.Claim_Received = model.Claim_Received == null || model.Claim_Received == false ? false : true;
            model.Claim_Acknowledged = model.Claim_Acknowledged == null || model.Claim_Acknowledged == false ? false : true;
            model.Review = model.Review == null || model.Review == false ? false : true;
            model.Outcome_Settlement = model.Outcome_Settlement == null || model.Outcome_Settlement == false ? false : true;
            model.Outcome_Declined = model.Outcome_Declined == null || model.Outcome_Declined == false ? false : true;
            model.Claim_Closed = model.Claim_Closed == null || model.Claim_Closed == false ? false : true;
            model.Litigated = model.Litigated == null || model.Litigated == false ? false : true;

            model.Claim_Team = newClaimModel.Claim_Team;
            model.Claim_Team_Name = newClaimModel.Claim_Team;
            model.Account_Name = newClaimModel.Account_Name;
            model.Accountid = newClaimModel.Accountid;
            model.Claim_Type = newClaimModel.Claim_Type;
            model.Oc_Num = newClaimModel.Oc_No;
            model.Policy_No = newClaimModel.Policy_No;
            model.Assigned_User = newClaimModel.Assigned_To_Id;
            model.Property_Address_1 = newClaimModel.Property_Address_1;
            model.Property_Address_1 = newClaimModel.Property_Address_2;
            model.Property_Postalcode = newClaimModel.Property_Postalcode;
            model.Property_State = newClaimModel.Property_State;
            model.Property_Suburb = newClaimModel.Property_Suburb;
            model.Insurer = newClaimModel.Insurer;
            model.InsurerName = newClaimModel.InsurerName;
            model.Insured_Name = newClaimModel.Insured_Name;

            // Get Claim Reference #
            model.Claim_Reference_Num = claimServices.GenerateClaimRefNo(model.Claim_Team);
            model.Claim_Reference_Num = model.Claim_Reference_Num.Replace("\"", "");

            InitializeModel(model, claimServices);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> NewRisksmartGccClaim(RisksmartGccClaim claim, IEnumerable<string> Region, IEnumerable<string> Incident_Category)
        {
            try
            {
                pickListServices = new PicklistServicecs();
                claimServices = new ClaimServices();
                client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;

                if (claim.Region != null)
                    claim.Region = String.Join(", ", Region.Where(s => !string.IsNullOrEmpty(s)));
                if (claim.Incident_Category != null)
                    claim.Incident_Category = String.Join(", ", Incident_Category.Where(s => !string.IsNullOrEmpty(s)));

                Mapper.Initialize(cfg => cfg.CreateMap<RisksmartGccClaim, ClaimGeneral>());
                ClaimGeneral generalClaim = Mapper.Map<ClaimGeneral>(claim);

                if (claim.Claim_Type ==  ClaimType.Notification.ToString())
                {
                    if (ModelState.ContainsKey("Policy_No"))
                        ModelState["Policy_No"].Errors.Clear();
                }

                if (ModelState.IsValid)
                {                    
                    generalClaim.Claim_Team_Name = claim.Claim_Team;
                    generalClaim.Accountid = claim.Accountid;
                    generalClaim.Account_Name = claim.Account_Name;
                    var result = await claimServices.TeamInsertClaimNotification(generalClaim, client.UserId);

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

            InitializeModel(claim,claimServices);

            return View(claim);
        }
        // GET: RisksmartGccClaim
        public ActionResult DetailRisksmartGccClaim(string id)
        {
            client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;

            claimServices = new ClaimServices();
            //Mapper mapper = new 
            Mapper.Initialize(cfg => cfg.CreateMap<ClaimGeneral, RisksmartGccClaim>());
            RisksmartGccClaim model = Mapper.Map<RisksmartGccClaim>(claimServices.GetClaimNotification(id));

            model.Claim_Received = model.Claim_Received == null || model.Claim_Received == false ? false : true;
            model.Claim_Acknowledged = model.Claim_Acknowledged == null || model.Claim_Acknowledged == false ? false : true;
            model.Review = model.Review == null || model.Review == false ? false : true;
            model.Outcome_Settlement = model.Outcome_Settlement == null || model.Outcome_Settlement == false ? false : true;
            model.Outcome_Declined = model.Outcome_Declined == null || model.Outcome_Declined == false ? false : true;
            model.Claim_Closed = model.Claim_Closed == null || model.Claim_Closed == false ? false : true;
            model.Litigated = model.Litigated == null || model.Litigated == false ? false : true;

            InitializeModel(model, claimServices);

            return View(model);
        }

        private void InitializeModel(RisksmartGccClaim model, ClaimServices claimServices)
        {

            pickListServices = new PicklistServicecs();

            if (ClaimHelper.IsManager(HonanClaimsPortal.Helpers.ClaimTeamManagers.RisksmartGCCManager))
                model.Assigned_User_List = claimServices.GetUsers(new List<string>() { "Risksmart GCC Manager" });

            model.Claim_Status_List = pickListServices.GetPickListItems("Honan Claim Status");

            //Get Reported By Types
            model.ReportedByTypeList = pickListServices.GetPickListItems("Risksmart GCC Reported By Type");

            //Get Regions
            model.RegionList = pickListServices.GetPickListItems("H_StoreRegion");

            // Add Juristiction list
            model.JuristictionList = new List<string>()
            {
                "","NSW","VIC","QLD","WA","SA","TAS"
            };

            // Add Gender list
            model.GenderList = new List<string>()
            {
                "","Male","Female"
            };

            //Get Incident categoris
            model.IncidentCategoryList = pickListServices.GetPickListItems("Risksmart GCC Incident Category");
            model.IncidentCategoryList.Insert(0, new PicklistItem());

            //Get Incident Party Types
            model.IncidentPartyTypeList = pickListServices.GetPickListItems("Risksmart GCC Incident Party Type");
            model.IncidentPartyTypeList.Insert(0, new PicklistItem());

            model.Policy_Section_List = pickListServices.GetPickListItems("Risksmart GCC Policy Section");
            model.Policy_Section_List.Insert(0, new PicklistItem());

            //Get Outcome List
            model.Outcome_List = pickListServices.GetPickListItems("Risksmart GCC Outcome");
            model.Outcome_List.Insert(0, new PicklistItem());

            model.Policy_Class_List = pickListServices.GetPickListItems("Risksmart GCC Policy Class");
            model.Policy_Class_List.Insert(0, new PicklistItem());

            model.Client_Group_List = pickListServices.GetPickListItems("Risksmart GCC Client Group");
            model.Client_Group_List.Insert(0, new PicklistItem());

            model.Notification_Status_List = pickListServices.GetPickListItems("Honan Notification Status");
            model.Notification_Status_List.Insert(0, new PicklistItem());

            model.Bodily_Location_List = pickListServices.GetPickListItems("Honan Claims Bodily Location");
            model.Bodily_Location_List.Insert(0, new PicklistItem());

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

            // Add CCTV available list
            model.CCTVAvailableList = new List<string>() { "", "Yes", "No" };

            // Add CCTV viewed list
            model.CCTVViewedList = new List<string>() { "", "Yes", "No" };

            model.PanelLawyersRetainedList = new List<string>() { "", "Yes", "No" };

            

            model.Total_Incurred = model.Total_Reserve + model.Net_Paid_Liability + model.Net_Paid_Defence;

            model.Liability_Reserve = model.Liability_Res_Source;
            model.Defence_Reserve = model.Defence_Res_Source;

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


            model.Claim_Received = model.Claim_Received == null || model.Claim_Received == false ? false : true;
            model.Claim_Acknowledged = model.Claim_Acknowledged == null || model.Claim_Acknowledged == false ? false : true;
            model.Review = model.Review == null || model.Review == false ? false : true;
            model.Outcome_Settlement = model.Outcome_Settlement == null || model.Outcome_Settlement == false ? false : true;
            model.Outcome_Declined = model.Outcome_Declined == null || model.Outcome_Declined == false ? false : true;
            model.Litigated = model.Litigated == null || model.Litigated == false ? false : true;
            model.Claim_Closed = model.Claim_Closed == null || model.Claim_Closed == false ? false : true;

            model.IncidentTypeList = pickListServices.GetPickListItems("Risksmart GCC Incident Type");
            model.YesNoList = new List<string>() { "", "Yes", "No" };

            model.Bodily_Location_List = pickListServices.GetPickListItems("Honan Claims Bodily Location");
            model.Bodily_Location_List.Insert(0, new PicklistItem());
        }

        [HttpPost]
        public async Task<ActionResult> DetailRisksmartGccClaim(RisksmartGccClaim model, IEnumerable<string> Incident_Category)
        {
            Session[SessionHelper.StoreobjectList] = null;
            PicklistServicecs picklistService = new PicklistServicecs();
            ClaimServices claims = new ClaimServices();

            if (model.Incident_Category != null)
                model.Incident_Category = String.Join(", ", Incident_Category.Where(s => !string.IsNullOrEmpty(s)));

            Mapper.Initialize(cfg => cfg.CreateMap<RisksmartGccClaim, ClaimGeneral>());
            ClaimGeneral generalClaim = Mapper.Map<ClaimGeneral>(model);

            generalClaim.Policy_Class = string.IsNullOrEmpty(model.Policy_Class) == true ? model.Policy_Class_Selection : model.Policy_Class;

        

            ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            if (ModelState.IsValid)
            {
                claims = new ClaimServices();
                var result = await claims.TeamUpdateClaimNotification(generalClaim, login.UserId);
                if (result)
                {
                    //return RedirectToAction("Index", "ClaimList");
                    TempData["ClaimSaved"] = Messages.ClaimSaved;
                    return Redirect(Request.UrlReferrer.ToString());
                }
                else
                    TempData["ErrorMsg"] = Messages.errorMessage;
            }

            InitializeModel(model, claims);

            return View(model);
        }


        public ActionResult HistoryAjaxHandler(string claimId, string fieldName, string newValue)
        {
            ClaimServices claimService = new ClaimServices();
            client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            return Json(claimService.CreateHistoryRecord(client.UserId, client.FirstName + " " + client.LastName, claimId, fieldName, newValue));
        }
       

    }
}