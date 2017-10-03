using HonanClaimsWebApi.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models.Policies
{
    public class PolicyModel
    {
            public string Policy_Number { get; set; }
            public string Client_Name { get; set; }
            public string Client_Code { get; set; }
            public string Current_Phase { get; set; }
            public bool Mark_As_Duplicate { get; set; }
            public string Policy_Class { get; set; }
            public string Associate { get; set; }
            public string Underwriter { get; set; }
            public string Account_Manager_Code { get; set; }
            public DateTime Inception_Date { get; set; }
            public DateTime Inception_Date_Invoice { get; set; }
            public DateTime Expiry_Date { get; set; }
            public DateTime Expiry_Date_Invoice { get; set; }
            public string Renewal_Months { get; set; }
            public string Days_To_Expiry { get; set; }
            public string Base_Premium { get; set; }
            public string Base_Gst { get; set; }
            public string Fire_Services_Levy { get; set; }
            public string Fire_Services_Levy_Gst { get; set; }
            public string Stamp_Duty { get; set; }
            public string Underwriter_Levy { get; set; }
            public string Underwriter_Levy_Gst { get; set; }
            public string Total_Premium { get; set; }
            public string Total_Premium_Gst { get; set; }
            public string Policy_Fee_Premium { get; set; }
            public string Policy_Fee_Premium_Gst { get; set; }
            public string Commission_Premium { get; set; }
            public string Commission_Premium_Gst { get; set; }
            public string Associate_Premium { get; set; }
            public string Associate_Premium_Gst { get; set; }
            public string Policy_Income { get; set; }
            public string Policy_Income_Gst { get; set; }
            public string Total_Total_Prem { get; set; }
            public string Commercial_Activity { get; set; }
            public string Commercial_Percent { get; set; }
            public string Claims_Last_5Y_Yesnotba { get; set; }
            public string Claims_Last_5Y_Number { get; set; }
            public string Claims_Last_5Y_Settled { get; set; }
            public string Situation_Street_Number { get; set; }
            public string Situation_Street { get; set; }
            public string Situation_Suburb { get; set; }
            public string Situation_State { get; set; }
            public string Situation_Postcode { get; set; }
            public string Situation_Address { get; set; }
            public string Invoiced_By { get; set; }
            public string Invoiced_On { get; set; }
            //INVOICED_UND_RESPID.
            public string Building_Sum_Insured { get; set; }
            public bool Contents_Included { get; set; }
            public string Contents_Sum_Insured { get; set; }
            public string Liability_Sum_Insured { get; set; }
            public string Lor { get; set; }
            public string Cat_Percent { get; set; }
            public string Office_Bearers { get; set; }
            public string Fidelity { get; set; }
            public string Machinery { get; set; }
            public string Vol_Works { get; set; }
            public string Basic_Excess { get; set; }
            public string Special_Excess { get; set; }
            public string Year_Built { get; set; }
            public string Lifts { get; set; }
            public string Units { get; set; }
            public string Levels { get; set; }
            public string Pools { get; set; }
            public string Spas { get; set; }
            public string Tennis { get; set; }
            public string Gyms { get; set; }
            public string Jettys { get; set; }
            public string Occupied { get; set; }
            public string Asbestos { get; set; }
            public string Eps { get; set; }
            public string Defects { get; set; }
            public string Rewired { get; set; }
            public string Replumbed { get; set; }
            public string Heritage { get; set; }
            public string Holiday_Letting { get; set; }
            public string Holiday_Letting_Percentage { get; set; }
            public string Onsite_Manager { get; set; }
            public string Construction_Summary { get; set; }
            public string Construction_Walls { get; set; }
            public string Construction_Floor { get; set; }
            public string Construction_Roof { get; set; }
            public bool Risk_Amounts_Checked { get; set; }
            public bool Risk_Amounts_Issue { get; set; }
            public DateTime Risk_Amounts_Checked_On { get; set; }
            public string Risk_Amounts_Checked_By { get; set; }
            public bool Risk_Details_Checked { get; set; }
            public bool Risk_Details_Issue { get; set; }
            public DateTime Risk_Details_Checked_On { get; set; }
            public string Risk_Details_Checked_By { get; set; }
            public bool Construction_Checked { get; set; }
            public bool Construction_Issue { get; set; }
            public DateTime Construction_Checked_On { get; set; }
            public string Construction_Checked_By { get; set; }
            public bool Other_Details_Checked { get; set; }
            public bool Other_Issue { get; set; }
            public DateTime Other_Details_Checked_On { get; set; }
            public string Other_Details_Checked_By { get; set; }
            public bool Superceded { get; set; }
            public DateTime Superceded_On { get; set; }
            public string Superceded_By { get; set; }
            public string Issue_Description { get; set; }
            public string Superseded_Comment { get; set; }
            public string Floating_Floor_Cover_Required { get; set; }
            public string Flood_Cover_Yesnotba { get; set; }
            public string Flood_Cover_Amount { get; set; }
        }

    public class PolictSimple
    {
        public string PolicyId { get; set; }
        public string PolicyNo { get; set; }
        public string Customer { get; set; }
        public string CustomerId { get; set; }
        public DateTime InceptionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Associate { get; set; }
        public string AssociateId { get; set; }
        public string Underwriter { get; set; }
        public string UnderwriterId { get; set; }
        public string PolicyStatus { get; set; }
        public string PolicyClass { get; set; }
        public string AccountId { get; set; }
    }

    public class PolicyReturnModel
    {
        public bool IsActive { get; set; }
        public string PolicyNumber { get; set; }
        public string   Associate { get; set; }
        public string Customer { get; set; }
        public string Underwriter { get; set; }
        public List<CommonModel> PolicyType { get; set; }
        public List<CommonModel> PolicyClass { get; set; }

    }
}
