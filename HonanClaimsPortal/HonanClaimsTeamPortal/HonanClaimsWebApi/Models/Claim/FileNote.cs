using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class FileNote
    {
        public string H_FileNotesId { get; set; }
        public string H_ClaimsId { get; set; }
        public string CreatedUserId { get; set; }
        public string CreatedUser { get; set; }
        public string CreatedDate { get; set; }
        public string ShortDescription { get; set; }
        public DateTime FileNoteDate { get; set; }
        public string Details { get; set; }
        public string ClaimRefNum { get; set; }
    }
}
