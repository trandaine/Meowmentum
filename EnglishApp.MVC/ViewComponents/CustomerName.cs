using EnglishApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishApp.MVC.ViewComponents
{
    public class CustomerName : ViewComponent
    {
        private readonly ICustomerService _customerService;

        public CustomerName(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IViewComponentResult Invoke()
        {
            var customerName = _customerService.GetCurrentCustomerName();
            return View("Default", customerName);
        }
    }
}
