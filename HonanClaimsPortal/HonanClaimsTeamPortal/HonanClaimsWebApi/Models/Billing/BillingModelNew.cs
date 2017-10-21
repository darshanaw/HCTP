using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Billing
{
    public class BillingModelNew
    {
        public string H_Billingsid_New { get; set; }
        public string H_Claimsid_Billing_New { get; set; }
        public string Service_By_New { get; set; }
        public bool Is_Billable_New { get; set; }
        [Required(ErrorMessage = "ClaimNo Required")]
        public string Claim_No_New { get; set; }
        public string ActivityId_New { get; set; }
        public string Activity_Name_New { get; set; }//added
        public string Policyid_Billing_New { get; set; }
        public string Policy_Name_Billing_New { get; set; }//added
        public string ClientId_New { get; set; }
        public string Client_Name_New { get; set; }//added
        [Required(ErrorMessage = "Service Date Required")]
        public DateTime Service_Date_New { get; set; }
        [Required(ErrorMessage = "Start Time Required")]
        public string Start_Time_HH_New { get; set; }
        [Required(ErrorMessage = "Start Time Required")]
        public string Start_Time_MM_New { get; set; }
        [Required(ErrorMessage = "End Time Required")]
        public string End_Time_HH_New { get; set; }
        [Required(ErrorMessage = "End Time Required")]
        public string End_Time_MM_New { get; set; }
        public int Qty_Mins_New { get; set; }
        public decimal Rate_New { get; set; }
        [Required(ErrorMessage = "Billable Required")]
        public decimal Billable_New { get; set; }
        public bool Timeslip_Checked_New { get; set; }
        public string Checked_By_New { get; set; }
        public DateTime? Checked_Date_New { get; set; }
        public bool Invoice_Processed_New { get; set; }//change type to bool
        public string Invoice_Processed_By_New { get; set; }
        public DateTime? Invoice_Processed_Date_New { get; set; }
        public string Invoice_No_New { get; set; }
        public string Invoice_Date_New { get; set; }
        public decimal Rate_Per_Unit_New { get; set; }
        [Required(ErrorMessage = "Units Done Required")]
        public int Units_New { get; set; }
        [Required(ErrorMessage = "Work Done Required")]
        public string Work_Done_New { get; set; }
        [Required(ErrorMessage = "Service By Required")]
        public string Service_By_Name_New { get; set; }
        public string Quarter_New { get; set; }

        public string Work_Done_Short_New { get; set; }
        public bool IsNew_Billable_New { get; set; }
        public string PageType_New { get; set; }
        public DateTime Start_Time_New { get; set; }
        public DateTime End_Time_New { get; set; }

        [Required(ErrorMessage = "Start Time Required")]
        public string sStart_Time_New { get; set; }
        [Required(ErrorMessage = "End Time Required")]
        public string sEnd_Time_New { get; set; }

        public string Checked_By_Name_New { get; set; }
        public string Invoice_Processed_By_Name_New { get; set; }
    }
}
