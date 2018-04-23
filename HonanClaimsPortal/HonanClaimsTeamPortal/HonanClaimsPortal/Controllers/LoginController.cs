using HonanClaimsWebApi.Models;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
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
                return RedirectToAction("Index", "Home");

            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        
    }
}