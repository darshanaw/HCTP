using HonanClaimsPortal.Models.ProtalLoginAccounts;
using HonanClaimsPortal.Models.ProtalLogingRequest;
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
        public async Task<ActionResult>GetStatus(string brunchid)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
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

    }
}