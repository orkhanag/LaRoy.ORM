﻿using System.ComponentModel.DataAnnotations;

namespace Test.DTO_s
{
    public class DailyCustomerPayments
    {
        [Key]
        public string? PinCode { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? MobileNumber { get; set; }
        public int RemainingDays { get; set; }
        public int PayableAmount { get; set; }
        public bool DelayStatus { get; set; }
        public DateTime? SendDate { get; set; }
    }
}
