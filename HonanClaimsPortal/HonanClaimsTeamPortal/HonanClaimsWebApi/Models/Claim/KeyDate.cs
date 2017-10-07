using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class KeyDate
    {
        public string H_Keydatesid { get; set; }
        public string H_Claimsid { get; set; }
        public int Seq { get; set; }
        public string Key_Date_Description { get; set; }
        public string Key_Date { get; set; }
        public string Imported { get; set; }
    }
}
