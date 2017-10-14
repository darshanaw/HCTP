using HonanClaimsWebApi.Models.ClaimList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApiAccess1.LoginServices
{
    public class ClaimTeamLoginModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Teams { get; set; }
        public bool IsPortalAdmin { get; set; }
        public bool IsManager { get; set; }
        public bool IsTimeslipChecker { get; set; }
    }
}
