using HonanClaimsWebApi.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.AccountList
{
    public class AccountListModel
    {
        public string AccountId { get; set; }
        public string Account { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
    }

    public class AccountModel
    {
        public string AccountName { get; set; }
        public List<CommonModel> AccountType { get; set; }
    }

}
