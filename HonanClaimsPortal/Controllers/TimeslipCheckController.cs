using HonanClaimsWebApi.Models.TimeslipCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    public class TimeslipCheckController : Controller
    {
        // GET: TimeslipCheck
        public async Task<ActionResult> Index()
        {
            var returnModel = new TimeslipReturnModel();
            returnModel.ClaimTeam = await GetComboDetails(arealist.ClaimTeam);
            returnModel.Account = await GetComboDetails(arealist.Account);
            returnModel.Claim = await GetComboDetails(arealist.Claim);
            returnModel.ServiceBy = await GetComboDetails(arealist.ServiceBy);
            return View(returnModel);
        }


        private async Task<List<TimeslipDataModel>> GetComboDetails(arealist area)
        {
            try
            {
                List<TimeslipDataModel> list = new List<TimeslipDataModel>();
                TimeSlipCheckRepo timelistcheckrepo = new TimeSlipCheckRepo();
                list = await timelistcheckrepo.GetComboList(area);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}