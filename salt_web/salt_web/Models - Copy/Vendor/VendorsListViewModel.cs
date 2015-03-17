using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace salt_web.Models
{
    public class VendorsListViewModel
    {
        public Guid VendorId { get; set; }
        public string VendorCode { get; set; }
        public string CompanyLegalName { get; set; }
        public string PhoneNumber { get; set; }

    }
}