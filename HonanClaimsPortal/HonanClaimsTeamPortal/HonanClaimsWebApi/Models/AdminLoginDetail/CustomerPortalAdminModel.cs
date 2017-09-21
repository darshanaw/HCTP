using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApiAccess1.Models.AdminLoginDetail
{
    public class CustomerPortalAdminModel
    {
        public string H_Customerportaladminid { get; set; }
        public string Claim_Team { get; set; }
        public string Account_Can_Add_Claims { get; set; }
        public string Manualclaim_Form { get; set; }
        public string Billing_Method { get; set; }
        public string Fee_Per_Hour { get; set; }
        public string Fee_Per_Billing_Method { get; set; }
        public string Accountname { get; set; }
        public string Customer_Logo { get; set; }
        public bool IsNew { get; set; }
        public string AccountId { get; set; }
    }
   
}
