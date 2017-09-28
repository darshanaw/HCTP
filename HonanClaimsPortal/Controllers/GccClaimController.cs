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
    public class GccClaimController : Controller
    {
        ClaimTeamLoginModel client;
        ClaimServices claims;
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
            
            claims = new ClaimServices();

            claim.Claim_Team = newClaimModel.Claim_Team;
            claim.Claim_Team_Name = newClaimModel.Claim_Team;
            claim.Account_Name = newClaimModel.Account_Name;
            claim.Accountid = newClaimModel.Accountid;
            claim.Claim_Type = newClaimModel.Claim_Type;
            claim.Oc_Id = newClaimModel.Oc_No;
            claim.Oc_Num = newClaimModel.Oc_Name;
            claim.Policy_No = newClaimModel.Policy_No;
            claim.Assigned_User = newClaimModel.Assigned_To;
            claim.Property_Address_1 = newClaimModel.Property_Address_1;
            claim.Property_Address_1 = newClaimModel.Property_Address_2;
            claim.Property_Postalcode = newClaimModel.Property_Postalcode;
            claim.Property_State = newClaimModel.Property_State;
            claim.Property_Suburb = newClaimModel.Property_Suburb;

            // Get Claim Reference #
            //claim.Claim_Reference_Num = claims.GenerateClaimRefNo((Session[SessionHelper.LoginClient] as LoginClient).ClaimTeam);
            //claim.Claim_Reference_Num = claim.Claim_Reference_Num.Replace("\"", "");

            InitializeModel(claim);

            return View(claim);
        }

        private void InitializeModel(GccClaim claim)
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
        }
    }
}