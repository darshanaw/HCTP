using HonanClaimsWebApi.Models.Common;
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
        public string H_ClaimNo_KCD_Date { get; set; }
        public int Seq { get; set; }
        public int MaxSeq { get; set; }
        public string Key_Date_Description { get; set; }
        public DateTime Key_Date { get; set; }
        public string Imported { get; set; }
        public List<PicklistItem> Key_Date_Description_List { get; set; }
    }
}
