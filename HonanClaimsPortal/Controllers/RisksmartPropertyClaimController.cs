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

            claimServices = new ClaimServices();

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
            model.Assigned_User = newClaimModel.Assigned_To_Id;
            model.Property_Address_1 = newClaimModel.Property_Address_1;
            model.Property_Address_1 = newClaimModel.Property_Address_2;
            model.Property_Postalcode = newClaimModel.Property_Postalcode;
            model.Property_State = newClaimModel.Property_State;
            model.Property_Suburb = newClaimModel.Property_Suburb;

            // Get Claim Reference #
            model.Claim_Reference_Num = claimServices.GenerateClaimRefNo(model.Claim_Team);
            model.Claim_Reference_Num = model.Claim_Reference_Num.Replace("\"", "");

            InitializeModel(model, claimServices);

            return View(model);
        }

        [HttpPost]
        public ActionResult NewRisksmartPropertyClaim(RisksmartPropertyClaim claim, IEnumerable<string> Region, IEnumerable<string> Incident_Category)
        {
            try
            {
                pickListServices = new PicklistServicecs();
                claimServices = new ClaimServices();
                client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;

                if (claim.Region != null)
                    claim.Region = String.Join(",", Region.Where(s => !string.IsNullOrEmpty(s)));
                if (claim.Incident_Category != null)
                    claim.Incident_Category = String.Join(",", Incident_Category.Where(s => !string.IsNullOrEmpty(s)));

                Mapper.Initialize(cfg => cfg.CreateMap<RisksmartPropertyClaim, ClaimGeneral>());
                ClaimGeneral generalClaim = Mapper.Map<ClaimGeneral>(claim);

                if (ModelState.IsValid)
                {
                    claimServices = new ClaimServices();
                    generalClaim.Claim_Team_Name = claim.Claim_Team;
                    generalClaim.Accountid = claim.Accountid;
                    generalClaim.Account_Name = claim.Account_Name;
                    var result = claimServices.TeamInsertClaimNotification(generalClaim, client.UserId);

                   
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

        public ActionResult DetailRisksmartPropertyClaim(string id)
        {
            client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;

            claimServices = new ClaimServices();
            //Mapper mapper = new 
            Mapper.Initialize(cfg => cfg.CreateMap<ClaimGeneral, RisksmartPropertyClaim>());
            RisksmartPropertyClaim model = Mapper.Map<RisksmartPropertyClaim>(claimServices.GetClaimNotification(id));

            model.Claim_Received = false;
            model.Claim_Acknowledged = false;
            model.Review = false;
            model.Outcome_Settlement = false;
            model.Outcome_Declined = false;
            model.Claim_Closed = false;
            model.Litigated = false;

            InitializeModel(model, claimServices);

            return View(model);
        }

            private void InitializeModel(RisksmartPropertyClaim model, ClaimServices claimServices)
        {
            pickListServices = new PicklistServicecs();

            if (ClaimHelper.IsManager(HonanClaimsPortal.Helpers.ClaimTeamManagers.RisksmartGCCManager))
                model.Assigned_User_List = claimServices.GetUsers(new List<string>() { "Risksmart GCC Manager" });

            model.Claim_Status_List = pickListServices.GetPickListItems("Honan Claim Status");

            //Get Regions
            model.IncidentCategoryList = pickListServices.GetPickListItems("Risksmart Property Incident Category");
            model.IncidentCategoryList.Insert(0, new PicklistItem());

            //Get Outcome List
            model.Outcome_List = pickListServices.GetPickListItems("Risksmart GCC Outcome");
            model.Outcome_List.Insert(0, new PicklistItem());

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

            model.ClientGroupList = pickListServices.GetPickListItems("Risksmart GCC Client Group");
            model.ClientGroupList.Insert(0, new PicklistItem());

            model.YesNoList = new List<string>() { "", "Yes", "No" };

            model.Liability_Reserve = model.Liability_Res_Source;
            model.Defence_Reserve = model.Defence_Res_Source;

            //Calculations
            PaymentServices paymentServices = new PaymentServices();
            decimal val, liabilityReserveGross = 0, defenceReserveGross = 0;


            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(model.H_Claimsid, "Liability Reserve", true), out val))
            {
                liabilityReserveGross = val;
                model.Liability_Reserve = model.Liability_Reserve - val;
            }

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(model.H_Claimsid, "Defence Reserve", true), out val))
            {
                defenceReserveGross = val;
                model.Defence_Reserve = model.Defence_Reserve - val;
            }

            model.Total_Reserve = model.Liability_Reserve + model.Defence_Reserve;

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(model.H_Claimsid, "Liability Reserve", false), out val))
                model.Net_Paid_Liability = val;

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(model.H_Claimsid, "Defence Reserve", false), out val))
                model.Net_Paid_Defence = val;

            model.Gross_Paid_To_Date = liabilityReserveGross + defenceReserveGross;

            model.Total_Incurred = model.Total_Reserve + model.Net_Paid_Liability + model.Net_Paid_Defence;
        }
    }
}