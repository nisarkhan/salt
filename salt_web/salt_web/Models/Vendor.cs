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
    
    public partial class Vendor
    {
        public System.Guid VendorPKID { get; set; }
        public string VendorCode { get; set; }
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
        public bool TaxID { get; set; }
        public bool MultiTaxID { get; set; }
        public bool BillingAddressSame { get; set; }
        public bool BillingContactSame { get; set; }
        public string ClientLocation { get; set; }
    }
}