using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HonanClaimsWebApi.Services
{
    public class ClaimServices
    {
        public List<SelectListItem> GetClaimTeams()
        {
            List<SelectListItem> str = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Test1",Value="Test1"},
                new SelectListItem(){Text="Test2",Value="Test2"},
                new SelectListItem(){Text="Test3",Value="Test3"}
            };

            return str;
        }
    }
}
