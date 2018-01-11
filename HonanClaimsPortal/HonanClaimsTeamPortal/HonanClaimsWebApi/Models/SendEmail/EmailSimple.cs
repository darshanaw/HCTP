using HonanClaimsWebApi.Models.Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HonanClaimsWebApi.Models.SendEmail
{
    public class EmailSimple
    {
        public string H_ClaimEmailsId { get; set; }
        public string CreateDate { get; set; }
        public string FromEmail { get; set; }
        public string EmailBody { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string ClaimNo { get; set; }
        public string CC { get; set; }
        public string ClaimId { get; set; }
        public List<HttpPostedFile> Files { get; set; }
        public List<ClaimAttachmentSimple> FilesSimple { get; set; }
    }
}
