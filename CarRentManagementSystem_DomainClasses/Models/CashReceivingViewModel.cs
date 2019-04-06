using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentManagementSystem_DomainClasses.Models
{
    public class CashReceivingViewModel
    {
        public int? Id { get; set; }
        public long BookingId_FK { get; set; }
        public string PaymentType { get; set; }
        public int ReceivedAmount { get; set; }
        public int RemainingAmount { get; set; }
        public System.DateTime ReceivedOn { get; set; }
        public int ReceivedBy_FK { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.DateTime> ModifiedBy_FK { get; set; }
    }
}