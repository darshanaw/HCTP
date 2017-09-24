using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.LookupModel
{
    public class CRMOCNumSimple
    {
        public string OCNum { get; set; }
        public string PolicyPropertyId { get; set; }
        public string PolicyId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Address { get {
                return Address1+", "+Address2+", "+State+", "+Suburb+" "+Postcode;
            } }
    }
}
