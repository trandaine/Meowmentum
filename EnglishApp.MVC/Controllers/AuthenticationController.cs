using EnglishApp.ApplicationCore.IdentityEntities;
using EnglishApp.BusinessLogic.DTOs.Authentications;
using EnglishApp.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EnglishApp.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly EnglishAppDbContext _context;
        private readonly SignInManager<EnglishAppIdentityUser> _signInManager;
        private readonly UserManager<EnglishAppIdentityUser> _userManager;
        private readonly IUserStore<EnglishAppIdentityUser> _userStore;
        private readonly IUserEmailStore<EnglishAppIdentityUser> _emailStore;
        private readonly ILogger<RegisterDTO> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuthenticationController(
            EnglishAppDbContext context,
            UserManager<EnglishAppIdentityUser> userManager,
            IUserStore<EnglishAppIdentityUser> userStore,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<EnglishAppIdentityUser> signInManager,
            ILogger<RegisterDTO> logger
            )
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login(string? returnUrl = null)
        {
            var loginDTO = new LoginDTO
            {
                ReturnUrl = returnUrl,
            };
            return View(loginDTO);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            loginDto.ReturnUrl ??= Url.Content("~/");


            //loginDto.ReturnUrl ??= Url.Content("/Error/Error");


            var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    // Get the current user's ID
                    var user = await _userManager.FindByEmailAsync(loginDto.Email);
                    var userId = await _userManager.GetUserIdAsync(user);

                    // Check if a Customer entry exists for this user
                    var customerExists = await _context.Customers.AnyAsync(c => c.UserId == userId);

                    if (!customerExists)
                    {
                        // Redirect to Customer/Create if no entry exists
                        return RedirectToAction("Create", "Customers");
                    }

                    return LocalRedirect(loginDto.ReturnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = loginDto.ReturnUrl, RememberMe = loginDto.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(loginDto);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(loginDto);
        }

        public IActionResult Register(string? returnUrl = null)
        {
            var registerDto = new RegisterDTO
            {
                ReturnUrl = returnUrl,
            };
            return View(registerDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            registerDto.ReturnUrl ??= Url.Content("~/");
            var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, registerDto.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, registerDto.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = registerDto.ReturnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(registerDto.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = registerDto.Email, returnUrl = registerDto.ReturnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        //return LocalRedirect(registerDto.ReturnUrl);
                        return RedirectToAction("Login", "Authentication");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(registerDto);
        }


        private EnglishAppIdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<EnglishAppIdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(EnglishAppIdentityUser)}'. " +
                    $"Ensure that '{nameof(EnglishAppIdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }


        private IUserEmailStore<EnglishAppIdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<EnglishAppIdentityUser>)_userStore;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToAction("Login", "Authentication");
            }
        }

    }
}
