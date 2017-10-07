using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class PaymentSimple
    {
        public string H_Paymentsid { get; set; }
        public string Date_Invoice_Received { get; set; }
        public string Invoice_Reference { get; set; }
        public string Payee_Account_Name { get; set; }
        public decimal? Total_Gross { get; set; }
        public decimal? Total_Net { get; set; }
        public string Payment_Status { get; set; }
    }
}
