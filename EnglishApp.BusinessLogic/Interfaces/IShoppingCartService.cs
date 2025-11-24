using EnglishApp.ApplicationCore.Entities;
using EnglishApp.BusinessLogic.DTOs.ShoppingCarts;

namespace EnglishApp.BusinessLogic.Interfaces
{
    public interface IShoppingCartService
    {
        Task<string> GetCountItemsInShoppingCart();
        //Task<List<CartItem>> GetCartItemsByUserId(string userId);
        Task<CartItemDTO[]> GetCartItemsByUserId(string userId);
    }
}
