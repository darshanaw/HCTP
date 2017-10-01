using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.ClaimList;
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
    public class ClaimListController : Controller
    {
        // GET: ClaimList
        public async  Task<ActionResult> Index()
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
            string UserId = client.UserId;

            List<CustomerModel> list = new List<CustomerModel>();
            CustomerListModel CustomerList = new CustomerListModel();
            ClaimListRepo protalLoginAccountsRepo = new ClaimListRepo();
            list = await protalLoginAccountsRepo.GetCustomerList(UserId);
            CustomerList.CustomerList = list;
            return View(CustomerList);
        }

        [HttpGet]
        public async Task<ActionResult> GetClaimList(bool myclaimsOnly, bool isopenClaim, string claimType, string searchText, string cutomerId)
        {
            try
            {
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                string UserId = client.UserId;

                List<ClaimListModel> list = new List<ClaimListModel>();
                ClaimListRepo protalLoginAccountsRepo = new ClaimListRepo();
                list = await protalLoginAccountsRepo.getClaimList(UserId, myclaimsOnly, isopenClaim, claimType, searchText, cutomerId);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetCustomerList()
        {
            try
            {
                ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin];
                string UserId = client.UserId;

                List<CustomerModel> list = new List<CustomerModel>();
                ClaimListRepo protalLoginAccountsRepo = new ClaimListRepo();
                list = await protalLoginAccountsRepo.GetCustomerList(UserId);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}