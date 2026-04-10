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

var vouchers = app.MapGroup("/api/v1/vouchers");

vouchers.MapPost(
    "/",
    async (CreateVoucherRequest request, ISender sender, CancellationToken cancellationToken) =>
    {
        var command = new CreateVoucherCommand(
            request.Name,
            request.DiscountType,
            request.DiscountValue,
            request.MaxUses,
            request.ExpiresAt
        );

        var createdVoucher = await sender.Send(command, cancellationToken);
        return Results.Created($"/api/v1/vouchers/{createdVoucher.Code}", createdVoucher);
    }
);

vouchers.MapGet(
    "/{code}",
    async (string code, ISender sender, CancellationToken cancellationToken) =>
    {
        var voucher = await sender.Send(new GetVoucherByCodeQuery(code), cancellationToken);
        return voucher is null ? Results.NotFound() : Results.Ok(voucher);
    }
);

app.Run();
