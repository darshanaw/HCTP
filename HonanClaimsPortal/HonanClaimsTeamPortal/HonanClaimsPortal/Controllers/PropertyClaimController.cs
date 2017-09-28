﻿using HonanClaimsPortal.Helpers;
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
    public class PropertyClaimController : Controller
    {
        ClaimTeamLoginModel client;
        ClaimServices claims;
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
            claim.AccountManager = newClaimModel.AccountManager;

            //claim.Claim_Team = login.ClaimTeam;
            //claim.Claim_Type = string.IsNullOrEmpty(Request.QueryString[QueryStringHelper.PageType]) ? Session[SessionHelper.Page].ToString() : Request.QueryString[QueryStringHelper.PageType];

            // Get Claim Reference #
            //  claim.Claim_Reference_Num = claims.GenerateClaimRefNo((Session[SessionHelper.LoginClient] as LoginClient).ClaimTeam);
            //  claim.Claim_Reference_Num = claim.Claim_Reference_Num.Replace("\"", "");


            InitializeModel(claim);

            return View(claim);
        }

        private void InitializeModel(PropertyClaim claim)
        {
            // Add Gender list
            claim.GenderList = new List<string>()
            {
                "","Male","Female"
            };

            claim.Policy_Section_List = pickListServices.GetPickListItems("Risksmart GCC Policy Section");
            claim.Policy_Section_List.Insert(0, new PicklistItem());

            //Get Suburbs
            claim.PropertySuburbList = pickListServices.GetPickListItems("H_Suburbs");
            claim.PropertySuburbList.Insert(0, new PicklistItem());

            //Get States
            claim.PropertyStateList = pickListServices.GetPickListItems("H_State");
            claim.PropertyStateList.Insert(0, new PicklistItem());

            claim.Policy_Class_List = pickListServices.GetPickListItems("Honan Policy Classes");
            claim.Policy_Class_List.Insert(0, new PicklistItem());

            claim.Causation_List = pickListServices.GetPickListItems("Risksmart Property Causation");
            claim.Causation_List.Insert(0, new PicklistItem());

            claim.YesNoList = new List<string>() { "", "Yes", "No" };

            //claim.Oc_Num = login.AccountName;
            //claim.Accountid = login.AccountId;

            //lookupServices = new LookupServices();
            //List<PolicySimple> policies = lookupServices.GetPolicies("", login.AccountId);

            //if (policies.Count == 1)
            //{
            //    claim.Policy_No = policies[0].PolicyNo;
            //    claim.Property_Address_1 = policies[0].Address1;
            //    claim.Property_Address_2 = policies[0].Address2;
            //    claim.Property_Suburb = policies[0].Suburb;
            //    claim.Property_State = policies[0].State;
            //    claim.Property_Postalcode = policies[0].Postcode;
            //}
        }
    }
}