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
    }
}
