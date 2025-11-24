using EnglishApp.ApplicationCore.Entities;
using EnglishApp.BusinessLogic.BaseClasses;
using EnglishApp.BusinessLogic.DTOs.ShoppingCarts;
using EnglishApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EnglishApp.Infrastructure.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly EnglishAppDbContext _context;
    //private readonly ICustomerService _customerService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public ShoppingCartService(EnglishAppDbContext context,
        IHttpContextAccessor httpContextAccessor
        //ICustomerService customerService
        )
    {
        _context = context;
        //_customerService = customerService;
        _httpContextAccessor = httpContextAccessor;
    }


    /// <summary>
    /// Hàm lấy danh sách các mục trong giỏ hàng của người dùng theo userId
    /// </summary>
    /// <param name="userId">truyền vào userId dạng string</param>
    /// <returns>trả về list các mục trong giỏ hàng</returns>
    public async Task<CartItemDTO[]> GetCartItemsByUserId(string userId)
    {
        try
        {
            var cartItems = await _context.CartItems
            .Include(ci => ci.Course)
            .Where(ci => ci.ShoppingCart.UserId.Equals(userId))
            .Select(cart => new CartItemDTO
            {
                Id = cart.Id,
                CourseId = cart.CourseId,
                Quantity = cart.Quantity,
                Image = !string.IsNullOrEmpty(cart.Course.Thumbnail)
                            ? "/media/course_images/" + cart.Course.Thumbnail
                            : "/media/default/image.png",
                Name = cart.Course.Name,
                Price = cart.Course.Price,
                
            })
            .ToArrayAsync();
            return cartItems;
        }
        catch (Exception)
        {

            throw;
        }
        //var userId = _customerService.GetCurrentUserId();

    }



    /// <summary>
    /// Hàm lấy tổng số lượng sản phẩm trong giỏ hàng của người dùng hiện tại
    /// </summary>
    /// <returns>Trả về số lượng sản phẩm hiện có ở dạng string</returns>
    public async Task<string> GetCountItemsInShoppingCart()
    {
        var statusCode = new StatusCode();
        try
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var updatedCart = await _context.ShoppingCarts
                //.AsNoTracking()
                //.SingleOrDefaultAsync(c => c.UserId == userId);
                .Where(c => c.UserId.Equals(userId))
                .Select(c => c.TotalQuantity)
                .SingleOrDefaultAsync();
            //if (updatedCart == null)
            //{
            //    return "0";
            //}
            // Assuming you want the total quantity of items
            //var totalQuantity = updatedCart.CartItems?.Sum(ci => ci.Quantity) ?? 0;
            //var totalQuantity = updatedCart;
            return updatedCart.ToString();
        }
        catch
        {
            statusCode.SetInternalError("Lỗi khi lấy số lượng sản phẩm trong giỏ hàng");
            return "0";
        }
    }
}