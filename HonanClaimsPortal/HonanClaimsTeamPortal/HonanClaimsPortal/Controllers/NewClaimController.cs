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
    public class NewClaimController : Controller
    {
        NewClaimModel newClaimModel;
        LookupServices lookupServices;
        // GET: NewClaim
        public ActionResult Index()
        {
            newClaimModel = new NewClaimModel();
            lookupServices = new LookupServices();

            newClaimModel.Claim_Team_List = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = ClaimTeams.RisksmartGCC, Value=ClaimTeams.RisksmartGCC},
                new SelectListItem(){ Text = ClaimTeams.RisksmartProperty, Value=ClaimTeams.RisksmartProperty},
                new SelectListItem(){ Text = ClaimTeams.PropertyClaims, Value=ClaimTeams.PropertyClaims},
                new SelectListItem(){ Text = ClaimTeams.GCCClaims, Value=ClaimTeams.GCCClaims}
            };

            newClaimModel.Claim_Type_List = new List<SelectListItem>()
            {
                new SelectListItem(){Text= Enum.GetName(typeof(ClaimType),0), Value = Enum.GetName(typeof(ClaimType),0)},
                new SelectListItem(){Text= Enum.GetName(typeof(ClaimType),1), Value = Enum.GetName(typeof(ClaimType),1)}
            };

          
            return View(newClaimModel);
        }
    }
}