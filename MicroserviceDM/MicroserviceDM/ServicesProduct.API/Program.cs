using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServicesProduct.Business.IService;
using ServicesProduct.Business.Service;
using ServicesProduct.DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Services.ProductAPI", Version = "v1" });
    c.CustomSchemaIds(type => type.ToString());
    c.EnableAnnotations();
});


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Prorduct")));


builder.Services.AddScoped<IProductService, ProductService>();

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
