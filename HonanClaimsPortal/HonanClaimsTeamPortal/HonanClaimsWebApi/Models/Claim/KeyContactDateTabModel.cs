using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class KeyContactDateTabModel
    {
        public string ClaimId { get; set; }
        public KeyContact KeyContact { get; set; }
        public KeyDate KeyDate { get; set; }
    }
}
