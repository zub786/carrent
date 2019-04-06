using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentManagementSystem_DomainClasses.Models
{
    public class BookingCashReceivingViewModel
    {
        public long BookingNo { get; set; }
        public decimal TotalReceived { get; set; }
        public string CustomerName { get; set; }
        public int BookingAmount { get; set; }
        public string PaymentType { get; set; }
        public bool isRecordNotFound { get; set; }
        public int EditReceivingId { get; set; }
        public decimal NewReceiveAmount { get; set; }
        public IEnumerable<object> CashDetails { get; set; }
    }
}