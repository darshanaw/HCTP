using HonanClaimsPortal.Helpers;
using HonanClaimsWebApiAccess1.LoginServices;
using HonanClaimsWebApiAccess1.Models.TeamGetClaimAssigment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    [AuthorizeUser]
    public class ClaimAssignmentController : Controller
    {
        // GET: ClaimAssignment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> TeamGetClaimAssigmentList()
        {
            try
            {
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                List<string> TeamList = client.Teams;
                List<HonanClaimsWebApiAccess1.Models.TeamGetClaimAssigment.CRMPicklistItem> list = new List<HonanClaimsWebApiAccess1.Models.TeamGetClaimAssigment.CRMPicklistItem>();
                TeamGetClaimAssigmentRepo teamGetClaimAssigmentRepo = new TeamGetClaimAssigmentRepo();
                list = await teamGetClaimAssigmentRepo.TeamGetClaimAssigmentList(TeamList);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetTeamGetUsersofTeam(string TeamName)
        {
            try
            {
                List<CRMPicklistItemModel> list = new List<CRMPicklistItemModel>();
                TeamGetClaimAssigmentRepo teamGetClaimAssigmentRepo = new TeamGetClaimAssigmentRepo();
                list = await teamGetClaimAssigmentRepo.GetTeamGetUsersofTeam(TeamName);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public async Task<ActionResult> TeamAssignUserToClaims(string ClaimIdList,string UserId)
        {
            try
            {               
                TeamGetClaimAssigmentRepo teamGetClaimAssigmentRepo = new TeamGetClaimAssigmentRepo();
                var list = await teamGetClaimAssigmentRepo.TeamAssignUserToClaims(ClaimIdList, UserId);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult RedirectToDetail(string claimId, string claimTeam, string tab)
        {
            switch (claimTeam)
            {
                case ClaimTeams.RisksmartGCC:
                    return RedirectToAction("DetailRisksmartGccClaim", "RisksmartGccClaim", new { id = claimId, tab = tab });
                case ClaimTeams.RisksmartProperty:
                    return RedirectToAction("DetailRisksmartPropertyClaim", "RisksmartPropertyClaim", new { id = claimId, tab = tab });
                case ClaimTeams.PropertyClaims:
                    return RedirectToAction("DetailPropertyClaim", "PropertyClaim", new { id = claimId, tab = tab });
                case ClaimTeams.GCCClaims:
                    return RedirectToAction("DetailGccClaim", "GccClaim", new { id = claimId, tab = tab });
                default:
                    return RedirectToAction("Index", "ClaimList");

            }
        }
    }
}