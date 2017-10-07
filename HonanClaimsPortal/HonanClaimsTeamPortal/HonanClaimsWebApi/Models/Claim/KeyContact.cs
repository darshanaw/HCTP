using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class KeyContact
    {
        public string H_Keycontactsid { get; set; }
        public string H_Claimsid { get; set; }
        public string Description { get; set; }
        public string Accountid { get; set; }
        public string Email_Address { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Contactid { get; set; }
        public string Contact_Name { get; set; }
        public string Account_Name { get; set; }
        public string Imported { get; set; }
    }
}
