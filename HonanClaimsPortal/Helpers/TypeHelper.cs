using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HonanClaimsPortal.Helpers
{
    public class TypeHelper
    {
    }

    public class CookieHelper
    {
        public static string UserCode_ = "UserCode_";
        public static string Password_ = "Password_";
        public static string CookieObject_ = "CookieObject_";
        
    }

    public class SessionHelper
    {
        public static string claimTeamLogin = "claimTeamLogin";
        public static string loginCounter = "loginCounter";
    }

    public class TempDataHelper
    {
        public static string NewClaimModel = "NewClaimModel";
    }

    public enum ClaimType
    {
        Claim = 0,
        Notification = 1
    }

}