using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApiAccess1.Models.TeamGetPortalRegistration
{
    public class TeamGetPortalRegistrationModel
    {
        public string keyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string RegistrationDate { get; set; }
        //public string Account_Id { get; set; }
        //public string H_Customerportaladminid { get; set; }
        public List<MatchingContactsModel> MatchingContacts { get; set; }
    }

    public class MatchingContactsModel
    {
        public string ContactId { get; set; }
        public string ContactName { get; set; }
        public string AccountName { get; set; }
        public string AccountId { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string H_PortalLoginId { get; set; }
    }
}
