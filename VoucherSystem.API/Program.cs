using MediatR;
using VoucherSystem.Application.Vouchers;
using VoucherSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(CreateVoucherHandler).Assembly)
);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
