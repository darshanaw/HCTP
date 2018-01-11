using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.LookupModel
{
    public class StoreSimple
    {
        public string AccountStoreId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
    }
}
