using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentManagementSystem_DomainClasses.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy_FK { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy_FK { get; set; }
        [Required, Display(Name = "User Name")]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public Nullable<bool> IsActive
        {
            get; set;
        }
    }
}