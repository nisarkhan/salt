using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace salt_web.Models
{

    public class StateRefundEntryViewModel
    {
        public System.Guid pkid { get; set; }
        public Nullable<System.Guid> ClientPKID { get; set; }
        public string BatchDescription { get; set; }
        public Nullable<decimal> AmountSubmitted { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public Nullable<decimal> DeniedAdjusted { get; set; }
        public Nullable<decimal> BalanceState { get; set; }
        public string BatchNumber { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> DateMTSMailed { get; set; }
    }

    public class FutureBenefitsEntryViewModel
    {        
        public System.Guid pkid { get; set; }
        public Nullable<System.Guid> ClientPKID { get; set; }
        public string Installment { get; set; }
        public Nullable<decimal> OriginalAmount { get; set; }
        public Nullable<decimal> Adjusted { get; set; }
        public string CollectionNote { get; set; }
        public Nullable<decimal> AmountRecovered { get; set; }
        public string BatchNumber { get; set; }
        public string Notes { get; set; }
    }

    #region Refund Summary

    public class RefundSummaryHeaderViewModel
    {
        public System.Guid StateIdentified { get; set; }
        public string FullName { get; set; }
    }

    public class RefundCollectionsList
    {
        public RefundSummaryViewModel RefCollections { get; set; }

        public RefundCollectionsList()
        {
            RefCollections = new RefundSummaryViewModel();
        }
    }

    public class RefundSummaryViewModel
    {
        public List<RefundSummaryViewModelGrid> RefundCollections { get; set; }

        public RefundSummaryViewModel()
        {
            RefundCollections = new List<RefundSummaryViewModelGrid>();
        }
         
        public string Abbrev { get; set; }
        public string FullName { get; set; }

        
    }

    public class RefundSummaryViewModelGrid
    {
        public Nullable<System.Guid> PKID { get; set; }
        public Nullable<System.Guid> ClientPKID { get; set; }        
        public string VendorName { get; set; }
        public string CompanyLegalName { get; set; }
        public string Abbrev { get; set; }
        public string FullName { get; set; }
        public Nullable<Decimal> TaxAmountIdentified { get; set; }
        public Nullable<Decimal> Adjust { get; set; }
        public string CollectionNoteName { get; set; }
        public Nullable<Decimal> AmountRecovered { get; set; }
        public string BatchNumber { get; set; }
        public string Notes { get; set; }
        public Nullable<Decimal> LeftToRecover { get; set; }
        public Nullable<Decimal> AmountIdentified { get; set; }
    }

    public class RefundSummarySum
    {
        //calcluations field:
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? TaxAmountIdentifiedSum { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? AdjustSum { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? AmountReceivedSum { get; set; }
        public decimal? BatchPercent { get; set; }
        public decimal? NotesPercent { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? LeftToReceoverSum { get; set; }
        public string CreditInterestRevieved { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? AdjustedTotalTax { get; set; }
    }
    #endregion

    #region Refund at State

    public class RefundState
    {
        public Nullable<System.Guid> PKID { get; set; }
        public string BatchDesc { get; set; }
        public Nullable<Decimal> AmountSubmitted { get; set; }
        public Nullable<Decimal> AmountPaid { get; set; }
        public Nullable<Decimal> DeniedAdjusted { get; set; }
        public Nullable<Decimal> BalanceAtState { get; set; }
        public string BatchNumber { get; set; }        
        public DateTime? MTSMailed { get; set; }
        public string Notes { get; set; }
    }

    public class FutureBenefit
    {
        public Nullable<System.Guid> PKID { get; set; }
        public Nullable<System.Guid> ClientPKID { get; set; }
        public string Installment { get; set; }
        public Nullable<Decimal> OrginalAmount { get; set; }
        public Nullable<Decimal> Adjusted { get; set; }
        public string CollectionNote { get; set; }
        public Nullable<Decimal> AmountRecovered { get; set; }
        public string BatchNumber { get; set; }        
        public string Notes { get; set; }
    }

    public class FutureBenefitSum
    {
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Nullable<Decimal> OrginalAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Nullable<Decimal> Adjusted { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Nullable<Decimal> AmountRecovered { get; set; }
    }

    public class CustomerPaymentViewModel
    {
        public Nullable<System.Guid> PKID { get; set; }
        public Nullable<System.Guid> ClientPKID { get; set; }
        public string CheckNumber { get; set; }
        public Nullable<Decimal> PrepayAmount { get; set; }
        public Nullable<Decimal> PrePayCredit { get; set; }
        public Nullable<Decimal> PrePayBalance { get; set; }
        public Nullable<Decimal> AppliedBalance { get; set; }
        public string BatchNumber { get; set; }
        public string Notes { get; set; }
        public Nullable<DateTime> TimeStamp { get; set; }
    }

    public class CustomerPaymentViewModelSum
    {
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Nullable<Decimal> PrepayBalance { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Nullable<Decimal> AppliedToBalanceDue { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Nullable<Decimal> FeeLeftToCollect { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Nullable<Decimal> FeeLeftToCollectWRAS { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Nullable<Decimal> BalanceToQuickBooksEdit { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Nullable<Decimal> BalanceToQuickBooks { get; set; }


    }


    public class RefundStateSum
    {
        //calcluations field:
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? AmountSubmitted { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? AmountPaid { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? DeniedAdjusted { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? BalanceAtState { get; set; }         
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? TotalRefundAtState { get; set; }
    }

     

    #endregion

    public class BillingInformationViewModel
    {
        public BillingInformationViewModel() 
        { 
            RefundSummary = new List<RefundCollectionsList>();
            RefundSummaryTotal = new RefundSummarySum();
            StateRefund = new List<RefundState>();
            FutureBenefits = new List<FutureBenefit>();

            CustomerPayments = new List<CustomerPaymentViewModel>();
        }

        public StateRefundEntryViewModel RefundStateViewModel { get; set; }
        public string CreditInterestRevieved { get; set; }
        public decimal? TaxAmountIdentified { get; set; }

        public List<RefundCollectionsList> RefundSummary { get; set; }
        public RefundSummarySum RefundSummaryTotal { get; set; }

        public List<RefundState> StateRefund { get; set; }
        public RefundStateSum RefundStateTotal { get; set; }

        public List<FutureBenefit> FutureBenefits { get; set; }
        public FutureBenefit FutureBenefit { get; set; }
        
        public FutureBenefitSum FutureBenefitTotal { get; set; }

        public List<CustomerPaymentViewModel> CustomerPayments { get; set; }
        public CustomerPaymentViewModel CustomerPayment { get; set; }
        public CustomerPaymentViewModelSum CustomerPaymentSum { get; set; }

        public int CustomerID { get; set; }
        public Guid ClientPKID { get; set; }
        public Nullable<System.Guid> PKID { get; set; }
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
        public Nullable<bool> MultiTaxID { get; set; }
        public Nullable<bool> BillingAddressSame { get; set; }
        public Nullable<bool> BillingContactSame { get; set; }
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
        public Nullable<bool> RecordsAddressSame { get; set; }
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
        public Nullable<bool> CancelationFee { get; set; }
        public string CancelationFeeAmount { get; set; }
        public Nullable<bool> CancelationFeeBilled { get; set; }
        public Nullable<bool> DiscountOffered { get; set; }
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
        public string AcctRep { get; set; }
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
        public Nullable<System.DateTime> JobFinalDate {get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Consultant { get; set; }
        

    }

    public class RefundEntriesViewModel
    {   
        public Nullable<System.Guid> PKID { get; set; }
        public Nullable<System.Guid> ClientPKID { get; set; }
        public Nullable<System.Guid> VendorPKID { get; set; }
        public Nullable<decimal> AmountIdentified { get; set; }
        public System.Guid StateIdentified { get; set; }
        public Nullable<decimal> Adjusted { get; set; }
        public Nullable<System.Guid> CollectionNote { get; set; }
        public Nullable<decimal> AmountRecovered { get; set; }
        public string BatchNumber { get; set; }
        public string Notes { get; set; }
        public Nullable<System.Guid> RecoveryType { get; set; }
        public Nullable<decimal> LeftToRecover { get; set; }

    }

    public class MenuUSStatesViewModel
    {
        public Nullable<System.Guid> PKID { get; set; }
        public string FullName { get; set; }
        public string Abbrev { get; set; }
    }
}