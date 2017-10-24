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

        public static bool IsMemberOfTeam(string teamName)
        {
            ClaimTeamLoginModel client = (ClaimTeamLoginModel)HttpContext.Current.Session[SessionHelper.claimTeamLogin];
            return client.Teams.IndexOf(teamName) != -1 ? true : false;
        }

        public static string GetFullTeamName(string teamNameShort)
        {
            switch (teamNameShort)
            {
                case ClaimTeams.RisksmartGCC:
                    return ClaimTeamsByTeamNames.RisksmartGCC;

                case ClaimTeams.RisksmartProperty:
                    return ClaimTeamsByTeamNames.RisksmartProperty;

                case ClaimTeams.PropertyClaims:
                    return ClaimTeamsByTeamNames.PropertyClaims;

                case ClaimTeams.GCCClaims:
                    return ClaimTeamsByTeamNames.GCCClaims;

            }

            return string.Empty;
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
        public const string GCCClaims = "GCC Claims"; //"GCC Claim Team"
        public const string PropertyClaims = "Property Claims"; //"Property Claims Team"
        public const string RisksmartGCC = "Risksmart GCC";//"Risksmart GCC Team";
        public const string RisksmartProperty = "Risksmart Property"; //"Risksmart Property Team"
    }


    public class ClaimTeamsByTeamNames
    {
        public const string GCCClaims = "GCC Claim Team"; 
        public const string PropertyClaims = "Property Claims Team"; 
        public const string RisksmartGCC = "Risksmart GCC Team";
        public const string RisksmartProperty = "Risksmart Property Team"; 
    }

}