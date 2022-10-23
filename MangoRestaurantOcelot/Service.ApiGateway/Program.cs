using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Service.ApiGateway.Aggregators;
using Service.ApiGateway.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json",optional:false, reloadOnChange: true);

// Add services to the container.

builder.Services.AddOcelot()
    .AddDelegatingHandler<NoEncodingHandler>(true)
    .AddSingletonDefinedAggregator<ProductCartByUserAggregator>();


builder.Services.AddAuthentication("Bearer")
   .AddJwtBearer("Bearer", options =>
   {
       options.Authority = "https://localhost:7063/";
       options.TokenValidationParameters = new TokenValidationParameters
       {

           ValidateAudience = false
       };
   });



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseOcelot().Wait();

app.UseHttpsRedirection();
app.Run();
