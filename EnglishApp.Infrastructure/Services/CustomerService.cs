using EnglishApp.ApplicationCore.Entities;
using EnglishApp.BusinessLogic.BaseClasses;
using EnglishApp.BusinessLogic.DTOs;
using EnglishApp.BusinessLogic.DTOs.Authentications;
using EnglishApp.BusinessLogic.Interfaces;
using EnglishApp.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EnglishApp.Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly EnglishAppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomerService(
        IHttpContextAccessor httpContextAccessor,
        EnglishAppDbContext context
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<StatusCode> Create(CustomerDTO customerDto)
    {
        var statusCode = new StatusCode();
        try
        {
            var userId = GetCurrentUserId();
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var newCustomer = new Customer
            {
                Name = customerDto.Name.Trim(),
                Email = userEmail ?? "",
                DateOfBirth = customerDto.DateOfBirth,
                DateCreated = DateTime.Now,
                UserId = userId,
                Gender = customerDto.Gender,
                Level = customerDto.Level,
            };
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            statusCode.SetSuccess("Tạo khách hàng thành công");
            return statusCode;

        }
        catch
        {
            statusCode.SetInternalError("Lỗi khi tạo khách hàng");
        }
        return null!;

    }


    public async Task<StatusCode> Update(CustomerDTO customerDto)
    {
        var statusCode = new StatusCode();
        try
        {
            var selectCustomer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == customerDto.Id);
            if (selectCustomer != null)
            {
                if(customerDto.FileSubmit != null)
                {
                    selectCustomer.Name = customerDto.Name.Trim();
                    selectCustomer.Email = customerDto.Email.Trim();
                    selectCustomer.DateOfBirth = customerDto.DateOfBirth;
                    selectCustomer.Gender = customerDto.Gender;
                    selectCustomer.Level = customerDto.Level;
                    selectCustomer.Avartar = customerDto.Avartar; // Cần phải có nếu không muốn cập nhật ảnh đại diện
                    selectCustomer.Description = customerDto.Description?.Trim();

                }
                else
                {
                    selectCustomer.Name = customerDto.Name.Trim();
                    selectCustomer.Email = customerDto.Email.Trim();
                    selectCustomer.DateOfBirth = customerDto.DateOfBirth;
                    selectCustomer.Gender = customerDto.Gender;
                    selectCustomer.Level = customerDto.Level;
                    selectCustomer.Description = customerDto.Description?.Trim();
                }
                
                _context.Update(selectCustomer);
                await _context.SaveChangesAsync();
                statusCode.SetSuccess("Chỉnh sửa thông tin thành công");
                return statusCode;
            }
        }
        catch
        {
            statusCode.SetInternalError("Chỉnh sửa thông tin thất bại");
        }
        return null!;
    }

    public async Task<CustomerDTO?> GetCustomerDtoById(int id)
    {

        var customer = await _context.Customers
            .Where(c => c.Id.Equals(id))
            .Select(c => new CustomerDTO
            {
                Id = c.Id,
                UserId = c.UserId,
                Name = c.Name,
                Email = c.Email,
                DateOfBirth = c.DateOfBirth,
                Avartar = c.Avartar,
                Gender = c.Gender,
                Level = c.Level,
            }).SingleOrDefaultAsync();
        return customer;

    }
    public async Task<CustomerDTO?> GetCustomerDtoByUserId(string userId)
    {

        var customer = await _context.Customers
            .Where(c => c.UserId.Equals(userId))
            .Select(c => new CustomerDTO
            {
                Id = c.Id,
                UserId = c.UserId,
                Name = c.Name,
                Email = c.Email,
                DateOfBirth = c.DateOfBirth,
                Avartar = c.Avartar,
                Gender = c.Gender,
                Level = c.Level,
            }).SingleOrDefaultAsync();
        return customer;

    }

    public bool CustomerExists(int id)
    {
        return _context.Customers.Any(e => e.Id == id);

    }

    /// <summary>
    /// Hàm lấy UserId của người dùng hiện tại kiểu guid.
    /// </summary>
    /// <returns>Id người dùng ở dạng Guid</returns>
    public string GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        //var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //if (User.Identity.IsAuthenticated)
        //{
        //    return _userManager.GetUserId(User);
        //}

        //if (Request.Cookies.TryGetValue("GuestId", out string guestId))
        //{
        //    return guestId;
        //}

        //guestId = Guid.NewGuid().ToString();
        //Response.Cookies.Append("GuestId", guestId, new CookieOptions
        //{
        //    Expires = DateTimeOffset.UtcNow.AddYears(1),
        //    HttpOnly = true
        //});
        //return guestId;

        if (userId == null)
        {
            var statusCode = new StatusCode();
            statusCode.SetInternalError();
            //_httpContextAccessor.HttpContext.Response.Redirect("/Home/Error");
            _httpContextAccessor.HttpContext?.Response.Redirect("/Error/Error");
        }
        return userId;
    }


    /// <summary>
    /// Hàm lấy tên khách hàng hiện tại để hiển thị lên ComponentView CustomerName.
    /// </summary>
    /// <returns>Tên khách hàng</returns>
    public string GetCurrentCustomerName()
    {
        var statusCode = new StatusCode();
        try
        {
            // Get username from logged-in user (Mail) or return "Guest"
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                var customerName = _context.Customers
                    .Where(c => c.Email == user.Identity.Name)
                    .Select(c => c.Name)
                    .SingleOrDefault();
                return customerName ?? "Unknown User";
            }
            return "Guest";
        }
        catch
        {
            statusCode.SetInternalError("Lỗi khi lấy tên khách hàng hiện tại");
            return "";
        }

    }
}