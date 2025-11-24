using EnglishApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishApp.MVC.ViewComponents
{
    public class CartCount : ViewComponent
    {
        private readonly IShoppingCartService _shoppingCartService;

        public CartCount(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cartCount = await _shoppingCartService.GetCountItemsInShoppingCart();
            return View("Default", cartCount);
        }
    }
}
