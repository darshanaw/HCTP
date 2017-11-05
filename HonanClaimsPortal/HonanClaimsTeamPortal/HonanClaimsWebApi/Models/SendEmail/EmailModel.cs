using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HonanClaimsWebApi.Models.SendEmail
{
    public class EmailModel
    {
        public List<string> ToEmails { get; set; }
        public List<string> CcEmails { get; set; }
        public List<string> ClaimIds { get; set; }
        [AllowHtml]
        public string emailBody { get; set; }
        [AllowHtml]
        public string subject { get; set; }
        public string isFileNote { get; set; }
    }
}
