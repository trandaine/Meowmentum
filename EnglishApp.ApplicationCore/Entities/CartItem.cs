namespace EnglishApp.ApplicationCore.Entities
{
    public class CartItem
    {
        public CartItem()
        {
            
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Position { get; set; }
        // Navigation properties

        public int ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

    }
}



