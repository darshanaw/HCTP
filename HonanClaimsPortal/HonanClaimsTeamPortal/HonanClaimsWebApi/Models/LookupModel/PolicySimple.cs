using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.LookupModel
{
    public class PolicySimple
    {
        public string PolicyId { get; set; }
        public string PolicyNo { get; set; }
        //public string Account { get; set; }
        //public string AccountManager { get; set; }
        public string PolicyClass { get; set; }
        public string PolicyExpiry { get; set; }
        public string PolicyStatus { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
    }
}
