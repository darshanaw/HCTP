using HonanClaimsWebApi.Models.Common;
using HonanClaimsWebApi.Models.TimeslipCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class BillingTabModel
    {
        public string ServiceBy { get; set; }
        public List<TimeslipDataModel> ServiceByList { get; set; }
        public DateTime? Service_Date_From { get; set; }
        public DateTime? Service_Date_To { get; set; }
        public double Total_Qty { get; set; }
        public string Invoiced { get; set; }
        public string InvoiceNumber { get; set; }
        public List<string> YesNoList { get; set; }
        public List<CRMPicklistItem> InvoiceNumberList { get; set; }
    }
}
