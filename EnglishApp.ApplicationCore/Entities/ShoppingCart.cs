using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishApp.ApplicationCore.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            CartItems = new Collection<CartItem>();
        }
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }


        
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

    }
}
