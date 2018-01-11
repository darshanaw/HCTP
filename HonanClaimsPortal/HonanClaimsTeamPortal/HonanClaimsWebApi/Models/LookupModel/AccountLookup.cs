using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HonanClaimsPortal.Models.LookupModel
{
    public class AccountLookup
    {
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string AccountManager { get; set; }
        public string BillingMethod { get; set; }
        
    }
}