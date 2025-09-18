//Transaction.cs → Separates payment transactions from general payment details.
using System;

namespace P1WEBMVC.Models.DomainModels;


public class Transaction
{
    public Guid TransactionId { get; set; }
    public Guid AppointmentId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid AdminAccountId { get; set; }  // Your account to receive commission
    public decimal TotalAmount { get; set; }
    public decimal DoctorEarnings { get; set; }  // 90% of TotalAmount
    public decimal CommissionAmount { get; set; }  // 10% deduction
    public DateTime TransactionDate { get; set; }
}

