using EnglishApp.ApplicationCore.Entities;
using EnglishApp.BusinessLogic.BaseClasses;
using EnglishApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly EnglishAppDbContext _context;

        private readonly ICustomerService _customerService;

        public CartService(
            EnglishAppDbContext context,
            ICustomerService customerService
            )
        {
            _context = context;
            _customerService = customerService;
        }

        public async Task<StatusCode> AddToCart(int productId, int quantity = 1)
        {
            var statusCode = new StatusCode();
            try
            {
                var userId = _customerService.GetCurrentUserId();
                var shoppingCart = await _context.ShoppingCarts
                    .Include(c => c.CartItems)
                    .SingleOrDefaultAsync(c => c.UserId == userId);
                var customerId = await _context.Customers
                    .Where(c => c.UserId == userId)
                    .Select(c => c.Id)
                    .SingleOrDefaultAsync();

                // Nếu giỏ hàng chưa tồn tại cho người dùng, tạo một giỏ hàng mới cùng với UserId và lưu vào database
                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCart
                    {
                        UserId = userId,
                        DateCreated = DateTime.UtcNow,
                        CustomerId = customerId
                    };
                    _context.ShoppingCarts.Add(shoppingCart);
                    await _context.SaveChangesAsync();
                }

                var item = shoppingCart.CartItems.SingleOrDefault(i => i.CourseId == productId);

                // Nếu mặt hàng đã tồn tại trong giỏ hàng, chỉ cần cập nhật sổ lượng và lưu vào database
                if (item != null)
                {
                    item.Quantity += quantity;
                    item.DateUpdated = DateTime.UtcNow;
                    _context.CartItems.Update(item);
                }
                // Nếu mặt hàng chưa tồn tại trong giỏ hàng, tạo một CartItem mới và lưu vào database
                else
                {
                    var countItem = await _context.CartItems.CountAsync();
                    var coursePrice = await _context.Courses
                        .Where(c => c.Id == productId)
                        .Select(c => c.Price)
                        .SingleOrDefaultAsync();
                    var courseImage = await _context.Courses
                        .Where(c => c.Id == productId)
                        .Select(c => c.Thumbnail)
                        .SingleOrDefaultAsync();
                    var courseName = await _context.Courses
                        .Where(c => c.Id == productId)
                        .Select(c => c.Name)
                        .SingleOrDefaultAsync();

                    var newCartItem = new CartItem
                    {
                        ShoppingCartId = shoppingCart.Id,
                        CourseId = productId,
                        Name = courseName,
                        Quantity = quantity,
                        Price = coursePrice,
                        DateCreated = DateTime.UtcNow,
                        Image = courseImage,
                        Position = countItem + 1
                    };
                    _context.CartItems.Add(newCartItem);
                }
                await _context.SaveChangesAsync();
                await UpdateShoppingCartAsync();
                statusCode.SetSuccess("Tạo khóa học thành công");
                return statusCode;

            }

            catch (Exception)
            {
                statusCode.SetInternalError("Tạo khóa học thất bại");
            }
            return null!;
        }

        /// <summary>
        /// Updates the TotalQuantity and TotalAmount of the ShoppingCart for the current user.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task UpdateShoppingCartAsync()
        {
            var userId = _customerService.GetCurrentUserId();
            var shoppingCart = await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (shoppingCart != null)
            {
                shoppingCart.TotalQuantity = shoppingCart.CartItems.Sum(ci => ci.Quantity);
                shoppingCart.TotalAmount = shoppingCart.CartItems.Sum(ci => ci.Price * ci.Quantity);
                _context.ShoppingCarts.Update(shoppingCart);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<StatusCode> Delete(int cartItemId)
        {
            var statusCode = new StatusCode();
            var cartItem = await _context.CartItems.SingleOrDefaultAsync(c => c.Id == cartItemId);
            try
            {
                if (cartItem != null)
                {
                    _context.CartItems.Remove(cartItem);
                }
                await _context.SaveChangesAsync();
                await UpdateShoppingCartAsync();
                statusCode.SetSuccess("Xóa mặt hàng khỏi giỏ hàng thành công");
                return statusCode;
            }
            catch
            {
                statusCode.SetInternalError("Xóa mặt hàng khỏi giỏ hàng thất bại");
            }
            return null!;

        }


        public async Task<StatusCode> Increase(int cartItemId)
        {
            var statusCode = new StatusCode();
            var cartItem = await _context.CartItems.SingleOrDefaultAsync(c => c.Id == cartItemId);
            try
            {
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                    cartItem.DateUpdated = DateTime.UtcNow;

                    _context.CartItems.Update(cartItem);
                }
                await _context.SaveChangesAsync();
                await UpdateShoppingCartAsync();
                statusCode.SetSuccess("Tăng số lượng mặt hàng thành công");
                return statusCode;
            }
            catch
            {
                statusCode.SetInternalError("Tăng số lượng mặt hàng thất bại");
            }

            return null!;
        }

        public async Task<StatusCode> Decrease(int cartItemId)
        {
            var statusCode = new StatusCode();
            var cartItem = await _context.CartItems.SingleOrDefaultAsync(c => c.Id == cartItemId);
            try
            {
                if (cartItem != null && cartItem.Quantity > 1)
                {
                    cartItem.Quantity -= 1;
                    cartItem.DateUpdated = DateTime.UtcNow;
                    _context.CartItems.Update(cartItem);
                    await _context.SaveChangesAsync();
                    await UpdateShoppingCartAsync();
                    statusCode.SetSuccess("Giảm số lượng mặt hàng thành công");
                    return statusCode;
                }
                else if (cartItem != null && cartItem.Quantity == 1)
                {
                    // Nếu số lượng hiện tại là 1 thì xóa mục khỏi giỏ hàng
                    //_context.CartItems.Remove(cartItem);
                    await Delete(cartItemId);
                    await _context.SaveChangesAsync();
                    //await UpdateShoppingCartAsync();
                    statusCode.SetSuccess("Xóa mặt hàng khỏi giỏ hàng thành công");
                    return statusCode;
                }
            }
            catch
            {
                statusCode.SetInternalError("Giảm số lượng mặt hàng thất bại");
            }
            // Nếu số lượng hiện tại lớn hơn 1 thì mới giảm
            return null!;
        }
    }





}

