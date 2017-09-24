using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Claim;
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

            RisksmartGccClaim model = new RisksmartGccClaim();
            model.Claim_Received = false;
            model.Claim_Acknowledged = false;
            model.Review = false;
            model.Outcome_Settlement = false;
            model.Outcome_Declined = false;
            model.Claim_Closed = false;
            model.Litigated = false;

            LoadDropdowns(model);

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

            LoadDropdowns(model);

            return View(model);
        }

        private void LoadDropdowns(RisksmartGccClaim model)
        {
            claimServices = new ClaimServices();
            pickListServices = new PicklistServicecs();

            model.Assigned_User_List = claimServices.GetClaimTeams();
            model.Claim_Status_List = pickListServices.GetPickListItems("Honan Claim Status");
        }
        
    }
}