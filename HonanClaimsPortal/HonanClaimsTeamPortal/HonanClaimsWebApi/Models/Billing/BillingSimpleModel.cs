using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Billing
{
    public class BillingSimpleModel
    {
        public string BillingId { get; set; }
        public string ClaimRefNo { get; set; }
        public string Customer { get; set; }
        public string ServiceBy { get; set; }
        public DateTime ServiceDate { get; set; }
        public string StartTime { get; set; }
        public int Qty { get; set; }
        public decimal Billable { get; set; }
        public string Checked { get; set; }
        public string Invoiced { get; set; }
        public string Work_Done { get; set; }
        public int Unit { get; set; }
    }


    public class MyBillingModel
    {
       public List<CustomerUserModel> CustomerUserModel { get; set;}
        public List<ServicesUserModel> ServicesUserModel { get; set; }
        public string ShowMe { get; set; }
        public string CustomerId { get; set; }
        public string ServiceUserId { get; set; }
        public string ServiceFromDate { get; set; }
        public string ServiceToDate { get; set; }
        public string TotalQ { get; set; }
    }

    public class CustomerUserModel
    {
        public string Order { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
    }

    public class ServicesUserModel
    {
        public string Order { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
    }
}
