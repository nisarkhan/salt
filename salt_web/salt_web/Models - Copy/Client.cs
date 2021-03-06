//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace salt_web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client
    {
        public int CustomerID { get; set; }
        public System.Guid ClientPKID { get; set; }
        public string ClientCode { get; set; }
        public string CompanyLegalName { get; set; }
        public string DBA { get; set; }
        public string MrMs { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactTitle { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Notes { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string TaxID { get; set; }
        public bool MultiTaxID { get; set; }
        public bool BillingAddressSame { get; set; }
        public bool BillingContactSame { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string ZipCode { get; set; }
        public string BillingContactTitle { get; set; }
        public string BillingInfo_PhoneNumber { get; set; }
        public string BillingInfo_Extension { get; set; }
        public string BillingInfo_FaxNumber { get; set; }
        public string BillingInfo_EmailAddress { get; set; }
        public string BillingInfo_Notes { get; set; }
        public string BillingInfo_ContactFirstName { get; set; }
        public string BillingInfo_ContactLastName { get; set; }
        public string BillingInfo_MrMrs { get; set; }
        public string RecordsAddress { get; set; }
        public string RecordsCity { get; set; }
        public string RecordsState { get; set; }
        public string RecordsZip { get; set; }
        public bool RecordsAddressSame { get; set; }
        public string TypeOfContract { get; set; }
        public string Contracts_City { get; set; }
        public string Contracts_State { get; set; }
        public string SalesPerson { get; set; }
        public Nullable<System.Guid> PrimarySalesStaff { get; set; }
        public Nullable<System.Guid> SecondarySalesStaff { get; set; }
        public string BusinessDevRep { get; set; }
        public string PrimaryConsultant { get; set; }
        public string SecondaryConsultant { get; set; }
        public string GMRevenue { get; set; }
        public string FeeTerms { get; set; }
        public string ContractDateSigned { get; set; }
        public string ContractSignedBy { get; set; }
        public string ContractTitle { get; set; }
        public Nullable<System.DateTime> TentativeAuditDate { get; set; }
        public string Auditor { get; set; }
        public Nullable<System.Guid> Auditors_Key { get; set; }
        public string OldAuditors_Key { get; set; }
        public string Third3rdAuditor { get; set; }
        public int JKHStatus { get; set; }
        public Nullable<System.DateTime> ScheduledAuditDate { get; set; }
        public string Confirmed { get; set; }
        public string PPMT { get; set; }
        public string FuturesRate { get; set; }
        public string FuturesInstallments { get; set; }
        public bool CancelationFee { get; set; }
        public string CancelationFeeAmount { get; set; }
        public bool CancelationFeeBilled { get; set; }
        public bool DiscountOffered { get; set; }
        public Nullable<System.DateTime> DateDiscountOffered { get; set; }
        public Nullable<decimal> AmountofDiscount { get; set; }
        public Nullable<decimal> EstimateDollarAmount { get; set; }
        public Nullable<System.DateTime> EstimateDollarAmountReceivedDate { get; set; }
        public string AccountRep { get; set; }
        public string PersonToCalculate { get; set; }
        public Nullable<System.DateTime> DateAssignedToRep { get; set; }
        public Nullable<System.DateTime> EstDateCalculation { get; set; }
        public Nullable<System.DateTime> DateCalculated { get; set; }
        public Nullable<decimal> CalculatedAmount { get; set; }
        public Nullable<System.DateTime> EstDateReportToBeDone { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
        public string ReportAmount { get; set; }
        public string LOADate { get; set; }
        public string PayNotes { get; set; }
        public Nullable<System.DateTime> ReportChecklistReceived { get; set; }
        public Nullable<System.DateTime> AdditionsReceived { get; set; }
        public Nullable<System.DateTime> FinalReportDate { get; set; }
        public Nullable<decimal> FinalReportAmount { get; set; }
        public Nullable<decimal> NonRefundAdvance { get; set; }
        public Nullable<decimal> NonRefundContract { get; set; }
        public Nullable<decimal> NonRefundPrePay { get; set; }
        public Nullable<decimal> NonRefundFuture { get; set; }
        public string CommentSalesTracking { get; set; }
        public string Assigned { get; set; }
        public string Travel { get; set; }
        public int RentalCar { get; set; }
        public Nullable<System.DateTime> AuditDate { get; set; }
        public string ARMeetingStatus { get; set; }
        public Nullable<System.DateTime> InKerr { get; set; }
        public Nullable<System.Guid> AcctRep { get; set; }
        public string PayDate { get; set; }
        public Nullable<System.Guid> SecondaryAuditor { get; set; }
        public Nullable<System.DateTime> CollectionsReportDate { get; set; }
        public Nullable<System.DateTime> CollectionsDateRecvd { get; set; }
        public Nullable<System.DateTime> CollectionsDateAssigned { get; set; }
        public Nullable<System.Guid> CollectionsAssignedTo { get; set; }
        public Nullable<System.DateTime> CollectionsDatePktRecvd { get; set; }
        public string CollectionsNotes { get; set; }
        public Nullable<System.DateTime> LastCallDate { get; set; }
        public Nullable<bool> CollectionClosed { get; set; }
        public Nullable<int> ofLocations { get; set; }
        public string LeadSource { get; set; }
        public Nullable<decimal> ReviewRevenue { get; set; }
        public Nullable<int> EmployeeCount { get; set; }
        public Nullable<System.DateTime> JobFinalDate { get; set; }
    }
}
