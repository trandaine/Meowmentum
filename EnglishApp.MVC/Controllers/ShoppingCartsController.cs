using EnglishApp.ApplicationCore.Entities;
using EnglishApp.ApplicationCore.IdentityEntities;
using EnglishApp.BusinessLogic.DTOs.ShoppingCarts;
using EnglishApp.BusinessLogic.Interfaces;
using EnglishApp.Infrastructure;
using EnglishApp.Infrastructure.Constants;
using EnglishApp.MVC.ExtensionMethods;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EnglishApp.MVC.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly EnglishAppDbContext _context;
        private readonly ISession _session;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICustomerService _customerService;
        private readonly UserManager<EnglishAppIdentityUser> _userManager;

        public ShoppingCartsController(
            EnglishAppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            ICustomerService customerService,
            UserManager<EnglishAppIdentityUser> userManager,
            IShoppingCartService shoppingCartService
            )
        {
            _customerService = customerService;
            _userManager = userManager;
            _context = context;
            _session = httpContextAccessor.HttpContext!.Session;
            _httpContextAccessor = httpContextAccessor;
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var englishAppDbContext = _context.ShoppingCarts.Include(s => s.Customer);
            return View(await englishAppDbContext.ToListAsync());
        }

        // GET: ShoppingCarts/Details/5
        public async Task<IActionResult> Details()
        {
            var userId = _customerService.GetCurrentUserId();
            if (userId == null)
            {
                return NotFound();
            }

            // Get the shopping cart Id
            var shoppingCart = await _context.ShoppingCarts
                .FirstOrDefaultAsync(m => m.UserId == userId);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            // Step 2: Get all CartItems for this ShoppingCart
            //var cartItems = await _context.CartItems
            //    .Where(ci => ci.ShoppingCartId == shoppingCart.Id)
            //    .ToListAsync();

            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Create
        //public IActionResult Create()
        //{
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
        //    return View();
        //}

        //// POST: ShoppingCarts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,UserId,TotalQuantity,TotalAmount,DateCreated,DateUpdated,CustomerId")] ShoppingCart shoppingCart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(shoppingCart);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", shoppingCart.CustomerId);
        //    return View(shoppingCart);
        //}

        //// GET: ShoppingCarts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
        //    if (shoppingCart == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", shoppingCart.CustomerId);
        //    return View(shoppingCart);
        //}

        //// POST: ShoppingCarts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TotalQuantity,TotalAmount,DateCreated,DateUpdated,CustomerId")] ShoppingCart shoppingCart)
        //{
        //    if (id != shoppingCart.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(shoppingCart);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ShoppingCartExists(shoppingCart.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", shoppingCart.CustomerId);
        //    return View(shoppingCart);
        //}

        //// GET: ShoppingCarts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var shoppingCart = await _context.ShoppingCarts
        //        .Include(s => s.Customer)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (shoppingCart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(shoppingCart);
        //}

        //// POST: ShoppingCarts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
        //    if (shoppingCart != null)
        //    {
        //        _context.ShoppingCarts.Remove(shoppingCart);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ShoppingCartExists(int id)
        //{
        //    return _context.ShoppingCarts.Any(e => e.Id == id);
        //}


        /// <summary>
        /// Hàm REFRESH COMPONENTVIEW ShoppingCartItemList hiển thị LẠI DANH SÁCH SẢN PHẨM trong giỏ hàng sau khi thêm sản phẩm thành công
        /// </summary>
        /// <returns></returns>
        //public async Task<IActionResult> GetShoppingCartItemListComponentView()
        public IActionResult GetShoppingCartItemListComponentView()
        {
            //var userId = _customerService.GetCurrentUserId();
            //var items = await _shoppingCartService.GetCartItemsByUserId(userId);
            //return ViewComponent("ShoppingCartItemList", new { items });
            return ViewComponent("ShoppingCartItemList");
        }



        /// <summary>
        /// Hàm REFRESH COMPONENTVIEW CartCount hiển thị LẠI TỔNG số lượng sản phẩm trong giỏ hàng sau khi thêm sản phẩm thành công
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetCartQuantityComponentView()
        {
            var cartCount = await _shoppingCartService.GetCountItemsInShoppingCart();

            return ViewComponent("CartCount", new { cartCount });
        }



        /// <summary>
        /// Hàm thêm sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="idCourse"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddToCart(int idCourse, int quantity = 1)
        {
            var totalQuantity = quantity;
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = _httpContextAccessor.HttpContext.User;
            //if (userId != null)
            //{
            //    user = _userManager.GetUserAsync(userId);
            //}

            //var cart = HttpContext.Session.GetObject<ShoppingCart>("Cart") ?? new ShoppingCart();
            var cart = _session.GetObject<ShoppingCartDTO>(ConnectionConstants.KEY_CART) ?? new ShoppingCartDTO(userId ?? "");
            //var cart = _session.GetObject<ShoppingCartDTO>(Constants.KEY_CART) ?? new ShoppingCartDTO(user ?? "");

            if (cart == null)
            {
                cart = new ShoppingCartDTO(userId ?? "");
                //cart = new ShoppingCartDTO(user ?? "");
            }

            var course = await _context.Courses
                .Where(d => d.Id == idCourse)
                .SingleOrDefaultAsync();
            if (course != null)
            {
                cart.AddItem(idCourse, course.Name, (decimal)course.Price, quantity, "");
                //HttpContext.Session.SetObject("Cart", cart);
                _session.SetObject(ConnectionConstants.KEY_CART, cart);
                totalQuantity = cart.TotalQuantity;
            }
            return Content(totalQuantity.ToString());
        }



    }
}
