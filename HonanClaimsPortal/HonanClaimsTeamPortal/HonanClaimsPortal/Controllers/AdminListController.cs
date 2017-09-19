using HonanClaimsPortal.Models.ProtalLoginAccounts;
using HonanClaimsPortal.Models.ProtalLogingRequest;
using HonanClaimsWebApiAccess1.Models.ProtalLogingRequest;
using HonanClaimsWebApiAccess1.Models.TeamGetPortalRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    public class AdminListController : Controller
    {
        // GET: AdminList
        public ActionResult AdminList()
        {
            return View();
        }

        public ActionResult AdminDetail(string adminId)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetProtalLogingAccount()
        {
            try
            {
                List<ProtalLoginAccountsModel> list = new List<ProtalLoginAccountsModel>();
                ProtalLoginAccountsRepo protalLoginAccountsRepo = new ProtalLoginAccountsRepo();
                list = await protalLoginAccountsRepo.GetProtalLogingAccount();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        [HttpGet]
        public async Task<ActionResult> GetNewProtalLogingRequest()
        {
            try
            {
                List<ProtalLogingRequestModel> list = new List<ProtalLogingRequestModel>();
                ProtalLogingRequestRepo protalLogingRequestRepo = new ProtalLogingRequestRepo();
                list = await protalLogingRequestRepo.GetProtalLogingAccount();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<ActionResult> PortalCutomerAdminDetail()
        {
            try
            {
                List<ProtalLogingRequestModel> list = new List<ProtalLogingRequestModel>();
                ProtalLogingRequestRepo protalLogingRequestRepo = new ProtalLogingRequestRepo();
                list = await protalLogingRequestRepo.GetProtalLogingAccount();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public async Task<ActionResult> TeamGetPortalRegistration(string portalRegRequestId)
        {
            TeamGetPortalRegistrationModel obj = new TeamGetPortalRegistrationModel();
            TeamGetPortalRegistrationRepo teamGetPortalRegistrationRepo = new TeamGetPortalRegistrationRepo();
            obj = await teamGetPortalRegistrationRepo.GetTeamGetPortalRegistration(portalRegRequestId);
            ViewBag.portalRegRequestId = portalRegRequestId.ToString();
            return View(obj);
            //return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> TeamInsertPortalLoginByContact(string portalRegRequestId, string matchingContactId, string userId)
        {
            TeamGetPortalRegistrationRepo teamGetPortalRegistrationRepo = new TeamGetPortalRegistrationRepo();
            var result  =await teamGetPortalRegistrationRepo.TeamInsertPortalLoginByContact(portalRegRequestId, matchingContactId, userId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<ActionResult> portalRegRequestId(string portalRegRequestId)
        {
            TeamGetPortalRegistrationRepo teamGetPortalRegistrationRepo = new TeamGetPortalRegistrationRepo();
            var result = await teamGetPortalRegistrationRepo.TeamDiscardLoginRequest(portalRegRequestId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}