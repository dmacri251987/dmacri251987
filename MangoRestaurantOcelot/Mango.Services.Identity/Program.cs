using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mango.Services.Identity.Common;
using Mango.Services.Identity.Initialiazer;
using Duende.IdentityServer.Services;
using Mango.Services.Identity.Services;

var builder = WebApplication.CreateBuilder(args);



//Configuration DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddControllersWithViews();

var build = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(SD.IdentityResources)
.AddInMemoryApiScopes(SD.ApiScope)
.AddInMemoryClients(SD.client)
.AddAspNetIdentity<ApplicationUser>();
build.AddDeveloperSigningCredential();


build.Services.AddScoped<IDbInitialiazer, DbInitialiazer>();
build.Services.AddScoped<IProfileService, ProfileService>();


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
app.UseIdentityServer();
app.UseAuthorization();

//SeedDatabase();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();





void SeedDatabase()
{

    using (var scope = app.Services.CreateScope())

    {

        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitialiazer>();

        dbInitializer.Initialize();
    }
}
