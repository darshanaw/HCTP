using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class DashboardObject
    {
        public string Category { get; set; }
        public List<DashboardBindData> Items { get; set; }
    }

    public class DashboardBindData
    {
        public string label { get; set; }
        public string color { get; set; }
        public string data { get; set; }
        public Dictionary<string, decimal> DataComplex { get; set; }

    }
}
