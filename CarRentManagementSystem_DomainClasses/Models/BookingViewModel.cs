using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentManagementSystem_DomainClasses.Models
{
    public class BookingViewModel
    {
        public long Id { get; set; }
        [Required, Display(Name = "Booking Date")]
        public System.DateTime BookingFromDate { get; set; }
        [Required, Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required, Display(Name = "Vehicle Name")]
        public int VehicleId_Fk { get; set; }
        [Required, Display(Name = "Start Time")]
        public System.DateTime StartTime { get; set; }
        [Display(Name = "Close Time")]
        public Nullable<System.DateTime> EndTime { get; set; }
        [Required, Display(Name = "From Location")]
        public string FromLocation { get; set; }
        [Display(Name = "To Location")]
        public string ToLocation { get; set; }
        [Required, Display(Name = "Advance Amount")]
        public int AdvanceAmount { get; set; }
        public int BookedBy_FK { get; set; }
        public string Shift { get; set; }
        [Required, Display(Name = "Client Address")]
        public string Address { get; set; }
        [Required, Display(Name = "Client Contact No(Phone)")]
        public string Phone { get; set; }
        [Required, Display(Name = "Client Contact No(Mobile)")]
        public string Mobile { get; set; }
        [Required, Display(Name = "Client CNIC No")]
        public string CNIC { get; set; }
        [Required, Display(Name = "Mileage Start")]
        public string MileageStart { get; set; }
        [Display(Name = "Mileage End")]
        public string MileageEnd { get; set; }
        [Display(Name = "Fuel Litter(s)")]
        public Nullable<int> FuleLtr { get; set; }
        [Required, Display(Name = "First Driver Name")]
        public int Driver1Id_FK { get; set; }
        [Display(Name = "Second Driver Name")]
        public Nullable<int> Driver2Id_FK { get; set; }
        [Required, Display(Name = "Rent Amount")]
        public int RentAmount { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        [Required, Display(Name = "Entry Date")]
        public Nullable<System.DateTime> EntryDate { get; set; }
        [Required, Display(Name = "Booking To Date")]
        public Nullable<System.DateTime> BookingToDate { get; set; }
        [Display(Name = "Duration")]
        public int Duration { get; set; }
    }
}