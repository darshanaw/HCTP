using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Claim;
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
        // GET: RisksmartGccClaim
        public ActionResult Index()
        {
            RisksmartGccClaim model = new RisksmartGccClaim();
            model.Claim_Received = false;
            model.Claim_Acknowledged = false;
            model.Review = false;
            model.Outcome_Settlement = false;
            model.Outcome_Declined = false;
            model.Claim_Closed = false;
            model.Litigated = false;

            return View(model);
        }
        
    }
}