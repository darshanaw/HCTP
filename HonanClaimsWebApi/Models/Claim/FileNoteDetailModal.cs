using HonanClaimsWebApi.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class FileNoteDetailModal
    {
        [Required]
        public string ClaimRefNo_Fn { get; set; }
        [Required]
        public string CreatedBy_Fn { get; set; }
        [Required]
        public string CreatedBy_Id_Fn { get; set; }
        public string ClaimId_Fn { get; set; }
        [Required]
        public DateTime? FileNoteDate_Fn { get; set; }
        [Required]
        public DateTime? CreatedDate_Fn { get; set; }
        [Required]
        public string ShortDescription_Fn { get; set; }
        [Required]
        public string Detail_Fn { get; set; }

        public List<CRMPicklistItem> RefnuberList_Fn { get; set; }

    }
}
