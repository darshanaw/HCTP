using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApiAccess1.Models.AdminLoginDetail
{
    public class AdminLoginsModel
    {
        public string H_PortalLoginId { get; set; }
        public string H_PortalLoginAdminId { get; set; }
        public string ActiveLogin { get; set; }
        public string ContactName { get; set; }
        public string ContactId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LastLogin { get; set; }
        public string Email { get; set; }
        public bool IsNew { get; set; }
    }
}
