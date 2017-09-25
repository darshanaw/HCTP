using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.TimeslipCheck
{
    public class TimeslipDataModel
    {
        public string Order { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
    }

    public enum arealist
    {
        ClaimTeam,
        Account,
        Claim,
        ServiceBy
    }
}
