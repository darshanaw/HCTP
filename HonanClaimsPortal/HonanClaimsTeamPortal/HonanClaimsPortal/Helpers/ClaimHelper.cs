using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HonanClaimsPortal.Helpers
{
    public class ClaimHelper
    {
        
        public static bool IsManager(string teamName)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)HttpContext.Current.Session[SessionHelper.claimTeamLogin];
            return client.Teams.Contains(teamName);
        }
    }

    public class ClaimTeamManagers
    {
        public const string GCCClaimsManager = "GCC Claims Manager";
        public const string PropertyClaimsManager = "Property Claims Manager";
        public const string RisksmartGCCManager = "Risksmart GCC Manager";
        public const string RisksmartPropertyManager = "Risksmart Property Manager";
    }

    public class ClaimTeams
    {
        public const string GCCClaims = "GCC Claim Team";
        public const string PropertyClaims = "Property Claims Team";
        public const string RisksmartGCC = "Risksmart GCC";//"Risksmart GCC Team";
        public const string RisksmartProperty = "Risksmart Property Team";
    }
}