using MediatR;
using VoucherSystem.Application.Interfaces;
using VoucherSystem.Domain.Entities;

namespace VoucherSystem.Application.Vouchers;

public class CreateVoucherHandler : IRequestHandler<CreateVoucherCommand, VoucherResponse>
{
    private readonly IVoucherRepository _repository;

    public CreateVoucherHandler(IVoucherRepository repository)
    {
        _repository = repository;
    }

    public async Task<VoucherResponse> Handle(
        CreateVoucherCommand command,
        CancellationToken cancellationToken
    )
    {
        var voucher = Voucher.Create(
            command.Name,
            command.DiscountType,
            command.DiscountValue,
            command.MaxUses,
            command.ExpiresAt
        );

        await _repository.AddAsync(voucher, cancellationToken);

        return new VoucherResponse
        {
            Id = voucher.Id,
            Name = voucher.Name!,
            Code = voucher.Code!.ToString(),
            DiscountType = voucher.DiscountType,
            DiscountValue = voucher.DiscountValue,
            MaxUses = voucher.MaxUses,
            CurrentUses = voucher.CurrentUses,
            ExpiresAt = voucher.ExpiresAt,
            Status = voucher.Status,
        };
    }
}
