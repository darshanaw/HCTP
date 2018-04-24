using HonanClaimsPortal.Helpers;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HonanClaimsPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_BeginRequest()
        //{
        //    if (HttpContext.Current.Session != null && HttpContext.Current.Session[SessionHelper.claimTeamLogin] != null)
        //    {
        //        string sKey = ((ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin]).UserCode;

        //        // Accessing the Cache Item extends the Sliding Expiration automatically
        //        string sUser = (string)HttpContext.Current.Cache[sKey];
        //    }
        //}

        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (context != null && context.Session != null && context.Session[SessionHelper.claimTeamLogin] != null)
            {
                string sKey = ((ClaimTeamLoginModel)Session[SessionHelper.claimTeamLogin]).UserCode;
                // Accessing the Cache Item extends the Sliding Expiration automatically
                string sUser = (string)HttpContext.Current.Cache[sKey];
            }
        }
    }
}
