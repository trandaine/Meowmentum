using EnglishApp.ApplicationCore.Enums;
using EnglishApp.ApplicationCore.IdentityEntities;
using EnglishApp.BusinessLogic.Interfaces;
using EnglishApp.Infrastructure;
using EnglishApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EnglishApp.MVC.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ITransactionService _transactionService;
        private readonly UserManager<EnglishAppIdentityUser> _userManager;

        public TransactionController(
            EnglishAppDbContext context,
            ICustomerService customerService,
            UserManager<EnglishAppIdentityUser> userManager,
            ITransactionService transactionService
            )
        {
            _transactionService = transactionService;
            _customerService = customerService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Checkout()
        {

            var userId = _customerService.GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            //var user = await _userManager.FindByIdAsync(userId);
            //var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);

            //if (customer == null)
            //{
            //    // Handle case where customer record doesn't exist for the user
            //    return RedirectToAction("Index", "Home");
            //}


            //var shoppingCart = await _context.ShoppingCarts
            //    .Include(c => c.CartItems)
            //    .ThenInclude(ci => ci.Course)
            //    .FirstOrDefaultAsync(c => c.UserId == userId);

            //if (shoppingCart == null || !shoppingCart.CartItems.Any())
            //{
            //    return RedirectToAction("Index", "ShoppingCarts");
            //}

            //var transactionId = Guid.NewGuid().ToString();
            ////var currency = await _context.Currencies.FirstOrDefaultAsync();
            //int currencyId = 2; // Default to 2
            //var position = _context.CourseCustomerDetails.Count() + 1;

            //foreach (var item in shoppingCart.CartItems)
            //{
            //    // Create CustomerPaymentRecord
            //    var paymentRecord = new CustomerPaymentRecord
            //    {
            //        Amount = item.Course.Price * item.Quantity,
            //        PaymentMethod = PaymentMethodEnum.BankTransfer, // Default as per plan
            //        PaymentStatus = PaymentStatusEnum.Success, // Default as per plan
            //        TransactionId = transactionId,
            //        DateCreated = DateTime.Now,
            //        UserId = userId,
            //        CustomerId = customer.Id,
            //        CourseId = item.CourseId,
            //        CurrencyId = currencyId
            //    };
            //    _context.CustomerPaymentRecords.Add(paymentRecord);

            //    // Create CourseCustomerDetail
            //    var courseDetail = new CourseCustomerDetail
            //    {
            //        CustomerId = customer.Id,
            //        Amount = item.Course.Price * item.Quantity,
            //        TransactionId = transactionId,
            //        CourseId = item.CourseId,
            //        DateCreated = DateTime.Now,
            //        Position = position // Default
            //    };
            //    _context.CourseCustomerDetails.Add(courseDetail);
            //}

            //// Clear Cart
            //_context.CartItems.RemoveRange(shoppingCart.CartItems);
            //shoppingCart.TotalQuantity = 0;
            //shoppingCart.TotalAmount = 0;

            //await _context.SaveChangesAsync();
            var transactionStatusCode = await _transactionService.Checkout();
            if (transactionStatusCode.Code == StatusCodeEnum.Success)
            {
                //return RedirectToAction("Index", "Home");
                return View();

            }
            else
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = transactionStatusCode.Message
                });
            }


            //return View("Error", new ErrorViewModel { 
            //    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            //    Message = ex.Message
            //});

        }
    }
}
