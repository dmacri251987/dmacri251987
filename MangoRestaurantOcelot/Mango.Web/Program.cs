using Mango.Web;
using Mango.Web.Services.IServices;
using Mango.Web.Services.Services;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);



/*
Dani: Esto permite cambiar datos sin recompilar en la vista
Instalar del nuget el paquete Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
y lo llamo a continuacion
*/

//Llamada
var mvcBuilder = builder.Services.AddRazorPages();
if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}


builder.Services.AddHttpClient<IProductService, ProductService>();
Mango.Web.Common.SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
Mango.Web.Common.SD.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShoppingCartApi"];
Mango.Web.Common.SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponApi"];

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICouponService, CouponService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies",c=>c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = builder.Configuration["ServiceUrls:IdentityApi"];
    options.GetClaimsFromUserInfoEndpoint = true;
    // Mango.Services.Identity.Common
    options.ClientId = "mango";
    // Mango.Services.Identity.Common
    options.ClientSecret = "secret";
    // Mango.Services.Identity.Common
    options.ResponseType = "code";
    options.ClaimActions.MapJsonKey("role", "role", "role");
    options.ClaimActions.MapJsonKey("sub", "sub", "sub");
    options.TokenValidationParameters.NameClaimType = "name";
    options.TokenValidationParameters.RoleClaimType = "role";
    //Mango.Services.Identity.Common ApiScope
    options.Scope.Add("mango");
    options.SaveTokens = true;
});
    


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();