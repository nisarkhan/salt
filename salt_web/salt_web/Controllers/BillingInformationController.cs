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


namespace salt_web.Controllers
{
    public class BillingInformationController : BaseController
    {
        private salt_entities db = new salt_entities();

        
        #region Collection

        public ViewResult Collection(Guid id)
        {
            DropDownLoadHelper ddl = new DropDownLoadHelper();
            ddl.DropDownLoadRecoveryType();
            ViewBag.DropDownLoadRecoveryType = RecoveryTypesDropDowns.RecoveryTypes;


            // IEnumerable<SelectListItem> lists = list.Select(b => new SelectListItem { Value = b.PKID.ToString().Trim(), Text = b.RecoveryType.Trim() }).ToList();

            // ViewBag.DropDownLoadRecoveryType = new SelectList(context.Types.OrderBy(b => b.Sort), "ID", "Type");


            //var list = db.MenuRecoveryTypes.OrderBy(b => b.Sort);
            //ViewBag.DropDownLoadRecoveryType = new SelectList(list.ToList(), "PKID", "RecoveryType");


            //Guid _pkid = new Guid();
            string sql = string.Format(" select refund.pkid,vendors.VendorPKID as VendorPKID, vendors.companylegalname as 'Vendor', " +
                            " refund.amountidentified, " +
                            " refund.amountrecovered, refund.recoverytype as Type, " +
                            " refund.amountidentified, " +
                            " rtrim(vendors.contactfirstname) + ' ' + vendors.contactlastname as 'Contact', " +
                            " vendors.emailaddress as 'E-mail', vendors.phonenumber, " +
                            " refund.recoveryType from dbo.refundentry as refund join dbo.vendors as vendors " +
                            " on refund.VendorPKID=vendors.VendorPKID  join dbo.menuusstates as states  " +
                            " on refund.stateidentified=states.pkid join dbo.MenuRecoveryType as mrTypes " +
                            " on mrTypes.pkid = refund.recoverytype where clientpkid='{0}' " +
                            "  /*and refund.stateidentified=convert(uniqueidentifier,@state)*/ " +
                            " order by mrTypes.sort , " +
                            " vendors.companylegalname ", id);

            CollectionDetailViewModel collectionDetailViewModel = new CollectionDetailViewModel();
            List<CollectionDetailViewModel> collectionDetailViewModelList = new List<CollectionDetailViewModel>();

            List<CollectionViewModel> collectionViewModelList = new List<CollectionViewModel>();
            List<CollectionViewModel> collectionViewModelDetailList = new List<CollectionViewModel>();

            //collectionViewModelList = db.Database.SqlQuery<CollectionViewModel>(sql).Select(b => new CollectionViewModel 
            //{                    
            //    VendorPKID = b.VendorPKID, 
            //    PKID = b.PKID,
            //    Vendor = b.Vendor,
            //    AmountIdentified = b.AmountIdentified.HasValue ? b.AmountIdentified.Value : 0 ,
            //    AmountRecovered = b.AmountRecovered.HasValue ? b.AmountRecovered.Value : 0, 
            //    AmountDue = b.AmountIdentified.HasValue ? b.AmountIdentified.Value : 0, 
            //    Type = b.Type.Value,
            //    RecoverType = b.RecoverType.HasValue ? b.RecoverType.Value : new Guid(), 
            //    Contact = b.Contact,
            //    PhoneNumber = b.PhoneNumber,
            //    Email = b.Email,
            //    EntryPKID = b.EntryPKID,
            //    DropDownTypes = RecoveryTypesDropDowns.RecoveryTypes

            //}).ToList();


            collectionViewModelList = db.Database.SqlQuery<CollectionViewModel>(sql).ToList();
            CollectionViewModel collectionViewModel = new CollectionViewModel();
            foreach (var item in collectionViewModelList)
            {
                collectionViewModel = new CollectionViewModel();
                //collectionmodellist
                collectionViewModel.VendorPKID = item.VendorPKID;
                collectionViewModel.PKID = item.PKID;
                collectionViewModel.Vendor = item.Vendor;
                collectionViewModel.AmountIdentified = item.AmountIdentified.HasValue ? item.AmountIdentified.Value : 0;
                collectionViewModel.AmountRecovered = item.AmountRecovered.HasValue ? item.AmountRecovered.Value : 0;
                collectionViewModel.AmountDue = item.AmountIdentified.HasValue ? item.AmountIdentified.Value : 0;
                collectionViewModel.Type = item.Type.Value;
                collectionViewModel.SelectedType = item.Type.Value.ToString();
                collectionViewModel.RecoverType = item.RecoverType.HasValue ? item.RecoverType.Value : new Guid();
                collectionViewModel.Contact = item.Contact;
                collectionViewModel.PhoneNumber = item.PhoneNumber;
                collectionViewModel.Email = item.Email;
                collectionViewModel.EntryPKID = item.EntryPKID;
                collectionViewModel.DropDownTypes = RecoveryTypesDropDowns.RecoveryTypes;


                //collectionviewmodeldetaillist

                sql = string.Format(" SELECT pkid, convert(nvarchar,date,1) as date, CASE WHEN  aspnet.firstname is null THEN    " +
                                   " aspnet.UserName ELSE  aspnet.firstname+' '+aspnet.lastname END AS rep, notes " +
                                   " FROM [CallLog] join aspnet_users as aspnet on rep=userid " +
                                   " WHERE ([EntryPKID] = convert(uniqueidentifier,'{0}')) ORDER BY  CallLog.date DESC ", item.PKID);

                collectionDetailViewModelList = db.Database.SqlQuery<CollectionDetailViewModel>(sql).Select(b => new CollectionDetailViewModel
                {
                    PKID = b.PKID,
                    Date = b.Date,
                    Notes = b.Notes,
                    Rep = b.Rep
                }).ToList();


                collectionViewModel.CollectionDetailViewModel = collectionDetailViewModelList;
                collectionViewModelDetailList.Add(collectionViewModel);


            }

            sql = string.Format(" select ClientCode as 'Job Code', AcctRep.firstname+' '+acctrep.lastname as 'Acct Rep', auditor.firstname+' '+auditor.lastname as Auditor, CompanyLegalName as Client, " +
                    " TaxID as 'Tax ID', CollectionsReportDate as CollectionsReportDate, CollectionsDateRecvd as CollectionsDateRecvd,  " +
                    " CollectionsDateAssigned as CollectionsDateAssigned, (SELECT TOP 1 X.FirstName + ' ' + X.LastName FROM dbo.aspnet_users X WHERE X.UserId =  CollectionsAssignedTo ) AS CollectionsAssignedTo,  " +
                    " CollectionsDatePktRecvd as CollectionsDatePktRecvd, CollectionsNotes,LastCallDate from dbo.clients join dbo.aspnet_users as auditor on auditors_key=auditor.userid join dbo.aspnet_users as acctrep on acctrep=acctrep.userid  " +
                    " where clientpkid = convert(uniqueidentifier,'{0}')", id);

            CollectionHeaderViewModel collectionHeaderViewModel = new CollectionHeaderViewModel();
            collectionHeaderViewModel = db.Database.SqlQuery<CollectionHeaderViewModel>(sql).Select(b => new CollectionHeaderViewModel
            {
                JobCode = b.JobCode,
                Acct = b.Acct,
                Rep = b.Rep,
                Auditor = b.Auditor,
                Client = b.Client,
                TaxID = b.TaxID,
                CollectionsReportDate = b.CollectionsReportDate.HasValue ? b.CollectionsReportDate.Value : DateTime.MinValue,
                CollectionsDateRecvd = b.CollectionsDateRecvd.HasValue ? b.CollectionsDateRecvd.Value : DateTime.MinValue,
                CollectionsDateAssigned = b.CollectionsDateAssigned.HasValue ? b.CollectionsDateAssigned.Value : DateTime.MinValue,
                CollectionsAssignedTo = b.CollectionsAssignedTo.ToString(),
                CollectionsDatePktRecvd = b.CollectionsDatePktRecvd.HasValue ? b.CollectionsDatePktRecvd.Value : DateTime.MinValue,
                CollectionsNotes = b.CollectionsNotes,
                LastCallDate = b.LastCallDate.HasValue ? b.LastCallDate.Value : DateTime.MinValue,
            }).FirstOrDefault();

            collectionViewModel.CollectionHeaderViewModel = collectionHeaderViewModel;
            return View(collectionViewModelDetailList);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CollectionUpdateTypeDDL(Guid? id)
        {
            string dropDownLoadRecoveryType = Request["DropDownLoadRecoveryType"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CollectionInsertTextArea(string notes, Guid? id)
        {
            return View();
        }

        #endregion

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            TempData["message"] = null;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.ClientCodeSortParm = String.IsNullOrEmpty(sortOrder) ? "ClientCode_desc" : "";
            ViewBag.CompanyLegalNameSortParm = sortOrder == "CompanyLegalName" ? "CompanyLegalName_desc" : "CompanyLegalName";
            ViewBag.PhoneNumberSortParm = sortOrder == "PhoneNumber" ? "PhoneNumber_desc" : "PhoneNumber";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            using (var context = new salt_entities())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var _data = from s in context.Clients select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    _data = _data.Where(s => s.CompanyLegalName.Contains(searchString) || s.ClientCode.Contains(searchString) || s.PhoneNumber.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "ClientCode_desc":
                        _data = _data.OrderByDescending(s => s.ClientCode);
                        break;
                    case "CompanyLegalName":
                        _data = _data.OrderBy(s => s.CompanyLegalName);
                        break;
                    case "CompanyLegalName_desc":
                        _data = _data.OrderByDescending(s => s.CompanyLegalName);
                        break;
                    case "PhoneNumber":
                        _data = _data.OrderBy(s => s.PhoneNumber);
                        break;
                    case "PhoneNumber_desc":
                        _data = _data.OrderByDescending(s => s.PhoneNumber);
                        break;
                    default:  // ClientCode ascending 
                        _data = _data.OrderBy(s => s.ClientCode);
                        break;
                }

                int pageSize = 25;
                int pageNumber = (page ?? 1);

                var onePageOfData = _data.ToPagedList(pageNumber, pageSize);
                ViewBag.OnePageOfData = onePageOfData;
                return View();
            }
        }
         
        public ActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BillingInformationViewModel _data = new BillingInformationViewModel();
            BillingInformationHelper binfoHelper = new BillingInformationHelper();

            _data = binfoHelper.BillingInformationEdit(id);

            if (_data == null)
            {
                return HttpNotFound();
            }

            _data.CreditInterestRevieved = db.CreditInterestReveiveds.Where(x => x.ClientPKID == id).Select(x => x.Number).FirstOrDefault();

            DropDownLoadHelper ddl = new DropDownLoadHelper();
            ddl.DropDownLoadBillingInformation();
            ViewBag.DropDownVendorNames = BillingInformationDropDowns.VendorNames;
            ViewBag.DropDownState = BillingInformationDropDowns.State;

            return View(_data);
        }

