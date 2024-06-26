﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.OpenApi.Models;
using WebAPI;
using WebAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebAPI.Interfaces;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add repositories and dependency injection // inject repository vào code
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// Add UnitOfWork dependency ProductRepository and CategoryRepository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Add AutoMapper // ánh xạ dữ liệu
builder.Services.AddAutoMapper(typeof(Startup));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Your API Name",
        Version = "v1",
        Description = "Your API description",
    });
});

// Configure rate limiting options, giới hạn thời gian truy cập


builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


// Add rate limiting middleware
builder.Services.AddMemoryCache(); // Add the IMemoryCache service here
builder.Services.AddInMemoryRateLimiting();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();
// The rest of the middleware

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
