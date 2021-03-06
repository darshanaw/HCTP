﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.TimeslipCheck
{
    public class BillingTimeRecordModel
    {

        public string H_Billingsid { get; set; }
        public string Claim_No { get; set; }
        public string sStart_Time { get; set; }
        //public string Start_Time_MM { get; set; }
        public string sEnd_Time { get; set; }
        //public string End_Time_MM { get; set; }
        public string Service_By { get; set; }
        public string Service_Date { get; set; }
        public bool Is_Billable { get; set; }
        public decimal Rate { get; set; }
        public int Qty_Mins { get; set; }
        public decimal Billable { get; set; }
        public string Work_Done { get; set; }
        public string H_Claimsid { get; set; }
        public string Service_By_Name { get; set; }

        public DateTime Start_Time { get; set; }
        public DateTime End_Time { get; set; }
        public int Units { get; set; }
        public Decimal Rate_Per_Unit { get; set; }
        public string H_Claimsid_Billing { get; set; }
    }
}
