using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace salt_web.Models
{
    public class EmployeeViewModel
    {
        public aspnet_Users AspNetUsers { get; set; }
        public aspnet_Membership AspNetMembership { get; set; }
         
        public EmployeeViewModel()
        {
            AspNetUsers = new aspnet_Users();
            AspNetMembership = new aspnet_Membership();
        }


        public Guid ApplicationId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage = "The First Name field is required.")]        
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The Last Name field is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The Email Address field is required.")]
        public string EmailAddress { get; set; }
        [Required]
        public string Initials { get; set; }
        public string OldID { get; set; }
        public DateTime? DOH { get; set; }
        public DateTime? LDE { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; }
        public Guid Manager { get; set; }
        public Guid SecManager { get; set; }
        public Guid Trainer { get; set; }
        public bool TypeManager { get; set; }
        public bool TypeAuditor { get; set; }
        public bool TypeBDR { get; set; }
        public bool TypeTrainer { get; set; }
        public bool TypeAcctRep { get; set; }
        public bool TypeColRep { get; set; }
        [Required]
        public string Password { get; set; }
        [Required(ErrorMessage = "The Password Question field is required.")]
        public string PasswordQuestion { get; set; }
        [Required(ErrorMessage = "The Password Answer field is required.")]
        public string PasswordAnswer { get; set; }


    }
}