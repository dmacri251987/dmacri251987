using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Services.CategoryAPI;
using Services.CategoryAPI.DbContexts;
using Services.CategoryAPI.Repository.Business;
using Services.CategoryAPI.Repository.Data;
using Services.ProductAPI.Repository.Business;

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

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

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

app.UseDeveloperExceptionPage();
app.Run();
