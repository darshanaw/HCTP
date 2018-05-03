using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult ResetSLXPassword(string userCode, string userId, int daysLeft)
        {
            PasswordResetModel model = new PasswordResetModel();
            model.UserCode = userCode;
            model.UserId = userId;
            model.DaysLeft = daysLeft;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ResetSLXPassword(PasswordResetModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            LoginService service = new LoginService();
            bool result = await service.TeamChangeUserPassword(model);
            if(result)
            {
                //string sKey = model.UserCode.ToLower();
                //string sUser = Convert.ToString(System.Web.HttpContext.Current.Cache[sKey]);
                //if (sUser == null || sUser == String.Empty)
                //{
                //    // No Cache item, so sesion is either expired or user is new sign-on
                //    // Set the cache item and Session hit-test for this user---

                //    int cacheExpiry = int.Parse(ConfigurationManager.AppSettings["CacheExpiry"].ToString());

                //    TimeSpan SessTimeOut = new TimeSpan(0, 0, cacheExpiry, 0, 0);
                //    System.Web.HttpContext.Current.Cache.Insert(sKey, sKey, null, DateTime.MaxValue, SessTimeOut,
                //       System.Web.Caching.CacheItemPriority.NotRemovable, null);
                //}

                if(Session[SessionHelper.claimTeamLoginTemp] != null)
                {
                    ClaimTeamLoginModel client = (ClaimTeamLoginModel)Session[SessionHelper.claimTeamLoginTemp];
                    Session[SessionHelper.loginCounter] = null;
                    client.UserCode = model.UserCode;
                    Session[SessionHelper.claimTeamLogin] = client;
                    if (client.ClaimTimer != null && client.ClaimTimer.IsTimerActive)
                        Session[HonanClaimsPortal.Helpers.SessionHelper.ShowTimer] = true;
                    else
                        Session[HonanClaimsPortal.Helpers.SessionHelper.ShowTimer] = true;

                }
               
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(PasswordResetModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.UserId = (Session[HonanClaimsPortal.Helpers.SessionHelper.claimTeamLogin] as HonanClaimsWebApiAccess1.LoginServices.ClaimTeamLoginModel).UserId;
            LoginService service = new LoginService();
            bool result = await service.TeamChangeUserPassword(model);
            if (result)
                return Json(result, JsonRequestBehavior.AllowGet);

            return View();
        }

    }
}