using EnglishApp.ApplicationCore.Enums;
using System.ComponentModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class CustomerPaymentRecord
    {
        public CustomerPaymentRecord()
        {

        }
        public int Id { get; set; }
        public decimal Amount { get; set; }
        [Description("Payment Method: CreditCard, PayPal, BankTransfer, Cash, MobilePayment, Other")]
        public PaymentMethodEnum PaymentMethod { get; set; }
        [Description("Payment Status: Pending, Completed, Failed, Refunded, Cancelled (Updated on 27/9/25)")]
        public PaymentStatusEnum PaymentStatus { get; set; }
        public string? TransactionId { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }


        #region Navigational Property
        [Description("Id của user trong bảng AspNetUsers")]
        public string UserId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        #endregion

    }
}
