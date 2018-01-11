using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Views
{
    public class ClaimNotificationSimple
    {
        public string ClaimId { get; set; }
        public string ClaimRefNo { get; set; }
        public string ClaimType { get; set; }
        public string CreatedBy { get; set; }
        public string Category { get; set; }
        public string ClaimStage { get; set; }
        public string Claimant { get; set; }
        public string NextAction { get; set; }
        public string NextActionOwner { get; set; }
        public string NextActionDueDate { get; set; }
        public string IncidentDate { get; set; }
        public string IncidentTime { get; set; }
        public string ClaimTeam { get; set; }
        public string PageName { get; set; }
        public string ReportedDate { get; set; }

    }
}
