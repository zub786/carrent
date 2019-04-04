using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentManagementSystem_DomainClasses.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Number { get; set; }
        public string Model { get; set; }
        public string RegistredIn { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
    }
}