using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HonanClaimsWebApi.Models.SendEmail
{
    public class PaymentEmailModel
    {
        public string ToEmail { get; set; }
        public string BccEmail { get; set; }
        [AllowHtml]
        public string EmailBody { get; set; }
        public string Subject { get; set; }
        public bool IncludeAttachment { get; set; }
        public string PaymentId { get; set; }
    }
}
