using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Billing
{
    public class BillingModel
    {
        public string H_Billingsid { get; set; }
        public string H_Claimsid_Billing { get; set; }
        public string Service_By { get; set; }
        public bool Is_Billable { get; set; }
        public string Claim_No { get; set; }
        public string ActivityId { get; set; }
        public string Activity_Name { get; set; }//added
        public string Policyid_Billing { get; set; }
        public string Policy_Name_Billing { get; set; }//added
        public string ClientId { get; set; }
        public string Client_Name { get; set; }//added
        [Required(ErrorMessage = "Service Date Required")]
        public DateTime Service_Date { get; set; }
        [Required(ErrorMessage = "Start Time Required")]
        public string Start_Time_HH { get; set; }
        [Required(ErrorMessage = "Start Time Required")]
        public string Start_Time_MM { get; set; }
        [Required(ErrorMessage = "End Time Required")]
        public string End_Time_HH { get; set; }
        [Required(ErrorMessage = "End Time Required")]
        public string End_Time_MM { get; set; }
        public int Qty_Mins { get; set; }
        [Required(ErrorMessage = "Rate Required")]
        public decimal Rate { get; set; }
        [Required(ErrorMessage = "Billable Required")]
        public decimal Billable { get; set; }
        public bool Timeslip_Checked { get; set; }
        public string Checked_By { get; set; }
        public DateTime? Checked_Date { get; set; }
        public bool Invoice_Processed { get; set; }//change type to bool
        public string Invoice_Processed_By { get; set; }
        public DateTime? Invoice_Processed_Date { get; set; }
        public string Invoice_No { get; set; }
        public string Invoice_Date { get; set; }
        //[Required(ErrorMessage = "Rate Per Unit Required")]
        public decimal Rate_Per_Unit { get; set; }
        [Required(ErrorMessage = "Units Required")]
        public int Units { get; set; }
        [Required(ErrorMessage = "Work Done Required")]
        public string Work_Done { get; set; }
        [Required(ErrorMessage = "Service By Required")]
        public string Service_By_Name { get; set; }
        public string Quarter { get; set; }
        
        public string Work_Done_Short { get; set; }
        public bool IsNew_Billable { get; set; }
        public string PageType { get; set; }
        public DateTime Start_Time { get; set; }
        public DateTime End_Time { get; set; }

        [Required(ErrorMessage = "Start Time Required")]
        public string sStart_Time { get; set; }
        [Required(ErrorMessage = "End Time Required")]
        public string sEnd_Time { get; set; }

        public string Checked_By_Name { get; set; }
        public string Invoice_Processed_By_Name { get; set; }
    }
}
