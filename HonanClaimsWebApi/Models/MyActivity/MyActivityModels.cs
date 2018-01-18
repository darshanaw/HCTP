using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.MyActivity
{
    public class MyActivityModels
    {
        public string ActivityTaskId { get; set; }
        public string ClaimRefNo { get; set; }
        public string ClaimId { get; set; }
        public string Customer { get; set; }
        public string Stage { get; set; }
        public string Action { get; set; }
        public string Owner { get; set; }
        public string Contact { get; set; }
        public string RowColor { get; set; }
        public int Seq { get; set; }
        public int SLADays { get; set; }
        public bool Complete { get; set; }
        public bool Skipped { get; set; }
        public string DueDate { get; set; }
        public string CompletedDate { get; set; }
        public string ClaimTeam { get; set; }
    }
}
