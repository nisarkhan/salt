using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace salt_web.Models.User
{
    public class RolesViewModel
    {
        [Required(ErrorMessage = "The ApplicationId field is required.")]
        public System.Guid ApplicationId { get; set; }
        [Required(ErrorMessage = "The RoleId field is required.")]
        public System.Guid RoleId { get; set; }
        [Required(ErrorMessage = "The RoleName field is required.")]
        public string RoleName { get; set; }
        [Required(ErrorMessage = "The Lowered Role Name field is required.")]
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }
    }
}