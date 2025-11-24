using EnglishApp.ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace EnglishApp.BusinessLogic.DTOs.ShoppingCarts
{

    [Bind("Id,Name,Quantity,Image,Price,DateCreated,DateUpdated,Position,ShoppingCart,Course,Amount")]
    public class CartItemDTO
    {
        //public int ProductId { get; set; }
        //public string ProductName { get; set; } = string.Empty;
        //public string? Image { get; set; }
        //public decimal Price { get; set; }
        //public int Quantity { get; set; }
        public decimal Amount => Price * Quantity;
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public string FilePath =>
                !string.IsNullOrEmpty(Image)
                    ? "/media/course_images/" + Image
                    : "/media/default/image.png";

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
