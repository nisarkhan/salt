using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace salt_web.Models
{
    public class CollectionHeaderViewModel
    {
        public string JobCode { get; set; }
        public string Acct { get; set; }
        public string Rep { get; set; }
        public string Auditor { get; set; }
        public string Client { get; set; }
        public string TaxID { get; set; }
        public DateTime? CollectionsReportDate { get; set; }
        public DateTime? CollectionsDateRecvd { get; set; }
        public DateTime? CollectionsDateAssigned { get; set; }
        public string CollectionsAssignedTo { get; set; }
        public DateTime? CollectionsDatePktRecvd { get; set; }
        public string CollectionsNotes { get; set; }
        public DateTime? LastCallDate { get; set; }
    }

    public class CollectionDetailViewModel
    {
        public Guid PKID { get; set; }
        public string Date { get; set; }
        public string Rep { get; set; }
        public string Notes { get; set; }
    }

    public class CollectionViewModel
    {
        public IEnumerable<SelectListItem> DropDownTypes { get; set; }
        public List<CollectionDetailViewModel> CollectionDetailViewModel { get; set; }

        public CollectionHeaderViewModel CollectionHeaderViewModel { get; set; }

        public Guid EntryPKID { get; set; }
        public Guid PKID { get; set; }
        public Guid VendorPKID { get; set; }
        public string Vendor { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? AmountIdentified { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? AmountRecovered { get; set; }
        public Guid? Type { get; set; }
        public string SelectedType { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? AmountDue { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? RecoverType { get; set; }

    }
}