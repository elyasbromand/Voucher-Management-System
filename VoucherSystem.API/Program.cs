using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VoucherSystem.API.Middleware;
using VoucherSystem.Application.Behaviors;
using VoucherSystem.Application.Mapping;
using VoucherSystem.Domain.Interfaces;
using VoucherSystem.Infrastructure.Persistence;
using VoucherSystem.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configuration
var configuration = builder.Configuration;

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
);

// Repositories
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IRedemptionRepository, RedemptionRepository>();
builder.Services.AddScoped<IFraudFlagRepository, FraudFlagRepository>();

// MediatR
builder.Services.AddMediatR(
    typeof(VoucherSystem.Application.Vouchers.Commands.CreateVoucherCommand).Assembly
);

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(
    typeof(VoucherSystem.Application.Vouchers.Commands.CreateVoucherCommand).Assembly
);

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Pipeline behaviors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use built-in exception handler and delegate to our handler service
builder.Services.AddSingleton<GlobalExceptionHandler>();
app.UseExceptionHandler(
    new Microsoft.AspNetCore.Builder.ExceptionHandlerOptions
    {
        ExceptionHandler = async context =>
        {
            var handler = context.RequestServices.GetRequiredService<GlobalExceptionHandler>();
            await handler.HandleAsync(context);
        },
    }
);

app.MapControllers();

app.Run();
