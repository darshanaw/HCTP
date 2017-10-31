using HonanClaimsWebApi.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Contact
{
    public class ContactModel
    {
        public string ContactId { get; set; }
        public string ContactName { get; set; }//
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountId { get; set; }
        public string Account { get; set; }
        public string WorkPhone { get; set; }//
        public string Mobile { get; set; }//
        public string Email { get; set; }//
        public string Title { get; set; }
        public string Accountmanager { get; set; }
        //public string Address { get; set; }
        public string Address1 { get; set; }//
        public string Address2 { get; set; }//
        public string Suburb { get; set; }//
        public string State { get; set; }//
        public string PostalCode { get; set; }//

        public string Workphone { get; set; }//
        public string Fax { get; set; }//
        public string Homephone { get; set; }//
        public string Otherphone { get; set; }
        public string Preferred_Contact { get; set; }
        public string Webaddress { get; set; }//
        //public string Type { get; set; }
        public string Status { get; set; }
        public string AccountType { get; set; }
        public string AccountStatus { get; set; }
        public bool PrimaryContactForAccount { get; set; }
        public bool AuthorizedServiceContact { get; set; }
        //public bool DoNotSolicit { get; set; }
        //public bool DoNotEmail { get; set; }
        //public bool DoNotCall { get; set; }
        //public bool DoNotMail { get; set; }
        //public bool DoNotFax { get; set; }

        public List<PicklistItem> PropertySuburbList { get; set; }
        public List<PicklistItem> PropertyStateList { get; set; }
    }

    public class ContactListModel
    {
        public string ConatctName { get; set; }
        public string AccountName { get; set; }
    }

    public class ContactUpdateModel
    {
        public string ContactId { get; set; }
        public string ContactName { get; set; }
        public string Title { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string WorkPhone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Workphone { get; set; }
        public string Fax { get; set; }
        public string Homephone { get; set; }
        public string Webaddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
