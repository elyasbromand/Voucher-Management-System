using MediatR;
using VoucherSystem.Application.Interfaces;

namespace VoucherSystem.Application.Vouchers;

public class GetVoucherByCodeHandler : IRequestHandler<GetVoucherByCodeQuery, VoucherResponse?>
{
    private readonly IVoucherRepository _repository;

    public GetVoucherByCodeHandler(IVoucherRepository repository)
    {
        _repository = repository;
    }

    public async Task<VoucherResponse?> Handle(
        GetVoucherByCodeQuery query,
        CancellationToken cancellationToken
    )
    {
        var voucher = await _repository.GetByCodeAsync(query.Code, cancellationToken);

        if (voucher is null)
            return null;

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
