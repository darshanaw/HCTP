using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class KeyContactDateTabModel
    {
        public string ClaimId_KCD_Tab { get; set; }
        public string Claim_Ref_No_KCD_Tab { get; set; }
        public KeyContact KeyContact { get; set; }
        public KeyDate KeyDate { get; set; }
    }
}
