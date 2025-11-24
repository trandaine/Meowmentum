using EnglishApp.ApplicationCore.Enums;
using EnglishApp.BusinessLogic.BaseClasses;
using EnglishApp.BusinessLogic.Interfaces;
using EnglishApp.Infrastructure;
using EnglishApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.MVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly EnglishAppDbContext _context;
        //private readonly UserManager<EnglishAppIdentityUser> _userManager;
        private readonly ICustomerService _customerService;
        private readonly ICartService _cartService;
        //private readonly IShoppingCartService _shoppingCartService;
        //private readonly IHttpContextAccessor _httpContextAccessor;


        public CartController(
            EnglishAppDbContext context,
            //IHttpContextAccessor httpContextAccessor,
            ICustomerService customerService,
            ICartService cartService
            //IShoppingCartService shoppingCartService,
            //UserManager<EnglishAppIdentityUser> userManager
            )
        {
            //_shoppingCartService = shoppingCartService;
            //_httpContextAccessor = httpContextAccessor;
            _context = context;
            _customerService = customerService;
            _cartService = cartService;
            //_userManager = userManager;
        }



        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // POST: CartController/Create
        /// <summary>
        /// Hàm thêm sản phẩm vào giỏ hàng và lưu vào database.
        /// </summary>
        /// <param name="productId">Mã sản phẩm nhận vào (CourseId)</param>
        /// <param name="quantity">Số lượng sản phẩm (Mặc định là 1)</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var statusCode = new StatusCode();
            var result = await _cartService.AddToCart(productId, quantity);
            if (result.Code == StatusCodeEnum.Success)
            {
                return Ok();
            }
            else
            {
                statusCode.SetInternalError("Thêm vào giỏ hàng thất bại");
                return BadRequest();
            }
            //try
            //{
            //    var userId = _customerService.GetCurrentUserId();
            //    var shoppingCart = await _context.ShoppingCarts
            //        .Include(c => c.CartItems)
            //        .SingleOrDefaultAsync(c => c.UserId == userId);
            //    var customerId = await _context.Customers
            //        .Where(c => c.UserId == userId)
            //        .Select(c => c.Id)
            //        .SingleOrDefaultAsync();

            //    // Nếu giỏ hàng chưa tồn tại cho người dùng, tạo một giỏ hàng mới cùng với UserId và lưu vào database
            //    if (shoppingCart == null)
            //    {
            //        shoppingCart = new ShoppingCart
            //        {
            //            UserId = userId,
            //            DateCreated = DateTime.UtcNow,
            //            CustomerId = customerId
            //        };
            //        _context.ShoppingCarts.Add(shoppingCart);
            //        await _context.SaveChangesAsync();
            //    }

            //    var item = shoppingCart.CartItems.FirstOrDefault(i => i.CourseId == productId);

            //    // Nếu mặt hàng đã tồn tại trong giỏ hàng, chỉ cần cập nhật sổ lượng và lưu vào database
            //    if (item != null)
            //    {
            //        item.Quantity += quantity;
            //        item.DateUpdated = DateTime.UtcNow;
            //        _context.CartItems.Update(item);
            //    }
            //    // Nếu mặt hàng chưa tồn tại trong giỏ hàng, tạo một CartItem mới và lưu vào database
            //    else
            //    {
            //        var countItem = await _context.CartItems.CountAsync();
            //        var coursePrice = await _context.Courses
            //            .Where(c => c.Id == productId)
            //            .Select(c => c.Price)
            //            .SingleOrDefaultAsync();
            //        var courseImage = await _context.Courses
            //            .Where(c => c.Id == productId)
            //            .Select(c => c.Thumbnail)
            //            .SingleOrDefaultAsync();
            //        var courseName = await _context.Courses
            //            .Where(c => c.Id == productId)
            //            .Select(c => c.Name)
            //            .SingleOrDefaultAsync();

            //        var newCartItem = new CartItem
            //        {
            //            ShoppingCartId = shoppingCart.Id,
            //            CourseId = productId,
            //            Name = courseName,
            //            Quantity = quantity,
            //            Price = coursePrice,
            //            DateCreated = DateTime.UtcNow,
            //            Image = courseImage,
            //            Position = countItem + 1
            //        };
            //        _context.CartItems.Add(newCartItem);
            //    }
            //    await _context.SaveChangesAsync();
            //    await UpdateShoppingCartAsync();

            //    return Ok();
            //}

            //catch (Exception)
            //{
            //    TempData["Message"] = "Thất bại";
            //    return Content("0");
            //}


            //return RedirectToAction("Index");
            //return Ok();
        }



        public async Task<IActionResult> IncreaseItemQuantity(int cartItemId)
        {
            var statusCode = new StatusCode();
            var result = await _cartService.Increase(cartItemId);
            if (result.Code == StatusCodeEnum.Success)
            {
                return Ok();
            }
            else
            {
                statusCode.SetInternalError("Tăng số lượng mặt hàng thất bại");
                return BadRequest();
            }

            //var cartItem = await _context.CartItems.FindAsync(cartItemId);
            //if (cartItem != null)
            //{
            //    cartItem.Quantity += 1;
            //    cartItem.DateUpdated = DateTime.UtcNow;
            //    _context.CartItems.Update(cartItem);
            //    await _context.SaveChangesAsync();
            //    await UpdateShoppingCartAsync();
            //    return Ok();
            //}
            //return BadRequest();
        }

        public async Task<IActionResult> DecreaseItemQuantity(int cartItemId)
        {
            var statusCode = new StatusCode();
            var result = await _cartService.Decrease(cartItemId);
            if (result.Code == StatusCodeEnum.Success)
            {
                return Ok();
            }
            else
            {
                statusCode.SetInternalError("Giảm số lượng mặt hàng thất bại");
                return BadRequest();
            }
            //var cartItem = await _context.CartItems.FindAsync(cartItemId);
            //// Nếu số lượng hiện tại lớn hơn 1 thì mới giảm
            //if (cartItem != null && cartItem.Quantity > 1)
            //{
            //    cartItem.Quantity -= 1;
            //    cartItem.DateUpdated = DateTime.UtcNow;
            //    _context.CartItems.Update(cartItem);
            //    await _context.SaveChangesAsync();
            //    await UpdateShoppingCartAsync();
            //    return Ok();
            //}
            //else if (cartItem != null && cartItem.Quantity == 1)
            //{
            //    // Nếu số lượng hiện tại là 1 thì xóa mục khỏi giỏ hàng
            //    //_context.CartItems.Remove(cartItem);
            //    await Delete(cartItemId);
            //    await _context.SaveChangesAsync();
            //    //await UpdateShoppingCartAsync();
            //    return Ok();
            //}
            //return Ok();
        }

        


        // GET: CartController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: CartController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{

        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: CartController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //POST: CartController/Delete/5
        [HttpPost]
        //public async Task<ActionResult> Delete(int id, IFormCollection collection)
        public async Task<ActionResult> Delete(int id)
        {
            var statusCode = new StatusCode();
            var result = await _cartService.Delete(id);
            if (result.Code == StatusCodeEnum.Success)
            {
                return Ok();
            }
            else
            {
                statusCode.SetInternalError("Xóa mặt hàng khỏi giỏ hàng thất bại");
                return BadRequest();

            }
        }



    }
}
