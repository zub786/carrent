//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRentManagementSystem_DAL.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CashReceiving
    {
        public int Id { get; set; }
        public long BookingId_FK { get; set; }
        public int PaymentType { get; set; }
        public int ReceivedAmount { get; set; }
        public int RemainingAmount { get; set; }
        public System.DateTime ReceivedOn { get; set; }
        public int ReceivedBy_FK { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.DateTime> ModifiedBy_FK { get; set; }
    }
}