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
            model.Property_Address_2 = newClaimModel.Property_Address_2;
            model.Property_Postalcode = newClaimModel.Property_Postalcode;
            model.Property_State = newClaimModel.Property_State;
            model.Property_Suburb = newClaimModel.Property_Suburb;
            model.Insurer = newClaimModel.Insurer;
            model.Insurer_Name = newClaimModel.InsurerName;
            model.Insured_Name = newClaimModel.Insured_Name;
            model.Policy_Class = newClaimModel.Policy_Class;
            model.Policy_Id = newClaimModel.Policy_Id;
            model.Account_Manager_Property = newClaimModel.Account_Manager_Property;
            model.Date_Of_Loss = newClaimModel.Incident_Date;
            model.Incident_Date = newClaimModel.Incident_Date;
            model.Policy_Id = newClaimModel.Policy_Id;

            // Get Claim Reference #
            model.Claim_Reference_Num = claimServices.GenerateClaimRefNo(model.Claim_Team);
            model.Claim_Reference_Num = model.Claim_Reference_Num.Replace("\"", "");

            InitializeModel(model, claimServices);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> NewRisksmartPropertyClaim(RisksmartPropertyClaim claim, IEnumerable<string> Region, IEnumerable<string> Incident_Category, IEnumerable<HttpPostedFileBase> upfiles)
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
                    generalClaim.Claim_Received = true;
                    generalClaim.Claim_Received_Date = DateTime.Today;
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

        public ActionResult DetailRisksmartPropertyClaim(string id)
        {
            client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;

            claimServices = new ClaimServices();
            //Mapper mapper = new 
            Mapper.Initialize(cfg => cfg.CreateMap<ClaimGeneral, RisksmartPropertyClaim>());
            RisksmartPropertyClaim model = Mapper.Map<RisksmartPropertyClaim>(claimServices.GetClaimNotification(id));

            InitializeModel(model, claimServices);

            return View(model);
        }

            private void InitializeModel(RisksmartPropertyClaim model, ClaimServices claimServices)
        {
            pickListServices = new PicklistServicecs();

            if (ClaimHelper.IsManager(HonanClaimsPortal.Helpers.ClaimTeamManagers.RisksmartPropertyManager))
                model.Assigned_User_List = claimServices.GetUsers(new List<string>() { "Risksmart Property Manager" });

            model.Claim_Status_List = pickListServices.GetPickListItems("Honan Claim Status");

            //Get Regions
            model.IncidentCategoryList = pickListServices.GetPickListItems("Risksmart Property Incident Category");
            model.IncidentCategoryList.Insert(0, new PicklistItem());

            //Get Outcome List
            model.Outcome_List = pickListServices.GetPickListItems("Risksmart Property Outcome");
            model.Outcome_List.Insert(0, new PicklistItem());

            //Get Suburbs
            model.PropertySuburbList = pickListServices.GetPickListItems("H_Suburbs");
            model.PropertySuburbList.Insert(0, new PicklistItem());

            //model.Policy_Section_List = pickListServices.GetPickListItems("Risksmart Property Policy Section");
            //model.Policy_Section_List.Insert(0, new PicklistItem());

            model.Policy_Class_List = pickListServices.GetPickListItems("Honan Policy Classes");
            model.Policy_Class_List.Insert(0, new PicklistItem());

            model.Policy_Section_List = GetPolicySectionDataAsList(model.Policy_Class);
            model.Policy_Section_List.Insert(0, new PicklistItem());

            model.Causation_List = pickListServices.GetPickListItems("Risksmart Property Causation");
            model.Causation_List.Insert(0, new PicklistItem());

            model.Client_Group_List = pickListServices.GetPickListItems("Risksmart Property Client Group");
            model.Client_Group_List.Insert(0, new PicklistItem());

            //model.ClientGroupList = pickListServices.GetPickListItems("Risksmart GCC Client Group");
            //model.ClientGroupList.Insert(0, new PicklistItem());

            model.Notification_Status_List = pickListServices.GetPickListItems("Honan Notification Status");
            model.Notification_Status_List.Insert(0, new PicklistItem());

            model.YesNoList = new List<string>() { "", "Yes", "No" };

            model.Liability_Reserve = model.Liability_Res_Source;
            model.Defence_Reserve = model.Defence_Res_Source;

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

            model.Claim_Received = model.Claim_Received == null || model.Claim_Received == false ? false : true;
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

            model.IncidentTypeList = pickListServices.GetPickListItems("Risksmart Property Incident Type");

            model.Bodily_Location_List = pickListServices.GetPickListItems("Honan Claims Bodily Location");
            model.Bodily_Location_List.Insert(0, new PicklistItem());
        }

        [HttpPost]
        public async  Task<ActionResult> DetailRisksmartPropertyClaim(RisksmartPropertyClaim model, IEnumerable<string> Incident_Category)
        {
            Session[SessionHelper.StoreobjectList] = null;
            PicklistServicecs picklistService = new PicklistServicecs();
            ClaimServices claims = new ClaimServices();

            if (Incident_Category != null)
                model.Incident_Category = String.Join(", ", Incident_Category.Where(s => !string.IsNullOrEmpty(s)));

            Mapper.Initialize(cfg => cfg.CreateMap<RisksmartPropertyClaim, ClaimGeneral>());
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

        public ActionResult GetPolicySectionData(string policyClass)
        {
            string pickListName = string.Empty;
            if (!string.IsNullOrEmpty(policyClass))
            {
                switch (policyClass)
                {
                    case "Strata Title":
                    case "Commercial (STR-COMM)":
                    case "Residential (STR-RES)":
                    case "Ancillary":
                    case "New Development":
                    case "Company Title":
                        {
                            pickListName = "Risksmart Property Policy Section 1";
                            break;
                        }

                    case "Landlord Building":
                        {
                            pickListName = "Risksmart Property Policy Section 2";
                            break;
                        }
                    case "Landlord Contents":
                        {
                            pickListName = "Risksmart Property Policy Section 3";
                            break;
                        }
                    case "Community Association":
                    case "Common Property Only":
                        {
                            pickListName = "Risksmart Property Policy Section 4";
                            break;
                        }
                    default:
                        {
                            return Json(new List<PicklistItem>(), JsonRequestBehavior.AllowGet);
                        }

                }

                PicklistServicecs services = new PicklistServicecs();
                return Json(services.GetPickListItems(pickListName), JsonRequestBehavior.AllowGet);
            }

            return Json(new List<PicklistItem>(), JsonRequestBehavior.AllowGet);
        }

        public List<PicklistItem> GetPolicySectionDataAsList(string policyClass)
        {
            string pickListName = string.Empty;
            if (!string.IsNullOrEmpty(policyClass))
            {
                switch (policyClass)
                {
                    case "Strata Title":
                    case "Commercial (STR-COMM)":
                    case "Residential (STR-RES)":
                    case "Ancillary":
                    case "New Development":
                    case "Company Title":
                        {
                            pickListName = "Risksmart Property Policy Section 1";
                            break;
                        }

                    case "Landlord Building":
                        {
                            pickListName = "Risksmart Property Policy Section 2";
                            break;
                        }
                    case "Landlord Contents":
                        {
                            pickListName = "Risksmart Property Policy Section 3";
                            break;
                        }
                    case "Community Association":
                    case "Common Property Only":
                        {
                            pickListName = "Risksmart Property Policy Section 4";
                            break;
                        }
                    default:
                        {
                            return new List<PicklistItem>();
                        }

                }
                PicklistServicecs services = new PicklistServicecs();
                return services.GetPickListItems(pickListName);
            }

            return new List<PicklistItem>();

        }


    }
}