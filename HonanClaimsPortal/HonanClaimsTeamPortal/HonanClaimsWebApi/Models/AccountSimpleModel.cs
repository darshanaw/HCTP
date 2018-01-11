using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models
{
    public class AccountSimpleModel
    {
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string AccountManager { get; set; }
        public string BillingMethod { get; set; }
        public string ServiceRate { get; set; }
    }
}
