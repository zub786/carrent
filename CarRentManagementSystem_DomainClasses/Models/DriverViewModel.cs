using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentManagementSystem_DomainClasses.Models
{
    public class DriverViewModel
    {
        public int Id { get; set; }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required, Display(Name = "CNIC(ID Card #)")]
        public string CNIC { get; set; }
        public string Address { get; set; }
        [Required, Display(Name = "Contact No")]
        public string ContactNo { get; set; }
        public string LicenseType { get; set; }
        public string LicenseNo { get; set; }
        [Required]
        public string City { get; set; }
        [Display(Name = "Total Experience In Years")]
        public Nullable<int> ExperienceInYears { get; set; }
        public bool IsActive { get; set; }
    }
}