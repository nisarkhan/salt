using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace salt_web.Models
{
    public class ClientUserListViewModel
    {
        public int CustomerID { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ClientCode { get; set; }
        public string CompanyLegalName { get; set; }
        public string PhoneNumber { get; set; }

    }
}