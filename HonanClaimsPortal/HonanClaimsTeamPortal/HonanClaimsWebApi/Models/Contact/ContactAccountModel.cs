using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models
{
    public class ContactAccountModel
    {
        public string Contact { get; set; }
        public string Account { get; set; }
        public string AccountId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Title { get; set; }
        public string ContactWorkPhone { get; set; }
        public string ContactMobile { get; set; }
        public string ContactFax { get; set; }
        public string AccountPhone { get; set; }
        public string AccountFax { get; set; }
        public string AccountType { get; set; }
        public string AccountManagerName { get; set; }
        public string AccountManagerId { get; set; }
        public List<PickListData> PickTitle { get; set; }
        public List<PickListData> PickTypes { get; set; }
    }

    public class Account
    {
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
    }

    public class PickListData
    {
        public string Order { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
    }
}
