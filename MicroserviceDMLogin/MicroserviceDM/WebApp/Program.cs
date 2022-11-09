using Microsoft.AspNetCore.Authentication.Cookies;
using WebApp.Common;
using WebApp.Services.IServices;
using WebApp.Services.Services;

var builder = WebApplication.CreateBuilder(args);

StaticDetails.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
StaticDetails.CategoryAPIBase = builder.Configuration["ServiceUrls:CategoryAPI"];
StaticDetails.GatewayAPIBase = builder.Configuration["ServiceUrls:GatewayAPI"];
StaticDetails.AuthenticaAPIBase = builder.Configuration["ServiceUrls:IdentityAPI"];
builder.Services.AddHttpClient<IProductService, ProductService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGatewayService, GatewayService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var mvcBuilder = builder.Services.AddRazorPages();
if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.LogoutPath = "/Home/Login";
        options.Cookie.Name = "authenticate_cookies";
    });


builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authenticate}/{action=Index}/{id?}");

app.Run();
