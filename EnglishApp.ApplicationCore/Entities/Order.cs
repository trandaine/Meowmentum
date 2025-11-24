using System.Collections.ObjectModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class Order
    {
        public Order()
        {
            OrderProductDetails = new Collection<OrderProductDetail>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }

        public DateTime DateCreated { get; set; }
        public decimal Amount { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? TransactionId { get; set; }

        public string? Note { get; set; }

        //List of Products
        public virtual ICollection<OrderProductDetail> OrderProductDetails { get; set; }
    }
}
