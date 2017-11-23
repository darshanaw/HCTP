using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmailWindow()
        {
            return View();
        }

    }
}