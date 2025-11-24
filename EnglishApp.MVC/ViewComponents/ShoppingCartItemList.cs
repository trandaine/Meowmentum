using EnglishApp.BusinessLogic.DTOs.ShoppingCarts;
using EnglishApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishApp.MVC.ViewComponents
{
    public class ShoppingCartItemList : ViewComponent
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICustomerService _customerService;
        public ShoppingCartItemList(IShoppingCartService shoppingCartService, ICustomerService customerService)
        {
            _customerService = customerService;
            _shoppingCartService = shoppingCartService;
        }

        //public async Task<IViewComponentResult> InvokeAsync()
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _customerService.GetCurrentUserId();
            if (userId == null)
            {
                // Pass an empty list instead of a string to match the expected model type
                return View("Default", new List<CartItemDTO>());
            }

            var items = await _shoppingCartService.GetCartItemsByUserId(userId);
            return View("Default", items);
        }
    }
}
