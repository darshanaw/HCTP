using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.TimeslipCheck
{
    public class TimeslipReturnModel
    {
        public List<TimeslipDataModel> ClaimTeam { get; set; }
        public List<TimeslipDataModel> Account { get; set; }
        public List<TimeslipDataModel> Claim { get; set; }
        public List<TimeslipDataModel> ServiceBy { get; set; }
    }
}
