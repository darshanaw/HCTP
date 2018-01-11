using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class ClaimFinObject
    {
        public decimal Total_Incurred { get; set; }
        public decimal Total_Reserve { get; set; }
        public decimal Net_Paid_Liability { get; set; }
        public decimal Net_Paid_Defence { get; set; }
        public decimal Liability_Res_Source { get; set; }
        public decimal Defence_Res_Source { get; set; }
        public decimal Liability_Reserve { get; set; }
        public decimal Defence_Reserve { get; set; }
        public decimal Excess { get; set; }
        public decimal Current_Exposure { get; set; }
        public decimal Gross_Paid_To_Date { get; set; }
        public decimal Over_Excess_Paid { get; set; }
        
    }
}

