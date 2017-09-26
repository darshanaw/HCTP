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
        public string Mainphone { get; set; }
        public string Fax { get; set; }
        public string Tollfree { get; set; }
        public string Webaddress { get; set; }
        public string Accountmanagerid { get; set; }
        public string Accountmanager { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string Client_Code { get; set; }
        public string H_Under_States { get; set; }
        public string Externalaccountno { get; set; }
        public string Notes { get; set; }
        public string H_Under_Abbrev { get; set; }
        public string H_Assoc_Prop_Hig { get; set; }
        public string H_Assoc_Benchmark { get; set; }
        public string PrefPropFormat { get; set; }
        public string H_Under_Renewals_Email { get; set; }
        public string H_Under_Nb_Email { get; set; }
        public string Aka { get; set; }
        public string H_Claim_Ref_Abbr { get; set; }
        public string H_Risksmart_Client_Group { get; set; }
        public string Billing_Method { get; set; }
        public string Billable_Rate { get; set; }
        public string Service_Rage { get; set; }
        public string Bsi_Incr_Pct { get; set; }
        public string Minimum_Cat_Pct { get; set; }
        public string Minimum_Lor_Pct { get; set; }
        public string Minimum_Ob_Amount { get; set; }
        public string Minimum_Liab_Amt { get; set; }
        public string Gross_Net_Preference { get; set; }
        public string Proposals_Email_Address { get; set; }
        public bool Proposal_Recommendation_Reqd { get; set; }
        public string Proposal_Type_Name { get; set; }
        public string Commission_Percentage { get; set; }
        public string Audit_Expenses { get; set; }
        public string Audit_Expenses_String { get; set; }
        public string Legal_Defence_Limit { get; set; }
        public string Legal_Defence_String { get; set; }
        public string W_H_And_S { get; set; }
        public string W_H_And_S_String { get; set; }
        public string Body_Corporate_Liability { get; set; }
    }

    public class AccountsModel
    {
        public string AccountName { get; set; }
        public List<CommonModel> AccountType { get; set; }
    }

}
