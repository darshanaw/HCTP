﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Billing
{
    public class BillingModel
    {
        public string H_Billingsid { get; set; }
        public string H_Claimsid { get; set; }
        public string Service_By { get; set; }
        public string Is_Billable { get; set; }
        public string Claim_No { get; set; }
        public string ActivityId { get; set; }
        public string Activity_Name { get; set; }//
        public string Policyid { get; set; }
        public string Policy_Name { get; set; }//
        public string ClientId { get; set; }
        public string Client_Name { get; set; }//
        public DateTime Service_Date { get; set; }
        public string Start_Time_HH { get; set; }
        public string Start_Time_MM { get; set; }
        public string End_Time_HH { get; set; }
        public string End_Time_MM { get; set; }
        public int Qty_Mins { get; set; }
        public decimal Rate { get; set; }
        public bool Billable { get; set; }
        public bool Timeslip_Checked { get; set; }
        public string Checked_By { get; set; }
        public DateTime? Checked_Date { get; set; }
        public string Invoice_Processed { get; set; }
        public string Invoice_Processed_By { get; set; }
        public string Invoice_Processed_Date { get; set; }
        public string Invoice_No { get; set; }
        public string Invoice_Date { get; set; }
        public decimal Rate_Per_Unit { get; set; }
        public int Units { get; set; }
        public string Work_Done { get; set; }
        public string Service_By_Name { get; set; }
        public string Quarter { get; set; }
        public string Work_Done_Short { get; set; }
    }
}
