using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HonanClaimsWebApi.Models.SendEmail
{
    public class EmailSignature
    {
        public string UserId { get; set; }
        [AllowHtml]
        public string Signature { get; set; }
        public bool ApplyOnNewEmails { get; set; }
        public bool ApplyOnReplies { get; set; }
        public bool ApplyOnForwards { get; set; }
        public string EmailSignatureId { get; set; }
    }
}
