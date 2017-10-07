using HonanClaimsWebApi.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class Payment
    {
        [Required(ErrorMessage = "Ref no Required.")]
        public string ClaimRefNo_Payment { get; set; }
        public List<CRMPicklistItem> ClaimRefNo_Payment_List { get; set; }
        public string H_Paymentsid { get; set; }
        public string H_Claimsid { get; set; }
        [Required(ErrorMessage = "Date_Invoice_Received Required")]
        public DateTime? Date_Invoice_Received { get; set; }
        public string Invoice_Reference { get; set; }
        public string Invoice_Attached { get; set; }
        //[Required(ErrorMessage = "Payee_Account")]
        public string Payee_Account { get; set; }
        [Required(ErrorMessage = "Payee_Account_Name")]
        public string Payee_Account_Name { get; set; }
        //[Required(ErrorMessage = "Payee_Contact")]
        public string Payee_Contact { get; set; }
        [Required(ErrorMessage = "Payee_Contact_Name")]
        public string Payee_Contact_Name { get; set; }
        [Required(ErrorMessage = "Total_Gross")]
        public decimal? Total_Gross { get; set; }
        public decimal? Total_Net { get; set; }
        public bool Gst_Included { get; set; }
        public string Gst { get; set; }
        public string Payment_To { get; set; }
        public string Payment_To_Name { get; set; }
        public string Sent_To_Contact { get; set; }
        public string Sent_To_Contact_Name { get; set; }
        public DateTime? Date_Sent_For_Payment { get; set; }
        [Required(ErrorMessage = "Payment Status Required")]
        public string Payment_Status { get; set; }
        public List<PicklistItem> Payment_Status_List { get; set; }
        public string Date_Invoice_Paid { get; set; }
        public string Payment_Type { get; set; }
        public List<PicklistItem> Payment_Type_List { get; set; }
        public string Payment_Method { get; set; }
        public List<PicklistItem> Payment_Method_List { get; set; }
        public string Payment_Note { get; set; }
        [Required(ErrorMessage = "Reserve type required.")]
        public string Reserve_Type { get; set; }

        public List<PicklistItem> Reserve_Type_List
        {
            get
            {
                return new List<PicklistItem>() { new PicklistItem { Code = "Liability Reserve", Text = "Liability Reserve" }, new PicklistItem { Code = "Defence Reserve", Text = "Defence Reserve" } };
            }
        }

        public string Phone { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage ="Payee type required.")]
        public string Payee_Type { get; set; }
        public List<PicklistItem> Payee_Type_List { get; set; }
        public bool Is_Settlement { get; set; }
        public DateTime? Settlement_Invoice_Received { get; set; }
        public bool Imported { get; set; }
        [Required(ErrorMessage = "Payment_Amount Required")]
        public double? Payment_Amount { get; set; }
        [Required(ErrorMessage = "Payment_Date")]
        public DateTime? Payment_Date { get; set; }
        public bool IsNew { get; set; }
    }
}
