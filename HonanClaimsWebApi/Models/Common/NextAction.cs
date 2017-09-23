using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Common
{
    public class NextAction
    {
        public string NextStep { get; set; }
        public string DueDate { get; set; }
        public string Owner { get; set; }
    }
}
