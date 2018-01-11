using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Policies;
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
    public class PoliciesController : Controller
    {
        // GET: Policies
        public async Task<ActionResult> Index()
        {
            PolicyReturnModel model = new PolicyReturnModel();
            PoliciesRepo policiesRepo = new PoliciesRepo();

            //model.PolicyClass = await policiesRepo.GetPloicyClass();
            model.IsActive = false;
            //model.PolicyType = await policiesRepo.GetPolicyPolicyType();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetPolicyList(bool IsActive,string PolicyNumber,string Associate,string Customer,string Underwriter,string PolicyType,string PolicyClass)
        {
            try
            {
                List<PolictSimple> list = new List<PolictSimple>();
                PoliciesRepo policiesRepo = new PoliciesRepo();
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];

                list = await policiesRepo.GetPolicyList(IsActive, PolicyNumber, Associate, Customer, Underwriter, PolicyType, PolicyClass, client.UserId);
                return new JsonResult() { Data = list, MaxJsonLength = 86753090, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                //return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> PoliciesDetail(string PolicyId)
        {
            PoliciesRepo policiesRepo = new PoliciesRepo();
            PolicyModel model = new PolicyModel();
            model = await policiesRepo.GetPoliciesDetail(PolicyId);
            return View(model);
        }

        public async Task<ActionResult> UpdatePolicy(string ocNum, string insuredName, string policyId)
        {
            PoliciesRepo policiesRepo = new PoliciesRepo();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            PolicyModel model = new PolicyModel();
            return new JsonResult() { Data = await policiesRepo.UpdatePolicy(ocNum, insuredName, policyId, client.UserId), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}