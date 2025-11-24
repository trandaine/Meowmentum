using EnglishApp.ApplicationCore.Entities;
using EnglishApp.ApplicationCore.Enums;
using EnglishApp.BusinessLogic.DTOs.Authentications;
using EnglishApp.BusinessLogic.Interfaces;
using EnglishApp.Infrastructure;
using EnglishApp.Infrastructure.Constants;
using EnglishApp.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EnglishApp.MVC.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerService _customerService;
        private readonly IWebHostEnvironment _env;



        public CustomersController(
            IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor,
            ICustomerService customerService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _customerService = customerService;

        }


        

        // GET: Customers/Details/5
        public async Task<IActionResult> Details()
        {
            
            var userId = _customerService.GetCurrentUserId();
            var customer = await _customerService.GetCustomerDtoByUserId(userId);
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            var customerGender = Enum.GetValues(typeof(GenderEnum))
                .Cast<GenderEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();
            ViewBag.CustomerGenders = new SelectList(customerGender, "Value", "Text");
            var customerLevel = Enum.GetValues(typeof(LevelEnum))
                .Cast<LevelEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();
            ViewBag.CustomerLevels = new SelectList(customerLevel, "Value", "Text");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerDTO customerModel)
        {
            if(ModelState.IsValid)
            {
                var customerStatus = await _customerService.Create(customerModel);
                if(customerStatus.Code == StatusCodeEnum.Success)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            var customerGender = Enum.GetValues(typeof(GenderEnum))
                .Cast<GenderEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();
            ViewBag.CustomerGenders = new SelectList(customerGender, "Value", "Text");
            var customerLevel = Enum.GetValues(typeof(LevelEnum))
                .Cast<LevelEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();
            ViewBag.CustomerLevels = new SelectList(customerLevel, "Value", "Text");
            return View(nameof(Create), customerModel);


        }



        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var customerGender = Enum.GetValues(typeof(GenderEnum))
               .Cast<GenderEnum>()
               .Select(e => new SelectListItem
               {
                   Value = ((int)e).ToString(),
                   Text = e.ToString()
               }).ToList();
            ViewBag.CustomerGenders = new SelectList(customerGender, "Value", "Text");
            var customerLevel = Enum.GetValues(typeof(LevelEnum))
                .Cast<LevelEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();
            ViewBag.CustomerLevels = new SelectList(customerLevel, "Value", "Text");

            var customer = await _customerService.GetCustomerDtoById(id);
            
            return PartialView(nameof(Edit),customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerDTO customerDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mediaHelper = new MediaHelper(_env.WebRootPath);
                    //var customerAvatarStatusCode = await mediaHelper.SaveMedia(customerDto.FileSubmit, "media\\customer_profile_images");
                    var customerAvatarStatusCode = await mediaHelper.SaveMedia(customerDto.FileSubmit, AppConstants.AVATAR_FILE_PATH);
                    if (customerAvatarStatusCode.Code == StatusCodeEnum.Success)
                    {
                        customerDto.Avartar = customerAvatarStatusCode.StringReturn;
                        await _customerService.Update(customerDto);
                    }
                    else
                    {
                        // Lưu ảnh đại diện không thành công thì vẫn cập nhật các thông tin khác
                        await _customerService.Update(customerDto);
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_customerService.CustomerExists(customerDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Details));
                return Json(new {success = true });
            }

            var customerGender = Enum.GetValues(typeof(GenderEnum))
               .Cast<GenderEnum>()
               .Select(e => new SelectListItem
               {
                   Value = ((int)e).ToString(),
                   Text = e.ToString()
               }).ToList();
            ViewBag.CustomerGenders = new SelectList(customerGender, "Value", "Text");
            var customerLevel = Enum.GetValues(typeof(LevelEnum))
                .Cast<LevelEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();
            ViewBag.CustomerLevels = new SelectList(customerLevel, "Value", "Text");
            return PartialView(nameof(Edit),customerDto);
        }

       
    }
}
