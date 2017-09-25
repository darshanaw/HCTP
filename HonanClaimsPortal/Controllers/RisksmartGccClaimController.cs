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
            client = Session[SessionHelper.loginCounter] as ClaimTeamLoginModel;

            if (TempData[TempDataHelper.NewClaimModel] == null)
                return RedirectToAction("Index", "NewClaim");

            NewClaimModel newClaimModel = TempData[TempDataHelper.NewClaimModel] as NewClaimModel;

            RisksmartGccClaim model = new RisksmartGccClaim();
            model.Claim_Received = false;
            model.Claim_Acknowledged = false;
            model.Review = false;
            model.Outcome_Settlement = false;
            model.Outcome_Declined = false;
            model.Claim_Closed = false;
            model.Litigated = false;

            model.Claim_Team = newClaimModel.Claim_Team;
            model.Claim_Team_Name = newClaimModel.Claim_Team;
            model.Account_Name = newClaimModel.Account_Name;
            model.Accountid = newClaimModel.Accountid;
            model.Claim_Type = newClaimModel.Claim_Type;
            model.Oc_Num = newClaimModel.Oc_No;
            model.Policy_No = newClaimModel.Policy_No;
            model.Assigned_User = newClaimModel.Assigned_To;
            model.Property_Address_1 = newClaimModel.Property_Address_1;
            model.Property_Address_1 = newClaimModel.Property_Address_2;
            model.Property_Postalcode = newClaimModel.Property_Postalcode;
            model.Property_State = newClaimModel.Property_State;
            model.Property_Suburb = newClaimModel.Property_Suburb;

            InitializeModel(model);

            return View(model);
        }
        // GET: RisksmartGccClaim
        public ActionResult DetailRisksmartGccClaim()
        {
            client = Session[SessionHelper.loginCounter] as ClaimTeamLoginModel;

            RisksmartGccClaim model = new RisksmartGccClaim();
            model.Claim_Received = false;
            model.Claim_Acknowledged = false;
            model.Review = false;
            model.Outcome_Settlement = false;
            model.Outcome_Declined = false;
            model.Claim_Closed = false;
            model.Litigated = false;

            InitializeModel(model);

            return View(model);
        }

        private void InitializeModel(RisksmartGccClaim model)
        {
            claimServices = new ClaimServices();
            pickListServices = new PicklistServicecs();

            model.Assigned_User_List = claimServices.GetClaimTeams();
            model.Claim_Status_List = pickListServices.GetPickListItems("Honan Claim Status");

            //Get Reported By Types
            model.ReportedByTypeList = pickListServices.GetPickListItems("Risksmart GCC Reported By Type");
            model.ReportedByTypeList.Insert(0, new PicklistItem());

            //Get Regions
            model.RegionList = pickListServices.GetPickListItems("H_StoreRegion");
            model.RegionList.Insert(0, new PicklistItem());

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
            model.IncidentCategoryList = pickListServices.GetPickListItems("GCC Incident Category");
            model.IncidentCategoryList.Insert(0, new PicklistItem());

            //Get Incident Party Types
            model.IncidentPartyTypeList = pickListServices.GetPickListItems("Risksmart GCC Incident Party Type");
            model.IncidentPartyTypeList.Insert(0, new PicklistItem());

            model.Policy_Section_List = pickListServices.GetPickListItems("Risksmart GCC Policy Section");
            model.Policy_Section_List.Insert(0, new PicklistItem());

            model.Policy_Class_List = pickListServices.GetPickListItems("Honan Policy Classes");
            model.Policy_Class_List.Insert(0, new PicklistItem());

            // Add CCTV available list
            model.CCTVAvailableList = new List<string>() { "", "Yes", "No" };

            // Add CCTV viewed list
            model.CCTVViewedList = new List<string>() { "", "Yes", "No" };
        }
        
    }
}