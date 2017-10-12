using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Services;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    public class ClaimGeneralController : Controller
    {
        public ActionResult AjaxShowBanner(string claimId)
        {
            ClaimServices service = new ClaimServices();
            return Json(service.ShowClaimWarningBanner(claimId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxHideBanner(string claimId)
        {
            ClaimTeamLoginModel client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            ClaimServices service = new ClaimServices();
            return Json(service.HideClaimWarningBanner(claimId, client.UserId), JsonRequestBehavior.AllowGet);
        }

    }
}