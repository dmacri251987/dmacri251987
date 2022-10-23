using WebApp.Common;
using WebApp.Services.IServices;
using WebApp.Services.Services;

var builder = WebApplication.CreateBuilder(args);


StaticDetails.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
StaticDetails.CategoryAPIBase = builder.Configuration["ServiceUrls:CategoryAPI"];
StaticDetails.GatewayAPIBase = builder.Configuration["ServiceUrls:GatewayAPI"];
builder.Services.AddHttpClient<IProductService, ProductService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGatewayService, GatewayServices>();
builder.Services.AddScoped<IProductService, ProductService>();

var mvcBuilder = builder.Services.AddRazorPages();
if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
