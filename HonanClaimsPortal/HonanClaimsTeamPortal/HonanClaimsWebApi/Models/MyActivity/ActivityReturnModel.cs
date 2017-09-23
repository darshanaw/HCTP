using HonanClaimsWebApi.Models.ClaimList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.MyActivity
{
    public class ActivityReturnModel
    {
        public List<CustomerModel> CustomerList { get; set; }
        public List<ActivityOwnerModel> OwnerList { get; set; }

        public List<ActivityClaimModel> ActivityClaim { get; set; }

        public List<ActivityUserModel> Activityusers { get; set; }



    }
}
