using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Services
{
    public class ServiceHelper
    {
        public List<string> FilterTeamList(List<string> teamnames)
        {

            List<string> newlist = new List<string>();
            if (teamnames.Count > 0)
            {
                if (teamnames.Contains("GCC Claims Manager"))
                {
                    newlist.Add("GCC Claims Manager");
                }
                if (teamnames.Contains("Property Claims Manager"))
                {
                    newlist.Add("Property Claims Manager");
                }
                if (teamnames.Contains("Risksmart GCC Manager"))
                {
                    newlist.Add("Risksmart GCC Manager");
                }
                if (teamnames.Contains("Risksmart Property Manager"))
                {
                    newlist.Add("Risksmart Property Manager");
                }
            }
            return newlist;
        }
    }
}
