using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Models.Common;
using HonanClaimsWebApi.Models.LookupModel;
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
    public class RisksmartPropertyClaimController : Controller
    {
        ClaimTeamLoginModel client;
        ClaimServices claimServices;
        PicklistServicecs pickListServices;
        LookupServices lookupServices;
     
        public ActionResult NewRisksmartPropertyClaim()
        {
            client = Session[SessionHelper.loginCounter] as ClaimTeamLoginModel;

            if (TempData[TempDataHelper.NewClaimModel] == null)
                return RedirectToAction("Index", "NewClaim");

            NewClaimModel newClaimModel = TempData[TempDataHelper.NewClaimModel] as NewClaimModel;

            RisksmartPropertyClaim model = new RisksmartPropertyClaim();

           
            model.Claim_Team = newClaimModel.Claim_Team;
            model.Claim_Team_Name = newClaimModel.Claim_Team;
            model.Account_Name = newClaimModel.Account_Name;
            model.Accountid = newClaimModel.Accountid;
            model.Claim_Type = newClaimModel.Claim_Type;
            model.Oc_Id = newClaimModel.Oc_Name;
            model.Oc_Num = newClaimModel.Oc_Name;
            model.Policy_No = newClaimModel.Policy_No;
            model.Assigned_User = newClaimModel.Assigned_To;
            model.Property_Address_1 = newClaimModel.Property_Address_1;
            model.Property_Address_1 = newClaimModel.Property_Address_2;
            model.Property_Postalcode = newClaimModel.Property_Postalcode;
            model.Property_State = newClaimModel.Property_State;
            model.Property_Suburb = newClaimModel.Property_Suburb;

            // Get Claim Reference #
            //claim.Claim_Reference_Num = claims.GenerateClaimRefNo((Session[SessionHelper.LoginClient] as LoginClient).ClaimTeam);
            //claim.Claim_Reference_Num = claim.Claim_Reference_Num.Replace("\"", "");

            InitializeModel(model);

            return View(model);
        }

        private void InitializeModel(RisksmartPropertyClaim model)
        {
            pickListServices = new PicklistServicecs();
            //Get Regions
            model.IncidentCategoryList = pickListServices.GetPickListItems("Risksmart Property Incident Category");
            model.IncidentCategoryList.Insert(0, new PicklistItem());

            //Get Suburbs
            model.PropertySuburbList = pickListServices.GetPickListItems("H_Suburbs");
            model.PropertySuburbList.Insert(0, new PicklistItem());

            model.Policy_Section_List = pickListServices.GetPickListItems("Risksmart GCC Policy Section");
            model.Policy_Section_List.Insert(0, new PicklistItem());

            model.Policy_Class_List = pickListServices.GetPickListItems("Honan Policy Classes");
            model.Policy_Class_List.Insert(0, new PicklistItem());

            model.Causation_List = pickListServices.GetPickListItems("Risksmart Property Causation");
            model.Causation_List.Insert(0, new PicklistItem());

            model.Client_Group_List = pickListServices.GetPickListItems("Risksmart GCC Client Group");
            model.Client_Group_List.Insert(0, new PicklistItem());
            

            model.YesNoList = new List<string>() { "", "Yes", "No" };
        }
    }
}