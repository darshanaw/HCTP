using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class ActivityTask
    {
        public string ActivityTaskId { get; set; }
        public string ClaimRefNo_Act { get; set; }
        public string ClaimId_Act { get; set; }
        public string Customer_Act { get; set; }
        public int Seq_Act { get; set; }
        public string Stage_Act { get; set; }
        public string Action_Act { get; set; }
        public string Owner_Act { get; set; }
        public string Contact_Act { get; set; }
        public int SLADays_Act { get; set; }
        public DateTime? DueDate_Act { get; set; }
        public bool Complete_Act { get; set; }
        public string RowColor_Act { get; set; }
        public string ContactId_Act { get; set; }
        public bool Skipped_Act { get; set; }
        public DateTime? CompletedDate_Act { get; set; }
    }
}
