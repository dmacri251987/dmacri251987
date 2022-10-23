using ApiGateway.Aggregators;
using ApiGateway.Handlers;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Configuration.AddJsonFile("ocelot.json",optional:false,reloadOnChange:true);

builder.Services.AddOcelot()
    .AddSingletonDefinedAggregator<ProductCategoriesAggregator>()
    .AddDelegatingHandler<NoEncodingHandler>(true);

var app = builder.Build();


app.UseHttpsRedirection();

app.UseOcelot().Wait();
app.Run();
