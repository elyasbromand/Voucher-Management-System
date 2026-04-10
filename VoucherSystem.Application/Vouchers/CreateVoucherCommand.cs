using MediatR;
using VoucherSystem.Domain.Enums;

namespace VoucherSystem.Application.Vouchers;

public record CreateVoucherCommand(
    string Name,
    DiscountType DiscountType,
    decimal DiscountValue,
    int MaxUses,
    DateTime ExpiresAt
) : IRequest<VoucherResponse>;
