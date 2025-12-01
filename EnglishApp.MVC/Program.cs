using EnglishApp.ApplicationCore.IdentityEntities;
using EnglishApp.BusinessLogic.Interfaces;
using EnglishApp.Infrastructure;
using EnglishApp.Infrastructure.Services;
using EnglishApp.MVC.Filters;
using MathNet.Numerics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EnglishAppIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'EnglishAppIdentityDbContextConnection' not found.");
//var connectionString = builder.Configuration.GetConnectionString("EnglishAppIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'EnglishAppIdentityDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();

//AddDbContext
builder.Services.AddDbContext<EnglishAppDbContext>();
builder.Services.AddDbContext<EnglishAppIdentityDbContext>();



// Cau lenh chuan de add Identity cho hai class tuong ung voi IdentityUser va IdentityRole
//builder.Services.AddIdentity<EnglishAppIdentityUser, EnglishAppIdentityRole>()
//    .AddEntityFrameworkStores<EnglishAppIdentityDbContext>()
//    .AddDefaultTokenProviders();



// Using AddDefaultIdentity only use IdentityUser class, not the IdentityRole class
builder.Services.AddDefaultIdentity<EnglishAppIdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EnglishAppIdentityDbContext>()
    .AddDefaultTokenProviders();

#region Filter services

builder.Services.AddScoped<UserPanelFilter>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<UserPanelFilter>(); // Áp dụng toàn cục
});


#endregion


#region Registered Services Area
//==== BEGIN: Registered Services Area ====//
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();




//==== END: Registered Services Area ====//
#endregion



//builder.Services.AddIdentity<EnglishAppIdentityUser, EnglishAppIdentityRole>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false; // Change to false for testing
//})
//    .AddEntityFrameworkStores<EnglishAppIdentityDbContext>()
//    .AddDefaultTokenProviders();


builder.Services.AddRazorPages();

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
});

builder.Services.AddScoped<LessonService>();

//Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
options.Cookie.HttpOnly = true;
options.Cookie.IsEssential = true;
});


// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    //=== Password settings.===//
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});


// Add session and HttpContextAccessor
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<CartService>();



// Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    //options.LoginPath = "/Identity/Pages/Account/Login";
    options.LoginPath = "/Authentication/Login";

    options.AccessDeniedPath = "/Identity/Pages/Account/AccessDenied";
    options.SlidingExpiration = true;
});



#region Ham thay gui
//builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(60);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Password settings.
//    options.Password.RequireDigit = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequiredLength = 5;
//    options.Password.RequiredUniqueChars = 1;

//    // Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings.
//    options.User.AllowedUserNameCharacters =
//            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = false;
//});

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    // Cookie settings
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

//    //options.LoginPath = "/Identity/Account/Login";
//    options.LoginPath = "/Authentication/Login";
//    options.AccessDeniedPath = "/Identity/Pages/Account/AccessDenied";
//    options.SlidingExpiration = true;
//});
#endregion


var app = builder.Build();

//await DataInitialize.Initialize(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    //app.UseExceptionHandler("/Error/Error");
    app.UseHsts();
}



app.UseStaticFiles(); // for wwwroot

// Add SharedMedia as an extra static file source
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "SharedMedia", "media")),
    RequestPath = "/media"
});



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");
//    //endpoints.MapRazorPages();
//});
app.MapRazorPages();
app.Run();
