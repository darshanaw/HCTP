using AutoMapper;
using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Models.Common;
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
    public class GccClaimController : Controller
    {
        ClaimTeamLoginModel client;
        ClaimServices claimServices;
        PicklistServicecs pickListServices;
        LookupServices lookupServices;

        // GET: GccClaim
        public ActionResult NewGccClaim()
        {
            if (TempData[TempDataHelper.NewClaimModel] == null)
                return RedirectToAction("Index", "NewClaim");

            NewClaimModel newClaimModel = TempData[TempDataHelper.NewClaimModel] as NewClaimModel;

            //if (Request.QueryString[QueryStringHelper.PageType] == null)
            //    return RedirectToAction("Index", "Home");

            //login = Session[SessionHelper.LoginClient] as LoginClient;
            //Session[SessionHelper.StoreobjectList] = null;

            GccClaim claim = new GccClaim();

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
            claim.Property_Address_1 = newClaimModel.Property_Address_2;
            claim.Property_Postalcode = newClaimModel.Property_Postalcode;
            claim.Property_State = newClaimModel.Property_State;
            claim.Property_Suburb = newClaimModel.Property_Suburb;

            // Get Claim Reference #
            claim.Claim_Reference_Num = claimServices.GenerateClaimRefNo(claim.Claim_Team);
            claim.Claim_Reference_Num = claim.Claim_Reference_Num.Replace("\"", "");

            InitializeModel(claim, claimServices);

            return View(claim);
        }

        [HttpPost]
        public ActionResult NewGccClaim(GccClaim claim, IEnumerable<string> Region, IEnumerable<string> Incident_Category)
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

                Mapper.Initialize(cfg => cfg.CreateMap<GccClaim, ClaimGeneral>());
                ClaimGeneral generalClaim = Mapper.Map<ClaimGeneral>(claim);

                if (ModelState.IsValid)
                {
                    generalClaim.Claim_Team_Name = claim.Claim_Team;
                    generalClaim.Accountid = claim.Accountid;
                    generalClaim.Account_Name = claim.Account_Name;
                    var result = claimServices.TeamInsertClaimNotification(generalClaim, client.UserId);

                    if (result.IsSuccess)
                    {
                        TempData["SuccessMsg"] = Messages.successMessage;

                        return RedirectToAction("index", "claimlist");

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

        public ActionResult DetailGccClaim(string id)
        {
            client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;

            claimServices = new ClaimServices();
            //Mapper mapper = new 
            Mapper.Initialize(cfg => cfg.CreateMap<ClaimGeneral, GccClaim>());
            GccClaim model = Mapper.Map<GccClaim>(claimServices.GetClaimNotification(id));

            InitializeModel(model, claimServices);

            return View(model);
        }

        private void InitializeModel(GccClaim claim, ClaimServices claimServices)
        {
            pickListServices = new PicklistServicecs();

            //Get Incident categoris
            claim.IncidentCategoryList = pickListServices.GetPickListItems("GCC Incident Category");
            claim.IncidentCategoryList.Insert(0, new PicklistItem());

            claim.IncidentPartyTypeList = pickListServices.GetPickListItems("GCC Incident Type");
            claim.IncidentPartyTypeList.Insert(0, new PicklistItem());

            claim.Policy_Class_List = pickListServices.GetPickListItems("Honan Policy Classes");
            claim.Policy_Class_List.Insert(0, new PicklistItem());

            claim.YesNoList = new List<string>() { "", "Yes", "No" };

            if (ClaimHelper.IsManager(HonanClaimsPortal.Helpers.ClaimTeamManagers.RisksmartGCCManager))
                claim.Assigned_User_List = claimServices.GetUsers(new List<string>() { "GCC Claims Manager" });

            claim.Claim_Status_List = pickListServices.GetPickListItems("Honan Claim Status");

            //Get Reported By Types
            //claim.ReportedByTypeList = pickListServices.GetPickListItems("Risksmart GCC Reported By Type");

            //Get Regions
            claim.RegionList = pickListServices.GetPickListItems("H_StoreRegion");

            // Add Juristiction list
            claim.JuristictionList = new List<string>()
            {
                "","NSW","VIC","QLD","WA","SA","TAS"
            };

            // Add Gender list
            claim.GenderList = new List<string>()
            {
                "","Male","Female"
            };


            //Get Incident Party Types
            claim.IncidentPartyTypeList = pickListServices.GetPickListItems("GCC Incident Type");
            claim.IncidentPartyTypeList.Insert(0, new PicklistItem());

            //claim.Policy_Section_List = pickListServices.GetPickListItems("Risksmart GCC Policy Section");
            //claim.Policy_Section_List.Insert(0, new PicklistItem());

            //Get Outcome List
            claim.Outcome_List = pickListServices.GetPickListItems("Risksmart GCC Outcome");
            claim.Outcome_List.Insert(0, new PicklistItem());

            claim.Policy_Class_List = pickListServices.GetPickListItems("Honan Policy Classes");
            claim.Policy_Class_List.Insert(0, new PicklistItem());

            //claim.ClientGroupList = pickListServices.GetPickListItems("Risksmart GCC Client Group");
            //claim.ClientGroupList.Insert(0, new PicklistItem());

            if (claim.Reported_Time != null)
            {
                string time = DateTime.Parse(claim.Reported_Time.ToString()).ToString("HH:mm");
                claim.Reported_TimeH = time.Split(':')[0].PadLeft(2, '0');
                claim.Reported_TimeM = time.Split(':')[1].PadLeft(2, '0');
            }

            if (claim.Incident_Time != null)
            {
                string time = DateTime.Parse(claim.Incident_Time.ToString()).ToString("HH:mm");
                claim.Incident_TimeH = time.Split(':')[0].PadLeft(2, '0');
                claim.Incident_TimeM = time.Split(':')[1].PadLeft(2, '0');
            }

            // Add CCTV available list
            claim.CCTVAvailableList = new List<string>() { "", "Yes", "No" };

            // Add CCTV viewed list
            claim.CCTVViewedList = new List<string>() { "", "Yes", "No" };

            //claim.PanelLawyersRetainedList = new List<string>() { "", "Yes", "No" };
            

            claim.Total_Incurred = claim.Total_Reserve + claim.Net_Paid_Liability + claim.Net_Paid_Defence;

            claim.Liability_Reserve = claim.Liability_Res_Source;
            claim.Defence_Reserve = claim.Defence_Res_Source;

            PaymentServices paymentServices = new PaymentServices();
            decimal val, liabilityReserveGross = 0, defenceReserveGross = 0;


            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(claim.H_Claimsid, "Liability Reserve", true), out val))
            {
                liabilityReserveGross = val;
                claim.Liability_Reserve = claim.Liability_Reserve - val;
            }

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(claim.H_Claimsid, "Defence Reserve", true), out val))
            {
                defenceReserveGross = val;
                claim.Defence_Reserve = claim.Defence_Reserve - val;
            }

            claim.Total_Reserve = claim.Liability_Reserve + claim.Defence_Reserve;

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(claim.H_Claimsid, "Liability Reserve", false), out val))
                claim.Net_Paid_Liability = val;

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(claim.H_Claimsid, "Defence Reserve", false), out val))
                claim.Net_Paid_Defence = val;

            claim.Gross_Paid_To_Date = liabilityReserveGross + defenceReserveGross;

            claim.Total_Incurred = claim.Total_Reserve + claim.Net_Paid_Liability + claim.Net_Paid_Defence;

            if (claim.Total_Reserve < claim.Excess)
                claim.Current_Exposure = claim.Total_Reserve;
            else
                claim.Current_Reserve = claim.Excess - claim.Net_Paid_Liability - claim.Net_Paid_Defence;


            claim.Claim_Closed = claim.Claim_Closed == null || claim.Claim_Closed == false ? false : true;
            claim.Claim_Declined = claim.Claim_Declined == null || claim.Claim_Declined == false ? false : true;
            claim.Partial_Indemnity_Granted = claim.Partial_Indemnity_Granted == null || claim.Partial_Indemnity_Granted == false ? false : true;
            claim.Indemnity_Granted = claim.Indemnity_Granted == null || claim.Indemnity_Granted == false ? false : true;
            claim.Expert_Appointed = claim.Expert_Appointed == null || claim.Expert_Appointed == false ? false : true;
            claim.Claim_Acknowledged = claim.Claim_Acknowledged == null || claim.Claim_Acknowledged == false ? false : true;
            claim.Claim_Lodged = claim.Claim_Lodged == null || claim.Claim_Lodged == false ? false : true;
            claim.Claim_Not_Lodged = claim.Claim_Not_Lodged == null || claim.Claim_Not_Lodged == false ? false : true;

        }

        [HttpPost]
        public async Task<ActionResult> DetailGccClaim(GccClaim model, IEnumerable<string> Incident_Category)
        {
            Session[SessionHelper.StoreobjectList] = null;
            PicklistServicecs picklistService = new PicklistServicecs();
            ClaimServices claims = new ClaimServices();

            if (model.Incident_Category != null)
                model.Incident_Category = String.Join(", ", Incident_Category.Where(s => !string.IsNullOrEmpty(s)));

            Mapper.Initialize(cfg => cfg.CreateMap<GccClaim, ClaimGeneral>());
            ClaimGeneral generalClaim = Mapper.Map<ClaimGeneral>(model);

            generalClaim.Policy_Class = string.IsNullOrEmpty(model.Policy_Class) == true ? model.Policy_Class_Selection : model.Policy_Class;

            ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            if (ModelState.IsValid)
            {
                claims = new ClaimServices();
                var result = await claims.TeamUpdateClaimNotification(generalClaim, login.UserId);

                if (result)
                {

                    return RedirectToAction("Index", "ClaimList");
                }
                else
                    TempData["ErrorMsg"] = Messages.errorMessage;
            }

            InitializeModel(model, claims);

            return View(model);
        }
    }
}