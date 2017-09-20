using HonanClaimsWebApiAccess1.Models.TeamGetClaimAssigment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
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
                //List<string> TeamList = (List<string>)Session["Teams"];
                //List<st> TeamList = { "Risksmart GCC Team", "Property Claims Team" };
                List<string> TeamList =new List<string>();
                TeamList.Add("Risksmart GCC Team");
                TeamList.Add("Property Claims Team");

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
        public async Task<ActionResult> TeamAssignUserToClaims(string ClaimIdList)
        {
            try
            {
                //string UserId = Session["UserId"].ToString();
                string UserId = "U6UJ9A000009";
                
                TeamGetClaimAssigmentRepo teamGetClaimAssigmentRepo = new TeamGetClaimAssigmentRepo();
                var list = await teamGetClaimAssigmentRepo.TeamAssignUserToClaims(ClaimIdList, UserId);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}