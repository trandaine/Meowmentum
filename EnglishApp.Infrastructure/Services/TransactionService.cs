using EnglishApp.ApplicationCore.Entities;
using EnglishApp.ApplicationCore.Enums;
using EnglishApp.BusinessLogic.BaseClasses;
using EnglishApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly EnglishAppDbContext _context;
        private readonly ICustomerService _customerService;
        public TransactionService
            (
            EnglishAppDbContext context,
            ICustomerService customerService
            )
        {
            _context = context;
            _customerService = customerService;
        }


        public async Task<StatusCode> Checkout()
        {
            var statusCode = new StatusCode();
            try
            {
                var userId = _customerService.GetCurrentUserId();
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
                var shoppingCart = await _context.ShoppingCarts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Course)
                    .FirstOrDefaultAsync(c => c.UserId == userId);
                var transactionId = Guid.NewGuid().ToString();
                //var currency = await _context.Currencies.FirstOrDefaultAsync();
                int currencyId = 2; // Default to 2
                var position = _context.CourseCustomerDetails.Count() + 1;

                foreach (var item in shoppingCart.CartItems)
                {
                    // Create CustomerPaymentRecord
                    var paymentRecord = new CustomerPaymentRecord
                    {
                        Amount = item.Course.Price * item.Quantity,
                        PaymentMethod = PaymentMethodEnum.BankTransfer, // Default as per plan
                        PaymentStatus = PaymentStatusEnum.Success, // Default as per plan
                        TransactionId = transactionId,
                        DateCreated = DateTime.Now,
                        UserId = userId,
                        CustomerId = customer.Id,
                        CourseId = item.CourseId,
                        CurrencyId = currencyId
                    };
                    _context.CustomerPaymentRecords.Add(paymentRecord);

                    // Create CourseCustomerDetail
                    var courseDetail = new CourseCustomerDetail
                    {
                        CustomerId = customer.Id,
                        Amount = item.Course.Price * item.Quantity,
                        TransactionId = transactionId,
                        CourseId = item.CourseId,
                        DateCreated = DateTime.Now,
                        Position = position // Default
                    };
                    _context.CourseCustomerDetails.Add(courseDetail);
                }

                // Clear Cart
                _context.CartItems.RemoveRange(shoppingCart.CartItems);
                shoppingCart.TotalQuantity = 0;
                shoppingCart.TotalAmount = 0;

                await _context.SaveChangesAsync();
                statusCode.SetSuccess("Checkout completed successfully.");
                return statusCode;

            }
            catch (Exception ex)
            {
                statusCode.SetInternalError($"An error occurred during checkout: {ex.Message}");
                return statusCode;
            }
        }
    }
}
