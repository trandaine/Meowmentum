using EnglishApp.BusinessLogic.BaseClasses;

namespace EnglishApp.BusinessLogic.Interfaces
{
    public interface ICartService
    {
        Task<StatusCode> AddToCart(int productId, int quantity = 1);
        Task<StatusCode> Delete(int cartItemId);
        Task<StatusCode> Increase(int cartItemId);
        Task<StatusCode> Decrease(int cartItemId);
    }
}
