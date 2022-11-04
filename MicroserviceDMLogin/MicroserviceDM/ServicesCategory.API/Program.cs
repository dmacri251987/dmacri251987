using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ServicesCategory.Business.IService;
using ServicesCategory.Business.Service;
using ServicesCategory.DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

var mvcBuilder = builder.Services.AddRazorPages();
if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Services.CategoryAPI", Version = "v1" });
    c.CustomSchemaIds(type => type.ToString());
    c.EnableAnnotations();
});


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Category")));


builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(b =>
    {
        b.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
