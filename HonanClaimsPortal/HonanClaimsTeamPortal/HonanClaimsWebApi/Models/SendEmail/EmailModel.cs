using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.SendEmail
{
    public class EmailModel
    {
        public List<string> ToEmails { get; set; }
        public List<string> CcEmails { get; set; }
        public List<string> ClaimIds { get; set; }
        public string emailBody { get; set; }
        public string subject { get; set; }
    }
}
