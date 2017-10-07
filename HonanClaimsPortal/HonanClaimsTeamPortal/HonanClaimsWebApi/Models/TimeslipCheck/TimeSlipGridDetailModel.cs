using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.TimeslipCheck
{
    public class TimeSlipGridDetailModel
    {
        public string BillingId { get; set; }
        public string ClaimRefNo { get; set; }
        public string Customer { get; set; }
        public string ServiceBy { get; set; }
        public string ServiceDate { get; set; }
        public string StartTime { get; set; }
        public string Invoiced { get; set; }
        public string Checked { get; set; }
        public int Qty { get; set; }
        public decimal Billable { get; set; }
    }
}
