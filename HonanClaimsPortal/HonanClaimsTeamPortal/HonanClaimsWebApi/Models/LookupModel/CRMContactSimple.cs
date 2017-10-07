using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.LookupModel
{
    public class CRMContactSimple
    {
        public string ContactId { get; set; }
        public string ContactName { get; set; }
        public string AccountName { get; set; }
        public string AccountId { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
