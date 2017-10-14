using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HonanClaimsWebApi.Models.Search
{
    public class SearchModel
    {
        public string CommonSearchProperty { get; set; }
        public List<SelectListItem> ClaimPropertyList { get; set; }
        public string CommonSearchText { get; set; }
    }
}
