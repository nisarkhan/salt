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
    
    public partial class CustomerPaymentsEntry
    {
        public System.Guid pkid { get; set; }
        public Nullable<System.Guid> ClientPKID { get; set; }
        public string CheckNumber { get; set; }
        public Nullable<decimal> PrepayAmount { get; set; }
        public Nullable<decimal> PrepayCredit { get; set; }
        public Nullable<decimal> PrepayBalance { get; set; }
        public Nullable<decimal> AppliedBalance { get; set; }
        public string BatchNumber { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
    }
}