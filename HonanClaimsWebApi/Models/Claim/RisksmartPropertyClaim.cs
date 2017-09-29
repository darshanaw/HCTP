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
    public class RisksmartPropertyClaim
    {
        public string H_Claimsid { get; set; }
        public string Accountid { get; set; }
        [Required(ErrorMessage = "Account Name Required")]
        public string Account_Name { get; set; }
        public string Insurer_Ref { get; set; }
        public string Claim_Reference_Num { get; set; }
        //[Required(ErrorMessage = "Reported By Type Required")]
        public string Reported_By_Type { get; set; }
        //[Required(ErrorMessage = "Reported By Required")]
        public string Reported_By { get; set; }
        //[Required(ErrorMessage = "Region Required")]
        public string Region { get; set; }
        //[Required(ErrorMessage = "Required")]
        public string Storeid { get; set; }
        //[Required(ErrorMessage = "Store Name Required")]
        public string Storeid_Name { get; set; }
        public string Exact_Location { get; set; }
        //[Required(ErrorMessage = "Responsible Department Required")]
        public string Responsible_Department { get; set; }
        public string Witnesses { get; set; }
        public string Client_Ref_Num { get; set; }
        public string Claimant_Company { get; set; }
        //[Required(ErrorMessage = "Claimant First Name Required")]
        public string Claimant_First_Name { get; set; }
        //[Required(ErrorMessage = "Claimant Last Name Required")]
        public string Claimant_Last_Name { get; set; }
        public string Claimant_Address { get; set; }
        public string Date_Of_Birth { get; set; }
        public string Gender { get; set; }
        public string Claimant_Work_Phone { get; set; }
        public string Claimant_Home_Phone { get; set; }
        public string Claimant_Mobile_Phone { get; set; }
        public string Email_Address { get; set; }
        //[Required(ErrorMessage = "Incident Name Required")]
        public string Incident_Name { get; set; }
        //[Required(ErrorMessage = "Incident Address Required")]
        public string Incident_Address { get; set; }
        //[Required(ErrorMessage = "Incident Category Required")]
        public string Incident_Category { get; set; }
        public string Incident_Type { get; set; }
        //[Required(ErrorMessage = "Incident Summary Required")]
        public string Incident_Summary { get; set; }
        public string Injury_Summary { get; set; }
        public string Juristiction { get; set; }
        public string Risksmart_Gcc_Ref { get; set; }
        public string Bodily_Location { get; set; }
        public string Treatment_Given { get; set; }
        public string Incident_Party_Type { get; set; }
        public string Cctv_Available { get; set; }
        public string Cctv_Viewed { get; set; }
        public string Cctv_Copied { get; set; }
        public string Cctv_Observations { get; set; }
        public string Insurer { get; set; }
        public string InsurerName { get; set; }
        public string Insurer_Conduct { get; set; }
        public string Insured_Name { get; set; }
        public string Property_Claim_Ref { get; set; }
        [Required(ErrorMessage = "Policy No Required")]
        public string Policy_No { get; set; }
        public string Policy_Name { get; set; }
        public string Policy_Type { get; set; }
        public string Period_From { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Excess { get; set; }
        public string Binder { get; set; }
        public bool? Litigated { get; set; }
        public string Cat_Code { get; set; }
        public string Idr { get; set; }
        public string Complexity { get; set; }
        public string Claim_Detail { get; set; }
        public string Account_Manager_Property { get; set; }
        public bool? Claim_Lodged { get; set; }
        public bool? Claim_Acknowledged { get; set; }
        public bool? Assessor_Appointed { get; set; }
        public bool? Claim_Approved { get; set; }
        public bool? Claim_Declined { get; set; }
        public string Claim_Status { get; set; }
        public List<PicklistItem> Claim_Status_List { get; set; }
        public string BillingMethod { get; set; }
        public string Client_Group { get; set; }
        public string Broker_Company { get; set; }
        public string Property_Address_1 { get; set; }
        public string Property_Address_2 { get; set; }
        public string Property_Suburb { get; set; }
        public string Property_State { get; set; }
        public string Property_Postalcode { get; set; }
        public string Are_You_Gst_Registered { get; set; }
        public string Abn_Num { get; set; }
        public string Itc_Pct { get; set; }
        public string Payment_Account_Name { get; set; }
        public string Payment_Bsb { get; set; }
        public string Payment_Account_Number { get; set; }
        public string Loss_Details { get; set; }
        public string Policy_Report_Num { get; set; }
        public string Details_Of_Responsible_3Rd_Pty { get; set; }
        public string Assigned_User_Name { get; set; }
        public string Assigned_User { get; set; }
        public List<SelectListItem> Assigned_User_List { get; set; }
        public string Claim_Stage { get; set; }
        public decimal Liability_Reserve { get; set; }
        public decimal Defence_Reserve { get; set; }
        public decimal Total_Reserve { get; set; }
        public decimal Gross_Paid_To_Date { get; set; }
        public decimal Net_Paid_To_Date { get; set; }
        public decimal Current_Reserve { get; set; }
        public decimal Current_Exposure { get; set; }
        public string Acknowledge_Claim_By { get; set; }
        public string Item_To_Claim_1 { get; set; }
        public string Item_To_Claim_2 { get; set; }
        public string Item_To_Claim_3 { get; set; }
        public string Item_To_Claim_4 { get; set; }
        public string Item_To_Claim_5 { get; set; }
        public string Item_To_Claim_6 { get; set; }
        public decimal Amount_For_Item_1 { get; set; }
        public decimal Amount_For_Item_2 { get; set; }
        public decimal Amount_For_Item_3 { get; set; }
        public decimal Amount_For_Item_4 { get; set; }
        public decimal Amount_For_Item_5 { get; set; }
        public decimal Amount_For_Item_6 { get; set; }
        public string Quote_Attached_1 { get; set; }
        public string Quote_Attached_2 { get; set; }
        public string Quote_Attached_3 { get; set; }
        public string Quote_Attached_4 { get; set; }
        public string Quote_Attached_5 { get; set; }
        public string Quote_Attached_6 { get; set; }
        public string Causation { get; set; }
        public string Claim_Note { get; set; }
        public bool? Hide_Banner { get; set; }
        public string Insurer_Name { get; set; }
        public string Policy_No_Name { get; set; }
        public string Broker_Company_Name { get; set; }

        [Required(ErrorMessage = "Reported Date Required")]
        public DateTime? Reported_Date { get; set; }
        public DateTime? Reported_Time { get; set; }

        [Required(ErrorMessage = "Reported Time Required")]
        public string Reported_TimeH { get; set; }

        [Required(ErrorMessage = "Reported Time Required")]
        public string Reported_TimeM { get; set; }
        //[Required(ErrorMessage = "Incident Date Required")]
        public DateTime? Incident_Date { get; set; }
        public string Period_To { get; set; }
        public DateTime? Account_Period_From { get; set; }
        public DateTime? Account_Period_To { get; set; }
        public DateTime? Assessor_Pointed_Name { get; set; }
        public DateTime? Approvedecline_Date { get; set; }
        public DateTime? Date_Of_Loss { get; set; }
        public DateTime? Assigned_Date { get; set; }
        public DateTime? Acknowledge_Claim_On { get; set; }
        public string Claim_Team { get; set; }
        public string Claim_Team_Name { get; set; }
        public string Claim_Type { get; set; }
        public DateTime? Claim_Acknowledged_Date { get; set; }
        public DateTime? Claim_Lodged_Date { get; set; }
        public string Policy_Section { get; set; }

        public decimal Net_Paid_Liability { get; set; }
        public decimal Net_Paid_Defence { get; set; }
        public decimal Total_Incurred { get; set; }
        public decimal Liability_Res_Source { get; set; }
        public decimal Defence_Res_Source { get; set; }
        public decimal Deductible { get; set; }
        public decimal Net_Incurred_On_Deductible { get; set; }
        public decimal Balance_On_Deduct_Remaining { get; set; }
        public bool? Claim_Received { get; set; }
        public bool? Partial_Indemnity_Granted { get; set; }
        public DateTime? Claim_Received_Date { get; set; }
        public bool? Claim_Closed { get; set; }
        public DateTime? Claim_Closed_Date { get; set; }
        public bool? Review { get; set; }
        public DateTime? Review_Date { get; set; }
        public bool? Outcome_Settlement { get; set; }
        public bool? Outcome_Declined { get; set; }
        public DateTime? Outcome_Date { get; set; }
        public string Outcome { get; set; }
        public string Oc_Claims_Contact { get; set; }
        public bool? Claim_Not_Lodged { get; set; }
        public DateTime? Claim_Not_Lodged_Date { get; set; }
        public bool? Expert_Appointed { get; set; }
        public DateTime? Claim_ClosedExpert_Appointed_Date { get; set; }
        public bool? Indemnity_Granted { get; set; }
        public string Broker { get; set; }
        public string Broker_Name { get; set; }
        public string Broker_Name_Id { get; set; }
        public string Panel_Lawyers_Retained { get; set; }
        public string Broker_Account { get; set; }
        public string Broker_Account_Id { get; set; }
        public string Oc_Num { get; set; }
        public string Oc_Id { get; set; }
        public string Reported_By_Contact_Phone_Num { get; set; }
        public DateTime? Incident_Time { get; set; }
        [Required(ErrorMessage = "Incident Time Required")]
        public string Incident_TimeH { get; set; }
        [Required(ErrorMessage = "Incident Time Required")]
        public string Incident_TimeM { get; set; }
        public string Policy_Status { get; set; }
        public string Policy_Class { get; set; }
        public decimal Basic_Excess { get; set; }
        public DateTime? AcctPeriod1 { get; set; }//New field
        public DateTime? AcctPeriod2 { get; set; }//New field
        public List<PicklistItem> Policy_Class_List { get; set; }
        public List<PicklistItem> Causation_List { get; set; }
        public List<PicklistItem> Client_Group_List { get; set; }
        public List<PicklistItem> Policy_Section_List { get; set; }
        public List<PicklistItem> ReportedByTypeList { get; set; }
        public List<PicklistItem> IncidentPartyTypeList { get; set; }
        public List<PicklistItem> RegionList { get; set; }
        public List<PicklistItem> IncidentCategoryList { get; set; }
        public List<string> JuristictionList { get; set; }
        public List<string> GenderList { get; set; }
        public List<string> CCTVAvailableList { get; set; }
        public List<string> CCTVViewedList { get; set; }
        public List<string> YesNoList { get; set; }
        public List<PicklistItem> PropertySuburbList { get; set; }
        public NextAction NextAction { get; set; }
    }
}
