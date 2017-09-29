using HonanClaimsPortal.Helpers;
using HonanClaimsPortal.ViewModels;
using HonanClaimsWebApi.Models.Views;
using HonanClaimsWebApi.Services;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    [AuthorizeUser]
    public class ViewPagesController : Controller
    {
        ClaimServices claims;
        List<ClaimNotificationSimple> claimsList;
        // GET: ViewPages
        public ActionResult ViewClaims()
        {
            return View();
        }

        public ActionResult ViewNotifications()
        {
            return View();
        }

        public ActionResult AjaxClaimsLoad(bool? isOpenClaim)
        {
            isOpenClaim = isOpenClaim ?? true; 
            claimsList = new List<ClaimNotificationSimple>();
            claimsList = GetClaims(isOpenClaim.Value);
            return Json(claimsList, JsonRequestBehavior.AllowGet);
        }

        private List<ClaimNotificationSimple> GetClaims(bool isOpenClaim)
        {
            claims = new ClaimServices();
            claimsList = new List<ClaimNotificationSimple>();
            ClaimTeamLoginModel user = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            claimsList = claims.GetClaimsObjectList(isOpenClaim, user.UserId);// user.UserId); "Q6UJ9A00KFLR"
            return claimsList;
        }

        public ActionResult AjaxNotificationsLoad()
        {
            claimsList = new List<ClaimNotificationSimple>();
            claimsList = GetNotifications();
            return Json(claimsList, JsonRequestBehavior.AllowGet);
        }

        private List<ClaimNotificationSimple> GetNotifications()
        {
            claims = new ClaimServices();
            claimsList = new List<ClaimNotificationSimple>();
            ClaimTeamLoginModel user = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            claimsList = claims.GetNotificationsObjectList(user.UserId);// user.UserId); "Q6UJ9A00KFLR"
            return claimsList;
            
        }
               
    }
}