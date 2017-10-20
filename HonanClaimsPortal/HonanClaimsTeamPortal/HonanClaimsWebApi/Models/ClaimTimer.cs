using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models
{
    public class ClaimTimer
    {
        public string UserId { get; set; }
        public string ClaimId { get; set; }
        public string ClaimRefNo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsTimerActive { get; set; }
        public string ClaimTimerId { get; set; }
    }
}