        #region EDITs

        #region RefundSummaryEdit

        public ActionResult RefundSummaryEdit(Guid? id)
        {
            DropDownLoadHelper ddl = new DropDownLoadHelper();
            ddl.DropDownLoadMenuCollectionNote();
            ViewBag.DropDownMenuCollectionNote = MenuCollectionNoteDropDowns.MenuCollectionNote;

            RefundSummaryViewModelGrid rsvm = new RefundSummaryViewModelGrid();
            string sql = string.Format("  SELECT [Vendors].[CompanyLegalName], " +
                                          " [RefundEntry].[PKID], [RefundEntry].[ClientPKID],  " +
                                          " [RefundEntry].[LeftToRecover], " +
                                          " [RefundEntry].[AmountIdentified], [MenuUSStates].[FullName] , " +
                                          " [RefundEntry].[Adjusted] as Adjust , [RefundEntry].[CollectionNote],  " +
                                          " [RefundEntry].[AmountRecovered], " +
                                          " [RefundEntry].[BatchNumber], [RefundEntry].[Notes]   " +
                                          " FROM [RefundEntry] join [Vendors] on [vendors].[vendorpkid]=[refundentry].[vendorpkid]  " +
                                          " join [menuusstates] on [menuusstates].[pkid] = [refundentry].[stateidentified]  " +
                                          " WHERE ([RefundEntry].[PKID] =  '{0}')", id);

            using (var context = new salt_entities())
            {
                var _data = context.Database.SqlQuery<RefundSummaryViewModelGrid>(sql).FirstOrDefault();

                if (_data == null)
                {
                    return HttpNotFound();
                }

                if (_data.CollectionNoteName == null)
                {
                    _data.CollectionNoteName = "723ce801-1d4e-4072-bcdd-4c0e81718841";
                }
                return View(_data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RefundSummaryEdit(RefundSummaryViewModelGrid model, Guid? id)
        {
            decimal? leftToRecover = 0;
            if (!model.LeftToRecover.HasValue)
            {
                leftToRecover = 0;
            }
            else
            {
                leftToRecover = model.LeftToRecover.Value;
            }

            string sql = string.Format(" UPDATE [RefundEntry]  " +
                                       //" SET [ClientPKID] = '{0}', " +
                                       " SET [AmountIdentified] = {0}, " +
                                       " [LeftToRecover] = {1}, " +
                                       " [Adjusted] = {2}," +
                                       " [CollectionNote] = '{3}', " +
                                       " [AmountRecovered] = {4}, " +
                                       " [BatchNumber] = '{5}', " +
                                       " [Notes] = '{6}' " +
                                       " WHERE [PKID] = '{7}'", /*model.PKID,*/ model.AmountIdentified, leftToRecover, model.Adjust, model.CollectionNoteName, model.AmountRecovered, model.BatchNumber, model.Notes, id);
            int rows = db.Database.ExecuteSqlCommand(sql);

            if (rows == 1)
            {
                TempData["type"] = "success";
                TempData["title"] = "Refund Summary";
                TempData["message"] = "The Refund Summary field(s) was updated successfully.";
            }

            DropDownLoadHelper ddl = new DropDownLoadHelper();
            ddl.DropDownLoadMenuCollectionNote();
            ViewBag.DropDownMenuCollectionNote = MenuCollectionNoteDropDowns.MenuCollectionNote;

            return View();

        }

        #endregion

        #region RefundStateEdit

        public ActionResult RefundStateEdit(Guid? id)
        {
            using (var context = new salt_entities())
            {
                StateRefundEntryViewModel _data = context.StateRefundEntries.Where(x => x.pkid == id).Select(x => new StateRefundEntryViewModel
                {
                    pkid = x.pkid,
                    ClientPKID = x.ClientPKID,
                    BatchDescription = x.BatchDescription,
                    AmountSubmitted = x.AmountSubmitted,
                    AmountPaid = x.AmountPaid,
                    DeniedAdjusted = x.DeniedAdjusted,
                    BalanceState = x.BalanceState,
                    BatchNumber = x.BatchNumber,
                    Notes = x.Notes,
                    DateMTSMailed = x.DateMTSMailed
                }).FirstOrDefault();


                if (_data == null)
                {
                    return HttpNotFound();
                }                 
                return View(_data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RefundStateEdit(StateRefundEntryViewModel model, Guid? id)
        {
            string sql = string.Format(" UPDATE [StateRefundEntry] " +
                                " SET [BatchDescription]  = '{0}', " +
                                " [AmountSubmitted]   = {1}, " +
                                " [AmountPaid]        = {2}, " +
                                " [DeniedAdjusted]    = {3}, " +
                                " [BalanceState]      = {4}, " +
                                " [BatchNumber]       = {5}, " +
                                " [DateMTSMailed]     = '{6}',  " +
                                " [Notes]             = '{7}'" +
                                " WHERE PKID = CONVERT(UNIQUEIDENTIFIER, '{8}') ", 
                                model.BatchDescription, model.AmountSubmitted, model.AmountPaid, model.DeniedAdjusted, 
                                ((model.AmountSubmitted - model.AmountPaid) + model.DeniedAdjusted),
                                model.BatchNumber, model.DateMTSMailed, model.Notes, id);

            int rows = db.Database.ExecuteSqlCommand(sql);

            if (rows == 1)
            {
                TempData["type"] = "success";
                TempData["title"] = "Refund State";
                TempData["message"] = "The Refund State field(s) was updated successfully.";
            }

            using (var context = new salt_entities())
            {
                StateRefundEntryViewModel _data = context.StateRefundEntries.Where(x => x.pkid == id).Select(x => new StateRefundEntryViewModel
                {
                    pkid = x.pkid,
                    ClientPKID = x.ClientPKID,
                    BatchDescription = x.BatchDescription,
                    AmountSubmitted = x.AmountSubmitted,
                    AmountPaid = x.AmountPaid,
                    DeniedAdjusted = x.DeniedAdjusted,
                    BalanceState = x.BalanceState,
                    BatchNumber = x.BatchNumber,
                    Notes = x.Notes,
                    DateMTSMailed = x.DateMTSMailed
                }).FirstOrDefault();


                if (_data == null)
                {
                    return HttpNotFound();
                }
                return View(_data);
            } 
        }

        #endregion

        #region FutureBenefitsEdit

        public ActionResult FutureBenefitsEdit(Guid? id)
        {
            using (var context = new salt_entities())
            {
                FutureBenefitsEntryViewModel _data = context.FutureBenefitsEntries.Where(x => x.pkid == id).Select(x => new FutureBenefitsEntryViewModel
                {
                    pkid = x.pkid,
                    ClientPKID = x.ClientPKID,
                    Installment = x.Installment,
                    OriginalAmount = x.OriginalAmount,
                    Adjusted = x.Adjusted,
                    CollectionNote = x.CollectionNote,
                    AmountRecovered = x.AmountRecovered,
                    BatchNumber = x.BatchNumber,
                    Notes = x.Notes,
                    
                }).FirstOrDefault();


                if (_data == null)
                {
                    return HttpNotFound();
                }
                return View(_data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FutureBenefitsEdit(FutureBenefitsEntryViewModel model, Guid? id)
        {
            string sql = string.Format(" DECLARE @Total1 DECIMAL(18, 4) " +
                                       " SET @Total1 = 0.0000  SELECT @Total1 = SUM ([AmountIdentified])  " +
                                       " FROM [refundentry]  " +
                                       " WHERE ([ClientPKID] = CONVERT(UNIQUEIDENTIFIER, '{0}')) " +
                                       " DECLARE @Total2 DECIMAL(18, 4) " +
                                       " SET @Total2 = 0.0000    " +
                                       " SELECT @Total2 = ISNULL(FuturesRate, 0) FROM Clients WHERE ClientPKID = CONVERT(UNIQUEIDENTIFIER, '{0}') " +
                                       " DECLARE @OA DECIMAL(18, 4) " +
                                       " SET @OA = @Total1 * @Total2 / 100 * 0.5 " +
                                       " DECLARE @AD DECIMAL(18, 4) " +
                                       " IF CONVERT(DECIMAL(18,4), {1} ) > 0.0  " +
                                       "     SET @AD = CONVERT(DECIMAL(18,4), {1}) - @OA " +
                                       " ELSE " +
                                       "     IF '{3}' = 'WO' AND CONVERT(DECIMAL(18,4), {1}) = 0 " +
                                       "         SET @AD = CONVERT(DECIMAL(18,4), {1}) - @OA " +
                                       "     ELSE " +
                                       "         SET @AD = 0.0000 " +        
                                       " IF '{2}' = 'DISCOUNTS' OR '{2}' = 'DISCOUNT' OR '{2}' = 'DISC' " +
                                       " BEGIN " +
                                       "     SET @OA = {1} " +
                                       "     SET @AD = 0.0000 " +
                                       " END    " +
                                       " UPDATE [FutureBenefitsEntry]  " +
                                       "     SET " +
                                       "         [Installment]       = '{2}',  " +
                                       "         [OriginalAmount]    = @OA,  " +
                                       "         [Adjusted]          = @AD,  " +
                                       "         [CollectionNote]    = '{3}',  " +
                                       "         [AmountRecovered]   = CONVERT(DECIMAL(18,4), {1}),  " +
                                       "         [BatchNumber]       = '{4}',  " +
                                       "         [Notes]             = '{5}'   " +
                                       "  WHERE PKID = CONVERT(UNIQUEIDENTIFIER, '{6}')  ",
                                                model.ClientPKID, model.AmountRecovered, model.Installment, model.CollectionNote, model.BatchNumber, model.Notes, id);

            
            int rows = db.Database.ExecuteSqlCommand(sql);

            if (rows == 1)
            {
                TempData["type"] = "success";
                TempData["title"] = "Future Benefits";
                TempData["message"] = "The Future Benefits field(s) was updated successfully.";
            }

            using (var context = new salt_entities())
            {
                FutureBenefitsEntryViewModel _data = context.FutureBenefitsEntries.Where(x => x.pkid == id).Select(x => new FutureBenefitsEntryViewModel
                {
                    pkid = x.pkid,
                    ClientPKID = x.ClientPKID,
                    Installment = x.Installment,
                    OriginalAmount = x.OriginalAmount,
                    Adjusted = x.Adjusted,
                    CollectionNote = x.CollectionNote,
                    AmountRecovered = x.AmountRecovered,
                    BatchNumber = x.BatchNumber,
                    Notes = x.Notes,

                }).FirstOrDefault();


                if (_data == null)
                {
                    return HttpNotFound();
                }
                return View(_data);
            }
        }

        #endregion

        #region CustomerPaymentsEdit

        public ActionResult CustomerPaymentsEdit(Guid? id)
        {
            using (var context = new salt_entities())
            {
                CustomerPaymentViewModel _data = context.CustomerPaymentsEntries.Where(x => x.pkid == id).Select(x => new CustomerPaymentViewModel
                {
                    PKID = x.pkid,
                    ClientPKID = x.ClientPKID,
                    CheckNumber = x.CheckNumber,
                    PrepayAmount = x.PrepayAmount,
                    PrePayCredit = x.PrepayCredit,
                    PrePayBalance = x.PrepayBalance,
                    AppliedBalance = x.AppliedBalance,
                    BatchNumber = x.BatchNumber,
                    Notes = x.Notes,
                    TimeStamp = x.TimeStamp                    
                }).FirstOrDefault();


                if (_data == null)
                {
                    return HttpNotFound();
                }
                return View(_data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerPaymentsEdit(CustomerPaymentViewModel model, Guid? id)
        {
            string sql = string.Format(" declare @bt datetime " +
                                      "   set @bt = (SELECT TimeStamp FROM [CustomerPaymentsEntry] WHERE ([PKID] = CONVERT(UNIQUEIDENTIFIER, '{0}'))) " +
                                      "   DECLARE @Total DECIMAL(18, 4) " +
                                      "   SET @Total = 0 " +
                                      "   SET @Total = ISNULL((SELECT SUM(PrepayAmount) FROM CustomerPaymentsEntry WHERE [ClientPKID] = convert(uniqueidentifier,'{1}') and (  TimeStamp between '1900-01-01' and @bt  -  0.0000001)), 0) " +
                                      "   - ISNULL((SELECT SUM(PrepayCredit) FROM CustomerPaymentsEntry WHERE [ClientPKID] = convert(uniqueidentifier,'{1}') and (  TimeStamp between '1900-01-01' and (@bt - 0.0000001) )), 0) " +
                                      "   + ISNULL( {2} ,0) - ISNULL( {3} , 0) " +
                                      "   /*SET @Total = ISNULL({3}, 0) - ISNULL({4}, 0) */" +
                                      "   UPDATE [CustomerPaymentsEntry]  " +
                                      "   SET  " +
                                      "       [CheckNumber]       = '{4}' ,  " +
                                      "       [PrepayAmount]      = {2} ,  " +
                                      "       [PrepayCredit]      = {3} ,  " +
                                      "       [PrepayBalance]     = ISNULL(@Total, 0) ,  " +
                                      "       [AppliedBalance]    = {5} ,  " +
                                      "       [BatchNumber]       = '{6}',  " +
                                      "       [Notes]             = '{7}' " +           
                                      "   WHERE PKID = CONVERT(UNIQUEIDENTIFIER, '{0}')  ", id, model.ClientPKID, 
                                            model.PrepayAmount, model.PrePayCredit, model.CheckNumber, model.AppliedBalance, model.BatchNumber, model.Notes );

            
            int rows = db.Database.ExecuteSqlCommand(sql);

            if (rows == 1)
            {
                TempData["type"] = "success";
                TempData["title"] = "Customer Payments";
                TempData["message"] = "The Customer Payments field(s) was updated successfully.";
            }

            using (var context = new salt_entities())
            {
                CustomerPaymentViewModel _data = context.CustomerPaymentsEntries.Where(x => x.pkid == id).Select(x => new CustomerPaymentViewModel
                {
                    PKID = x.pkid,
                    ClientPKID = x.ClientPKID,
                    CheckNumber = x.CheckNumber,
                    PrepayAmount = x.PrepayAmount,
                    PrePayCredit = x.PrepayCredit,
                    PrePayBalance = x.PrepayBalance,
                    AppliedBalance = x.AppliedBalance,
                    BatchNumber = x.BatchNumber,
                    Notes = x.Notes,
                    TimeStamp = x.TimeStamp
                }).FirstOrDefault();


                if (_data == null)
                {
                    return HttpNotFound();
                }
                return View(_data);
            }
        }

        #endregion

        #endregion

        #region CREATEs

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RefundStateCreate(BillingInformationViewModel model)
        {
            TempData["type"] = "";

            if (ModelState.IsValid)
            {
                using (var context = new salt_entities())
                {
                    StateRefundEntry staterefund = new StateRefundEntry();

                    staterefund.pkid = Guid.NewGuid();
                    staterefund.ClientPKID = model.ClientPKID;
                    staterefund.BatchDescription = model.RefundStateViewModel.BatchDescription;
                    staterefund.AmountSubmitted = model.RefundStateViewModel.AmountSubmitted;
                    staterefund.AmountPaid = model.RefundStateViewModel.AmountPaid;
                    staterefund.DeniedAdjusted = model.RefundStateViewModel.DeniedAdjusted;
                    staterefund.BalanceState = model.RefundStateViewModel.BalanceState;
                    staterefund.BatchNumber = model.RefundStateViewModel.BatchNumber;
                    staterefund.Notes = model.RefundStateViewModel.Notes;
                    staterefund.DateMTSMailed = model.RefundStateViewModel.DateMTSMailed;

                    context.StateRefundEntries.Add(staterefund);

                    int rows = context.SaveChanges();

                    if (rows == 1)
                    {
                        model.RefundStateViewModel = null;
                        TempData["type"] = "success";
                        TempData["title"] = "Refund State";
                        TempData["message"] = "The Refund State created successfully.";
                    }
                }
            }

            BillingInformationViewModel _data = GetBillingInformation.GetBillingInformationBy(model.ClientPKID);
            DropDownLoadHelper ddl = new DropDownLoadHelper();
            ddl.DropDownLoadBillingInformation();
            ViewBag.DropDownVendorNames = BillingInformationDropDowns.VendorNames;
            ViewBag.DropDownState = BillingInformationDropDowns.State;

            if (_data == null)
            {
                return HttpNotFound();
            }
            return View("Create", _data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FutureBenefitsCreate(BillingInformationViewModel model)
        {
            TempData["type"] = "";
            
            if (ModelState.IsValid)
            {
                using (var context = new salt_entities())
                {
                    FutureBenefitsEntry fb = new FutureBenefitsEntry();

                    fb.pkid = Guid.NewGuid();
                    fb.ClientPKID = model.ClientPKID;
                    fb.Installment = model.FutureBenefit.Installment;
                    fb.OriginalAmount = model.FutureBenefit.OrginalAmount;
                    fb.Adjusted = model.FutureBenefit.Adjusted;
                    fb.CollectionNote = model.FutureBenefit.CollectionNote;
                    fb.AmountRecovered = model.FutureBenefit.AmountRecovered;
                    fb.BatchNumber = model.FutureBenefit.BatchNumber;
                    fb.Notes = model.FutureBenefit.Notes;

                    context.FutureBenefitsEntries.Add(fb);

                    int rows = context.SaveChanges();

                    if (rows == 1)
                    {
                        model.RefundStateViewModel = null;
                        TempData["type"] = "success";
                        TempData["title"] = "Future Benefits";
                        TempData["message"] = "The Future Benefits created successfully.";
                    }
                }
            }

            BillingInformationViewModel _data = GetBillingInformation.GetBillingInformationBy(model.ClientPKID);
            DropDownLoadHelper ddl = new DropDownLoadHelper();
            ddl.DropDownLoadBillingInformation();
            ViewBag.DropDownVendorNames = BillingInformationDropDowns.VendorNames;
            ViewBag.DropDownState = BillingInformationDropDowns.State;

            if (_data == null)
            {
                return HttpNotFound();
            }
            return View("Create", _data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerPaymentsCreate(BillingInformationViewModel model)
        {
            TempData["type"] = "";

            if (ModelState.IsValid)
            {
                using (var context = new salt_entities())
                {
                    CustomerPaymentsEntry cp = new CustomerPaymentsEntry();

                    cp.pkid = Guid.NewGuid();
                    cp.ClientPKID = model.ClientPKID;
                    cp.CheckNumber = model.CustomerPayment.CheckNumber;
                    cp.PrepayAmount = model.CustomerPayment.PrepayAmount;
                    cp.PrepayCredit = model.CustomerPayment.PrePayCredit;
                    cp.PrepayBalance = model.CustomerPayment.PrePayBalance;
                    cp.AppliedBalance = model.CustomerPayment.AppliedBalance;
                    cp.BatchNumber = model.CustomerPayment.BatchNumber;
                    cp.Notes = model.CustomerPayment.Notes;                    

                    context.CustomerPaymentsEntries.Add(cp);

                    int rows = context.SaveChanges();

                    if (rows == 1)
                    {
                        model.RefundStateViewModel = null;
                        TempData["type"] = "success";
                        TempData["title"] = "Customer Payments";
                        TempData["message"] = "The Customer Payments created successfully.";
                    }
                }
            }

            BillingInformationViewModel _data = GetBillingInformation.GetBillingInformationBy(model.ClientPKID);
            DropDownLoadHelper ddl = new DropDownLoadHelper();
            ddl.DropDownLoadBillingInformation();
            ViewBag.DropDownVendorNames = BillingInformationDropDowns.VendorNames;
            ViewBag.DropDownState = BillingInformationDropDowns.State;

            if (_data == null)
            {
                return HttpNotFound();
            }
            return View("Create", _data);
        }


        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BillingInformationViewModel model, decimal? creditInterestRevived, Guid id)
        {
            DropDownLoadHelper ddl = new DropDownLoadHelper();

            if (ModelState.IsValid)
            {
                if (creditInterestRevived.HasValue)
                {
                    using (var context = new salt_entities())
                    {
                        CreditInterestReveived cr = new CreditInterestReveived(); 

                        string sql = string.Format("update CreditInterestReveived set number = '{0}' where clientpkid = '{1}'", creditInterestRevived, id);
                        int rows = db.Database.ExecuteSqlCommand(sql);

                        if (rows == 1)
                        {
                            TempData["type"] = "success";
                            TempData["title"] = "Refund Summary";
                            TempData["message"] = "The Credit Interest Reveived field was updated successfully.";
                        }
                    }
                }
                else
                {
                    using (var context = new salt_entities())
                    {
                        RefundEntry re = new RefundEntry();
                        CreditInterestReveived cr = new CreditInterestReveived();

                        re.PKID = Guid.NewGuid();
                        re.ClientPKID = model.ClientPKID;
                        re.VendorPKID = Guid.Parse(model.CompanyLegalName);
                        re.StateIdentified = Guid.Parse(model.State);
                        re.AmountIdentified = model.TaxAmountIdentified;
                        context.RefundEntries.Add(re);
                        
                        int rows = context.SaveChanges();

                        if (rows == 1)
                        {
                            TempData["type"] = "success";
                            TempData["title"] = "Refund Summary";
                            TempData["message"] = "The Refund Summary created successfully.";
                        }                         
                    }
                }
            }

            BillingInformationViewModel _data = GetBillingInformation.GetBillingInformationBy(model.ClientPKID);
            _data.CreditInterestRevieved = db.CreditInterestReveiveds.Where(x => x.ClientPKID == id).Select(x => x.Number).FirstOrDefault();

            if (_data == null)
            {
                return HttpNotFound();
            }

            ddl.DropDownLoadBillingInformation();
            ViewBag.DropDownVendorNames = BillingInformationDropDowns.VendorNames;
            ViewBag.DropDownState = BillingInformationDropDowns.State;

            return View(_data);
        }

        
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            BillingInformationViewModel _data = new BillingInformationViewModel();
            BillingInformationHelper binfoHelper = new BillingInformationHelper();

            _data = binfoHelper.BillingInformationEdit(id);

            if (_data == null)
            {
                return HttpNotFound();
            }
            return View(_data);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, decimal? balanceToQuickBooksEdit, decimal? creditInterestRevived)
        {
            if (ModelState.IsValid)
            {
                using (var context = new salt_entities())
                {
                    if (balanceToQuickBooksEdit != null)
                    {
                        string sql = string.Format(" IF NOT EXISTS(SELECT TOP 1 1 FROM ProgressPayments WHERE [ClientPKID] = '{0}') INSERT INTO ProgressPayments(ClientPKID, ProgressPayment) VALUES ('{0}', {1}) ELSE UPDATE ProgressPayments SET ProgressPayment = {1} WHERE ClientPKID = '{0}'", id, balanceToQuickBooksEdit);
                        int rows = db.Database.ExecuteSqlCommand(sql);

                        if (rows == 1)
                        {
                            TempData["type"] = "success";
                            TempData["title"] = "Customer Payments";
                            TempData["message"] = "The Balance To Quickbooks field was updated successfully.";
                        }

                    }
                    if (creditInterestRevived != null)
                    {
                        string sql = string.Format(" IF NOT EXISTS(SELECT TOP 1 1 FROM CreditInterestReveived WHERE [ClientPKID] = '{0}') INSERT INTO CreditInterestReveived(ClientPKID, Number) VALUES ('{0}', {1}) ELSE UPDATE CreditInterestReveived SET Number = {1} WHERE ClientPKID = '{0}' ", id, creditInterestRevived);
                        int rows = db.Database.ExecuteSqlCommand(sql);

                        if (rows == 1)
                        {
                            TempData["type"] = "success";
                            TempData["title"] = "Refund Summary";
                            TempData["message"] = "The Credit Interest Reveived field was updated successfully.";
                        }

                    }
                }                
            }

            //reload the data to the page
            BillingInformationViewModel _data = new BillingInformationViewModel();
            BillingInformationHelper binfoHelper = new BillingInformationHelper();

            _data = binfoHelper.BillingInformationEdit(id);
            if (_data == null)
            {
                return HttpNotFound();
            }
            ViewBag.Guid = null;
            return View(_data);
        }

        public ActionResult Delete(Guid? id, string gridName)
        {
            int rows = 0;
            string _clientpkid = string.Empty;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new salt_entities())
            {
                if (gridName == "RefundState")
                {
                    StateRefundEntry re = context.StateRefundEntries.Find(id);
                    if (re == null)
                    {
                        return HttpNotFound();
                    }
                    _clientpkid = re.ClientPKID.ToString();
                    context.Entry(re).State = EntityState.Deleted;
                    rows = context.SaveChanges();
                }
                else if (gridName == "RefundSummary")
                {
                    RefundEntry re = context.RefundEntries.Find(id);
                    if (re == null)
                    {
                        return HttpNotFound();
                    }
                    _clientpkid = re.ClientPKID.ToString();
                    context.Entry(re).State = EntityState.Deleted;
                    rows = context.SaveChanges();
                }
                else if (gridName == "FutureBenefits")
                {
                    FutureBenefitsEntry re = context.FutureBenefitsEntries.Find(id);
                    if (re == null)
                    {
                        return HttpNotFound();
                    }
                    _clientpkid = re.ClientPKID.ToString();
                    context.Entry(re).State = EntityState.Deleted;
                    rows = context.SaveChanges();
                }
                else if (gridName == "CustomerPayments")
                {
                    CustomerPaymentsEntry re = context.CustomerPaymentsEntries.Find(id);
                    if (re == null)
                    {
                        return HttpNotFound();
                    }
                    _clientpkid = re.ClientPKID.ToString();
                    context.Entry(re).State = EntityState.Deleted;
                    rows = context.SaveChanges();
                }

                if (rows == 1)
                {
                    TempData["type"] = "success";
                    TempData["title"] = "Delete";
                    TempData["message"] = string.Format("Item deleted successfully.");
                }
                return RedirectToAction("Edit", new { id = _clientpkid });                
            }            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
