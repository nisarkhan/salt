using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using salt_web.Models;
using salt_web.Helper;
using PagedList;
using AutoMapper;

namespace salt_web.Helper
{
    public static class GetBillingInformation
    {
        public static BillingInformationViewModel GetBillingInformationBy(Guid id)
        {
            using (var context = new salt_entities())
            {
                BillingInformationViewModel _data = new BillingInformationViewModel();
                BillingInformationHelper binfoHelper = new BillingInformationHelper();

                _data = binfoHelper.BillingInformationEdit(id);
                _data.CreditInterestRevieved = context.CreditInterestReveiveds.Where(x => x.ClientPKID == id).Select(x => x.Number).FirstOrDefault();

                return _data;
            }
        }
    }


    public class BillingInformationHelper : Controller
    {
        private salt_entities db = new salt_entities();

        public BillingInformationViewModel BillingInformationEdit(Guid? id)
        {
            var _data = new BillingInformationViewModel();

            //header
            using (var context = new salt_entities())
            {
                string sql = string.Format(" SELECT c.FirstName + ' ' + c.LastName as CollectionAssignedToName, a.[FirstName]+' '+a.[LastName] AS AcctRep, b.[FirstName]+' '+b.LastName AS Consultant, * " +
                                         " FROM dbo.Clients LEFT OUTER JOIN " +
                                         " dbo.aspnet_Users AS c ON dbo.Clients.CollectionsAssignedTo = c.UserId LEFT OUTER JOIN" +
                                         " dbo.aspnet_Users AS b ON dbo.Clients.Auditors_Key = b.UserId LEFT OUTER JOIN" +
                                         " dbo.aspnet_Users AS a ON dbo.Clients.AcctRep = a.UserId" +
                                         " WHERE dbo.Clients.ClientPKID = '{0}'", id);
                _data = context.Database.SqlQuery<BillingInformationViewModel>(sql).FirstOrDefault();
            }

            List<RefundCollectionsList> refundSummaryCollectionLists = new List<RefundCollectionsList>();

            
            using (var context = new salt_entities())
            {
                //refund summary:
                #region refund summary
                //string sql = " SELECT DISTINCT a.StateIdentified, b.fullname FROM RefundEntry a join menuusstates b on a.stateidentified=b.PKID WHERE ClientPKID = '7e53f21f-0e94-4c46-a671-0055016bac64' ORDER BY b.fullname";
                //var _data1 = context.Database.SqlQuery<BillingInformationViewModel>(sql).ToList();

                IEnumerable<RefundSummaryHeaderViewModel> _refundSummaryHeader = 
                    (from a in context.RefundEntries
                        join b in context.MenuUSStates on a.StateIdentified equals b.PKID
                        where a.ClientPKID == id
                            select new RefundSummaryHeaderViewModel
                            {
                                StateIdentified = a.StateIdentified,
                                FullName = b.FullName
                            }).OrderBy(b => b.FullName).Distinct().ToList();

                foreach (var item in _refundSummaryHeader)
                {
                    RefundCollectionsList lists = new RefundCollectionsList();
                   

                    string sql = string.Format("SELECT  " +
	                                                "dbo.RefundEntry.PKID,  " +
	                                                "dbo.RefundEntry.LeftToRecover,  " +
	                                                "dbo.Vendors.CompanyLegalName,  " +
                                                    "dbo.RefundEntry.AmountIdentified, " +
	                                                "dbo.MenuUSStates.Abbrev,  " +
                                                    "dbo.MenuUSStates.FullName,  " +
	                                                "dbo.RefundEntry.Adjusted as Adjust, " +
                                                    "dbo.MenuCollectionNote.CollectionNoteName,  " +
	                                                "dbo.RefundEntry.AmountRecovered, " +
	                                                "dbo.RefundEntry.BatchNumber,  " +
                                                    "dbo.RefundEntry.Notes  " +	
                                                    "FROM             " +
	                                                "   dbo.RefundEntry INNER JOIN " +
	                                                "   dbo.MenuUSStates ON dbo.RefundEntry.StateIdentified = dbo.MenuUSStates.PKID INNER JOIN " +
	                                                "   dbo.Vendors ON dbo.Vendors.VendorPKID = dbo.RefundEntry.VendorPKID LEFT OUTER JOIN " +
	                                                "   dbo.MenuCollectionNote ON dbo.RefundEntry.CollectionNote = dbo.MenuCollectionNote.CollectionNotePKID " +
                                            " WHERE (dbo.RefundEntry.ClientPKID = '{0}') AND  " +
                                            " (dbo.RefundEntry.StateIdentified = '{1}') " +
                                            " ORDER BY dbo.Vendors.CompanyLegalName", id, item.StateIdentified);


                    var items = context.Database.SqlQuery<RefundSummaryViewModelGrid>(sql, id, item.StateIdentified).OrderBy(x => x.FullName).ToList();

                    items = items.Select(x => new RefundSummaryViewModelGrid()
                            {
                                PKID = x.PKID,
                                VendorName = x.CompanyLegalName,
                                CompanyLegalName = x.CompanyLegalName,
                                Abbrev = x.Abbrev,
                                FullName = x.FullName,
                                TaxAmountIdentified = x.AmountIdentified,
                                Adjust = x.Adjust,
                                CollectionNoteName = x.CollectionNoteName,
                                AmountRecovered = x.AmountRecovered,
                                BatchNumber = x.BatchNumber,
                                Notes = x.Notes,
                                LeftToRecover = x.LeftToRecover
                            }).OrderBy(b => b.Abbrev).ToList();

                    #region not using
                    //var items0 =
                    //    (from a in context.RefundEntries
                    //     join b in context.Vendors on a.VendorPKID equals b.VendorPKID
                    //     join d in context.MenuCollectionNotes on a.CollectionNote equals d.CollectionNotePKID
                    //     join c in context.MenuUSStates on a.StateIdentified equals c.PKID
                    //     where a.ClientPKID == id
                    //     where a.StateIdentified == item.StateIdentified
                    //     select new RefundSummaryViewModelGrid
                    //        {
                    //            VendorName = b.CompanyLegalName,
                    //            CompanyLegalName = b.CompanyLegalName,
                    //            Abbrev = c.Abbrev,
                    //            FullName = c.FullName,
                    //            TaxAmountIdentified = a.AmountIdentified,
                    //            Adjust = a.Adjusted,
                    //            CollectionNoteName = d.CollectionNoteName,
                    //            AmountRecovered = a.AmountRecovered,
                    //            BatchNumber = a.BatchNumber,
                    //            Notes = a.Notes,
                    //            LeftToRecover = a.LeftToRecover
                    //        }).OrderBy(b => b.Abbrev).ToList();
                    #endregion

                    IEnumerable<string> fullName = items.Select(x => x.FullName).Distinct();
                    IEnumerable<string> abbrev = items.Select(x => x.Abbrev).Distinct();

                    lists.RefCollections.Abbrev = abbrev.FirstOrDefault();
                    lists.RefCollections.FullName = fullName.FirstOrDefault();
                    lists.RefCollections.RefundCollections.AddRange(items);
                    refundSummaryCollectionLists.Add(lists);
                }
                #endregion

                //refund at state:
                #region refund state
                var itemState =
                           (from a in context.StateRefundEntries
                            where a.ClientPKID == id
                            select new RefundState
                            {
                                PKID = a.pkid,
                                BatchDesc = a.BatchDescription,
                                AmountSubmitted = a.AmountSubmitted,
                                AmountPaid = a.AmountPaid,
                                DeniedAdjusted = a.DeniedAdjusted,
                                BalanceAtState = a.BalanceState,
                                BatchNumber = a.BatchNumber,
                                Notes = a.Notes,
                                MTSMailed = a.DateMTSMailed

                            }).OrderBy(b => b.BatchDesc).ToList();
                _data.StateRefund.AddRange(itemState);

                #endregion

                //future benefits
                #region future benefits
                var itemFutureBenefits =
                           (from a in context.FutureBenefitsEntries
                            where a.ClientPKID == id
                            select new FutureBenefit
                            {
                                PKID = a.pkid,
                                ClientPKID = a.ClientPKID,
                                Installment = a.Installment,
                                OrginalAmount = a.OriginalAmount,
                                Adjusted = a.Adjusted,
                                CollectionNote = a.CollectionNote,
                                AmountRecovered = a.AmountRecovered,
                                BatchNumber = a.BatchNumber,
                                Notes = a.Notes
                            }).OrderBy(b => b.Installment).ToList();
                _data.FutureBenefits.AddRange(itemFutureBenefits);

                #endregion

                //customer payments
                #region future benefits
                var itemCustomerPayments =
                           (from a in context.CustomerPaymentsEntries
                            where a.ClientPKID == id
                            select new CustomerPaymentViewModel
                            {
                                PKID = a.pkid,
                                ClientPKID = a.ClientPKID,
                                CheckNumber = a.CheckNumber,
                                PrepayAmount = a.PrepayAmount,
                                PrePayCredit = a.PrepayCredit,
                                PrePayBalance = a.PrepayBalance,
                                AppliedBalance = a.AppliedBalance,
                                BatchNumber = a.BatchNumber,
                                TimeStamp = a.TimeStamp,
                                Notes = a.Notes
                            }).OrderBy(b => b.TimeStamp).ToList();  
                _data.CustomerPayments.AddRange(itemCustomerPayments);

                #endregion

            }

            _data.RefundSummary.AddRange(refundSummaryCollectionLists);
            
            #region refund summary totals

            decimal? taxAmountIdentifiedSum = _data.RefundSummary.Select(x => x.RefCollections.RefundCollections.ToList().Sum(y => y.TaxAmountIdentified)).Sum();
            decimal? adjustSum = _data.RefundSummary.Select(x => x.RefCollections.RefundCollections.ToList().Sum(y => y.Adjust)).Sum();
            decimal? amountReceivedSum = _data.RefundSummary.Select(x => x.RefCollections.RefundCollections.ToList().Sum(y => y.AmountRecovered)).Sum();

            string batchPercentSQL = string.Format("SELECT (SELECT SUM([AmountRecovered])) / (SELECT (SELECT SUM([AmountIdentified])) + (SELECT SUM([Adjusted]))) * 100 FROM refundentry WHERE ClientPKID ='{0}'", id);
            decimal? batchPercent = db.Database.SqlQuery<decimal?>(batchPercentSQL).FirstOrDefault();

            string adjustedTotalTaxSQL = string.Format("SELECT (SUM([Adjusted])) + (SELECT SUM([AmountIdentified]))  FROM [refundentry] WHERE ([ClientPKID] ='{0}')", id);
            decimal? adjustedTotalTax = db.Database.SqlQuery<decimal?>(adjustedTotalTaxSQL).FirstOrDefault();

            string notesPercentSQL = string.Format("SELECT SUM([AmountRecovered]) / (SUM([AmountIdentified]) + SUM([Adjusted])) * 100 FROM [refundentry] WHERE [ClientPKID] = '{0}'", id);
            decimal? notesPercent = db.Database.SqlQuery<decimal?>(notesPercentSQL).FirstOrDefault();

            decimal? leftToReceoverSum = _data.RefundSummary.Select(x => x.RefCollections.RefundCollections.ToList().Sum(y => y.LeftToRecover)).Sum();
            string creditInterestRevieved = db.CreditInterestReveiveds.Where(a => a.ClientPKID == id).Select(x => x.Number).FirstOrDefault();

            _data.RefundSummaryTotal.TaxAmountIdentifiedSum = taxAmountIdentifiedSum;
            _data.RefundSummaryTotal.AdjustSum = adjustSum;
            _data.RefundSummaryTotal.AmountReceivedSum = amountReceivedSum;

            _data.RefundSummaryTotal.BatchPercent = batchPercent;
            _data.RefundSummaryTotal.AdjustedTotalTax = adjustedTotalTax;
            _data.RefundSummaryTotal.NotesPercent = notesPercent;
            _data.RefundSummaryTotal.LeftToReceoverSum = leftToReceoverSum;
            _data.RefundSummaryTotal.CreditInterestRevieved = creditInterestRevieved;

            #endregion

            #region refund state totals

            RefundStateSum refundStateTotals = new RefundStateSum();

            var totals = from p in db.StateRefundEntries
                        where p.ClientPKID == id 
                        group p by 1 into g

                        select new RefundStateSum
                        {
                           AmountSubmitted = g.Sum(x => x.AmountSubmitted),
                           AmountPaid = g.Sum(x => x.AmountPaid),
                           DeniedAdjusted = g.Sum(x => x.DeniedAdjusted),
                           BalanceAtState = g.Sum(x => x.BalanceState)
                        };

            refundStateTotals.TotalRefundAtState = (db.StateRefundEntries.Where(x => x.ClientPKID == id)
                                        .Select(x => x.BalanceState).Sum()) + (db.RefundEntries.Where(x => x.ClientPKID == id)
                                        .Select(x => x.AmountRecovered).Sum());


            refundStateTotals.AmountSubmitted = totals.Select(x => x.AmountSubmitted).FirstOrDefault();
            refundStateTotals.AmountPaid = totals.Select(x => x.AmountPaid).FirstOrDefault();
            refundStateTotals.DeniedAdjusted = totals.Select(x => x.DeniedAdjusted).FirstOrDefault();
            refundStateTotals.BalanceAtState = totals.Select(x => x.BalanceAtState).FirstOrDefault();

            _data.RefundStateTotal = refundStateTotals;


            #endregion

            #region future benefit totals

            FutureBenefitSum futureBenefit = new FutureBenefitSum();

            var benefitTotal = from p in db.FutureBenefitsEntries
                         where p.ClientPKID == id
                         group p by 1 into g

                         select new FutureBenefitSum
                         {
                             OrginalAmount = g.Sum(x => x.OriginalAmount),
                             //Adjusted = g.Sum(x => x.Adjusted),                             
                             AmountRecovered = g.Sum(x => x.AmountRecovered),
                             Adjusted = g.Sum(x => x.AmountRecovered) - g.Sum(x => x.OriginalAmount)
                         };

            futureBenefit.OrginalAmount = benefitTotal.Select(x => x.OrginalAmount).FirstOrDefault();            
            futureBenefit.Adjusted = benefitTotal.Select(x => x.Adjusted).FirstOrDefault();
            //futureBenefit.Adjusted = ((futureBenefit.OrginalAmount) - (futureBenefit.Adjusted));
            futureBenefit.AmountRecovered = benefitTotal.Select(x => x.AmountRecovered).FirstOrDefault();

            _data.FutureBenefitTotal = futureBenefit;

            #endregion

            //customer payment
            #region customer payment totals

            CustomerPaymentViewModelSum customerPaymentSum = new CustomerPaymentViewModelSum();

            var prepayAmount_prepayCredit = from p in db.CustomerPaymentsEntries
                               where p.ClientPKID == id
                               group p by 1 into g
                                select new CustomerPaymentViewModelSum
                               {      
                                   PrepayBalance = g.Sum(x => x.PrepayAmount) - g.Sum(x => x.PrepayCredit),
                                   AppliedToBalanceDue = g.Sum(x => x.AppliedBalance)
                               };

            //FEE LEFT TO COLLEC
            string feeLeftSQL = string.Format("DECLARE @FeeTerm DECIMAL(18, 4) SELECT @FeeTerm = ISNULL(FeeTerms, 0) FROM Clients WHERE [ClientPKID] = '{0}' SET @FeeTerm = ISNULL(@FeeTerm, 0) / 100 DECLARE @Total1 DECIMAL(18, 4) SELECT @Total1 =SUM([AmountRecovered]) FROM [refundentry] WHERE ([ClientPKID] = '{0}') SET @Total1 = ISNULL(@Total1, 0) DECLARE @Total2 DECIMAL(18, 4) SELECT @Total2 = (CASE WHEN Number IS NULL OR Number = '' THEN '0.00' ELSE Number END) FROM [CreditInterestReveived] WHERE ([ClientPKID] = '{0}') SET @Total2 = ISNULL(@Total2, 0) DECLARE @Total3 DECIMAL(18, 4) SELECT @Total3 = SUM([AmountRecovered]) FROM [futurebenefitsentry] WHERE ([ClientPKID] = '{0}') SET @Total3 = ISNULL(@Total3, 0) DECLARE @Total4 DECIMAL(18, 4) SELECT @Total4 = SUM([AppliedBalance]) FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = convert(uniqueidentifier, '{0}')) SET @Total4 = ISNULL(@Total4, 0) SELECT (@Total1 + @Total2 + @Total3) * @FeeTerm - @Total4", id);
            decimal? feeLeftToCollect = db.Database.SqlQuery<decimal?>(feeLeftSQL).FirstOrDefault();

            //FEE LEFT TO COLLECT W/RA'S	
            feeLeftSQL = string.Format("DECLARE @FeeTerm DECIMAL(18, 4) SELECT @FeeTerm = ISNULL(FeeTerms, 0) FROM Clients WHERE [ClientPKID] = '{0}' SET @FeeTerm = ISNULL(@FeeTerm, 0) / 100 DECLARE @Total1 DECIMAL(18, 4) SET @Total1 = (SELECT ISNULL(SUM([BalanceState]),0) FROM [staterefundentry]  WHERE ([ClientPKID] = '{0}'))+(SELECT ISNULL(SUM([AmountRecovered]),0) FROM [refundentry]  WHERE ([ClientPKID] = '{0}'))+(SELECT ISNULL(SUM([AmountSubmitted]),0) FROM [staterefundentry] WHERE ([ClientPKID] = '{0}')) SET @Total1 = ISNULL(@Total1, 0) DECLARE @Total2 DECIMAL(18, 4) SELECT @Total2 = (CASE WHEN Number IS NULL OR Number = '' THEN '0.00' ELSE Number END) FROM [CreditInterestReveived] WHERE ([ClientPKID] = '{0}') SET @Total2 = ISNULL(@Total2, 0) DECLARE @Total3 DECIMAL(18, 4) SELECT @Total3 = SUM([AmountRecovered]) FROM [futurebenefitsentry] WHERE ([ClientPKID] = '{0}') SET @Total3 = ISNULL(@Total3, 0) DECLARE @Total4 DECIMAL(18, 4) SELECT @Total4 = SUM([AppliedBalance]) FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = convert(uniqueidentifier, '{0}')) SET @Total4 = ISNULL(@Total4, 0) SELECT (@Total1 + @Total2 + @Total3) * @FeeTerm - @Total4", id);
            decimal? feeLeftToCollectWRAS = db.Database.SqlQuery<decimal?>(feeLeftSQL).FirstOrDefault();


            //BALANCE TO QUICKBOOKS:
            feeLeftSQL = string.Format("declare @ClientPKID varchar(60) set @ClientPKID = '{0}' IF NOT EXISTS(SELECT TOP 1 1 FROM ProgressPayments WHERE [ClientPKID] = CONVERT(UNIQUEIDENTIFIER, @ClientPKID)) INSERT INTO ProgressPayments(ClientPKID, ProgressPayment) VALUES (CONVERT(UNIQUEIDENTIFIER, @ClientPKID), '') SELECT ProgressPayment FROM [ProgressPayments] WHERE [ClientPKID] = CONVERT(UNIQUEIDENTIFIER, @ClientPKID)", id);
            decimal? balanceToQuickbooksEdit = db.Database.SqlQuery<decimal?>(feeLeftSQL).FirstOrDefault();

            feeLeftSQL = string.Format("declare @ClientPKID varchar(60) set @ClientPKID = '{0}' DECLARE @FeeTerm DECIMAL(18, 4) SELECT @FeeTerm = ISNULL(FeeTerms, 0) FROM Clients WHERE [ClientPKID] = convert(uniqueidentifier,@ClientPKID) SET @FeeTerm = ISNULL(@FeeTerm, 0) / 100 DECLARE @Total1 DECIMAL(18, 4) SELECT @Total1 =SUM([AmountRecovered]) FROM [refundentry] WHERE ([ClientPKID] = @ClientPKID) SET @Total1 = ISNULL(@Total1, 0) DECLARE @Total2 DECIMAL(18, 4) SELECT @Total2 = (CASE WHEN Number IS NULL OR Number = '' THEN '0.00' ELSE Number END) FROM [CreditInterestReveived] WHERE ([ClientPKID] = @ClientPKID) SET @Total2 = ISNULL(@Total2, 0) DECLARE @Total3 DECIMAL(18, 4) SELECT @Total3 = SUM([AmountRecovered]) FROM [futurebenefitsentry] WHERE ([ClientPKID] = @ClientPKID) SET @Total3 = ISNULL(@Total3, 0) DECLARE @Total4 DECIMAL(18, 4) SELECT @Total4 = SUM([AppliedBalance]) FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = convert(uniqueidentifier, @ClientPKID)) SET @Total4 = ISNULL(@Total4, 0) DECLARE @Total5 DECIMAL(18, 4) SELECT @Total5 = SUM([PrepayAmount]) FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = convert(uniqueidentifier, @ClientPKID)) SET @Total5 = ISNULL(@Total5, 0) DECLARE @Total6 DECIMAL(18, 4) SELECT @Total6 = [ProgressPayment] FROM [ProgressPayments] WHERE ([ClientPKID] = convert(uniqueidentifier, @ClientPKID)) SET @Total6 = ISNULL(@Total6, 0) SELECT (@Total1 + @Total2 + @Total3) * @FeeTerm - @Total4 + @Total5 +@Total6", id);
            decimal? balanceToQuickbooks = db.Database.SqlQuery<decimal?>(feeLeftSQL).FirstOrDefault();


            customerPaymentSum.FeeLeftToCollect = feeLeftToCollect;
            customerPaymentSum.FeeLeftToCollectWRAS = feeLeftToCollectWRAS;
            customerPaymentSum.BalanceToQuickBooksEdit =  balanceToQuickbooksEdit;
            customerPaymentSum.BalanceToQuickBooks =  balanceToQuickbooks;


            customerPaymentSum.PrepayBalance = prepayAmount_prepayCredit.Select(x => x.PrepayBalance).FirstOrDefault();
            customerPaymentSum.AppliedToBalanceDue = prepayAmount_prepayCredit.Select(x => x.AppliedToBalanceDue).FirstOrDefault();

             

            _data.CustomerPaymentSum = customerPaymentSum;

            #endregion

            return _data;
        }
    }
}