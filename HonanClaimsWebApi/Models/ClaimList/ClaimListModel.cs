using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.ClaimList
{
   public class ClaimListModel
    {
        public string ClaimsId { get; set; }
        public string ClaimRefNo { get; set; }
        public string DateOfLoss { get; set; }
        public string Contact { get; set; }
        public string ClaimStage { get; set; }
        public string Status { get; set; }
        public string ClaimType { get; set; }
        public string ClaimTeam { get; set; }

        public string InsuredAccount { get; set; }
        public string Claimant { get; set; }
        public string Category { get; set; }
        public string NextAction { get; set; }
    }

    public class CustomerListModel
    {
        public List<CustomerModel> CustomerList { get; set; }
    }
}
