using EnglishApp.BusinessLogic.Interfaces;
using EnglishApp.Infrastructure;
using EnglishApp.Infrastructure.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<EnglishAppDbContext>();

builder.Services.AddScoped<ICoursesService, CoursesService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My .NET 10 API",
        Version = "v1",
        Description = "Demo API with Swagger support"
    });
});


var app = builder.Build();

app.UseStaticFiles(); // for wwwroot

// Add SharedMedia as an extra static file source
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "SharedMedia", "media")),
    RequestPath = "/media"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My .NET 10 API v1");
        c.RoutePrefix = string.Empty; // Swagger UI at root (http://localhost:<port>/)
    });
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
