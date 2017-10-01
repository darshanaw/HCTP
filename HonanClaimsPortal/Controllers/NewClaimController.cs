using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    [AuthorizeUser]
    public class NewClaimController : Controller
    {
        NewClaimModel newClaimModel;
        LookupServices lookupServices;
        PicklistServicecs picklistServicecs;
        // GET: NewClaim
        public ActionResult Index()
        {
            newClaimModel = new NewClaimModel();
            lookupServices = new LookupServices();
            picklistServicecs = new PicklistServicecs();

            InitializeModel(newClaimModel);

            //newClaimModel.Assigned_To_List = picklistServicecs.GetTeamGetUserOfTeam()


            return View(newClaimModel);
        }

        public ActionResult RedirectToDetail(string claimId, string claimTeam)
        {
            switch(claimTeam)
            {
                case ClaimTeams.RisksmartGCC:
                    return RedirectToAction("DetailRisksmartGccClaim", "RisksmartGccClaim", new { id=claimId });
                         case ClaimTeams.RisksmartProperty:
                    return RedirectToAction("DetailRisksmartPropertyClaim", "RisksmartPropertyClaim", new { id = claimId });
                case ClaimTeams.PropertyClaims:
                    return RedirectToAction("DetailPropertyClaim", "PropertyClaim", new { id = claimId });
                case ClaimTeams.GCCClaims:
                    return RedirectToAction("DetailGccClaim", "GccClaim", new { id = claimId });
                default:
                     return RedirectToAction("Index", "ClaimList");

            }
        }

        private void InitializeModel(NewClaimModel model)
        {
            model.Claim_Team_List = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = ClaimTeams.RisksmartGCC, Value=ClaimTeams.RisksmartGCC},
                new SelectListItem(){ Text = ClaimTeams.RisksmartProperty, Value=ClaimTeams.RisksmartProperty},
                new SelectListItem(){ Text = ClaimTeams.PropertyClaims, Value=ClaimTeams.PropertyClaims},
                new SelectListItem(){ Text = ClaimTeams.GCCClaims, Value=ClaimTeams.GCCClaims}
            };

            model.Claim_Type_List = new List<SelectListItem>()
            {
                new SelectListItem(){Text= Enum.GetName(typeof(ClaimType),0), Value = Enum.GetName(typeof(ClaimType),0)},
                new SelectListItem(){Text= Enum.GetName(typeof(ClaimType),1), Value = Enum.GetName(typeof(ClaimType),1)}
            };

        }

        [HttpPost]
        public ActionResult Index(NewClaimModel model)
        {
            if (ModelState.IsValid)
            {
                TempData[TempDataHelper.NewClaimModel] = model;

                
                switch (model.Claim_Team)
                {
                    case ClaimTeams.RisksmartGCC:
                        return RedirectToAction("NewRisksmartGccClaim", "RisksmartGccClaim");
                    case ClaimTeams.RisksmartProperty:
                        return RedirectToAction("NewRisksmartPropertyClaim", "RisksmartPropertyClaim");
                    case ClaimTeams.PropertyClaims:
                        return RedirectToAction("NewPropertyClaim", "PropertyClaim");
                    case ClaimTeams.GCCClaims:
                        return RedirectToAction("NewGccClaim", "GccClaim");
                }

           }

            InitializeModel(model);
            return View(model);
        }
    }
}