using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class ClaimAttachmentSimple
    {
        public string KeyId { get; set; }
        public string ClaimId { get; set; }
        public string AttachmentType { get; set; }      //identify if it is CustomerDoc or Attachment
        public string AttachmentName { get; set; }      //true file name
        public string AttachmentDescription { get; set; }      //display name in SLX
        public DateTime LastUpdated { get; set; }
        public UInt64 Size { get; set; }
        public string UserId { get; set; }
        public string IsCustomerDoc { get; set; }

    }
}
