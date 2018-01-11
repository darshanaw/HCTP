using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HonanClaimsPortal.Models.ProtalLoginAccounts
{
    public class ProtalLoginAccountsModel
    {
        public string H_CustomerPortalAdminId { get; set; }
        public string PortalAccountName { get; set; }
        public string Account_Name { get; set; }
        public string Accountid { get; set; }
        public string Billing_Method { get; set; }
        public bool CanAddClaims { get; set; }
        public int ActiveLogins { get; set; }
        public string LastLogin { get; set; }
    }
}