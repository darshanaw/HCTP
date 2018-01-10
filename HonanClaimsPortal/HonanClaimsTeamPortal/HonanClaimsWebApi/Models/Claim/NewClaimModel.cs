using HonanClaimsWebApi.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HonanClaimsWebApi.Models.Claim
{
    public class NewClaimModel
    {
        [Required(ErrorMessage ="Claim Team Required")]
        public string Claim_Team { get; set; }
        public List<SelectListItem> Claim_Team_List { get; set; }
        [Required(ErrorMessage = "Account Name Required")]
        public string Account_Name { get; set; }
        public string Accountid { get; set; }
        [Required(ErrorMessage = "Claim Type Required")]
        public string Claim_Type { get; set; }
        public List<SelectListItem> Claim_Type_List { get; set; }
        public string Oc_No { get; set; }
        public string Oc_Name { get; set; }
        [Required(ErrorMessage = "Policy No Required")]
        public string Policy_No { get; set; }
        public string Policy_Id { get; set; }
        public string Policy_Class { get; set; }
        public string Assigned_To { get; set; }
        public string Assigned_To_Id { get; set; }
        public List<CRMPicklistItem> Assigned_To_List { get; set; }
        public string Property_Address_1 { get; set; }
        public string Property_Address_2 { get; set; }
        public string Property_Suburb { get; set; }
        public string Property_State { get; set; }
        public string Property_Postalcode { get; set; }
        public string AccountManager { get; set; }
        public string InsurerName { get; set; }
        public string Insurer { get; set; }
        public string Insured_Name { get; set; }
        public string Account_Manager_Property { get; set; }
        public List<PicklistItem> Policy_Classes { get; set; }
        public DateTime? Incident_Date { get; set; }

    }
}
