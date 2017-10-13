using HonanClaimsWebApi.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Claim
{
    public class ActivityTaskDetail
    {
        public string H_Activitytasksid_Dtl { get; set; }
        //[Required(ErrorMessage = "H_Claimsid_Dtl required")]
        public string H_Claimsid_Dtl { get; set; }
        [Required(ErrorMessage = "H_ClaimsRefid_Dtl required")]
        public string H_ClaimsRefid_Dtl { get; set; }
        public List<CRMPicklistItem> H_Claimsid_Dtl_List { get; set; }
        [Required(ErrorMessage = "Seq_Dtl required")]
        public int Seq_Dtl { get; set; }
        [Required(ErrorMessage = "Stage_Dtl required")]
        public string Stage_Dtl { get; set; }
        [Required(ErrorMessage = "Actionid_Dtl required")]
        public string Actionid_Dtl { get; set; }
        [Required(ErrorMessage = "Details_Dtl required")]
        public string Details_Dtl { get; set; }
        [Required(ErrorMessage = "Sla_Days_Dtl required")]
        public int Sla_Days_Dtl { get; set; }
        [Required(ErrorMessage = "Owner_Dtl Required.")]
        public string Owner_Dtl { get; set; }
        public string Owner_Contact_Dtl { get; set; }
        public string Owner_Contact_Name_Dtl { get; set; }
        public string Owner_Company_Dtl { get; set; }
        public string Owner_Company_Name_Dtl { get; set; }
        public string Phone_Dtl { get; set; }
        public string Email_Dtl { get; set; }
        public DateTime? Last_Task_Completed_Dtl { get; set; }
        public string Last_Task_Completed_Dtl_String { get; set; }
        public DateTime? This_Task_Due_Date_Dtl { get; set; }
        public bool Completed_Dtl { get; set; }
        public DateTime? Completed_Date_Dtl { get; set; }
        public bool Update_Key_Contacts_Dtl { get; set; }
        public bool Update_Key_Date_Dtl { get; set; }
        public string Completed_By_Dtl { get; set; }
        public string Completed_By_Name_Dtl { get; set; }
        public bool Manual_Due_Date_Dtl { get; set; }
        public bool High_Priority_Task_Dtl { get; set; }
        public int Next_Seq_Dtl { get; set; }
        public bool Skipped_Dtl { get; set; }
        public bool Imported_Dtl { get; set; }
        public bool IsNew { get; set; }

    }
}
