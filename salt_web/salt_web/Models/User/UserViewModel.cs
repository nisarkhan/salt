using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace salt_web.Models.User
{
    public class UserViewModel
    {
        public aspnet_Users AspNetUsers { get; set; }
        public aspnet_Membership AspNetMembership { get; set; }
        public aspnet_Roles AspnetRoles { get; set; }
        public UserViewModel()
        {
            AspNetUsers = new aspnet_Users();
            AspNetMembership = new aspnet_Membership();
            AspnetRoles = new aspnet_Roles();
        
        }

        public Guid UserId { get; set; }
        [Required(ErrorMessage = "The User Name field is required.")]
        public string UserName { get; set; }
        


        //[DataType(DataType.Password)]
        //[RegularExpression(@"^.{7,}$", ErrorMessage = "Minimum 7 characters required")]
        //public string Password { get; set; }
        //[Required(ErrorMessage = "The Confirm Password field is required.")]

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "The Password field is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string PasswordSalt { get; set; }




        [Required(ErrorMessage = "The Security Question field is required.")]
        public string PasswordQuestion { get; set; }
        [Required(ErrorMessage = "The Security Answer field is required.")]
        public string PasswordAnswer { get; set; }

        [Required(ErrorMessage = "The Group field is required.")]       
        public string RoleName { get; set; } //RoleName

        //[Required(ErrorMessage = "The FirstName field is required.")]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "The LastName field is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; }
         
    }
}