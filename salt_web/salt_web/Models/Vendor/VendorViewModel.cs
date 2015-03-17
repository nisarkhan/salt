using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace salt_web.Models
{
    public class VendorViewModel
    {
        ////public Guid VendorId { get; set; }
        ////public string VendorCode { get; set; }
        ////public string CompanyLegalName { get; set; }
        ////public string PhoneNumber { get; set; }

        ////public string TotalRows { get; set; }
        ////public string TotalDisplayRows { get; set; }

        public System.Guid VendorPKID { get; set; }
        [Required(ErrorMessage = "The Vendor Code field is required.")]
        public string VendorCode { get; set; }
        [Required(ErrorMessage = "The Company Legal Name field is required.")]
        public string CompanyLegalName { get; set; }
        public string DBA { get; set; }
        public string MrMs { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactTitle { get; set; }
        [Required(ErrorMessage = "The Phone Number field is required.")]
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Notes { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool TaxID { get; set; }
        public bool MultiTaxID { get; set; }
        public bool BillingAddressSame { get; set; }
        public bool BillingContactSame { get; set; }
        public string ClientLocation { get; set; }


    }
}