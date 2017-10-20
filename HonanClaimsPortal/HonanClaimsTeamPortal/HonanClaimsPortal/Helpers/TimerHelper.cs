using HonanClaimsWebApi.Models;
using HonanClaimsWebApi.Models.Billing;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HonanClaimsPortal.Helpers
{
    public class TimerHelper
    {
        public static ClaimTimer GetTimerStart()
        {
            BillingRepo repo = new BillingRepo();
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)HttpContext.Current.Session[SessionHelper.claimTeamLogin];
            return repo.GetTimerStart(client.UserId);

        }
    }
}