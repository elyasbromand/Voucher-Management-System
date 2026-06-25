using MediatR;
using VoucherSystem.Application.DTOs;

namespace VoucherSystem.Application.Vouchers.Queries;

public sealed class GetVoucherByCodeQuery : IRequest<VoucherDto?>
{
    public string Code { get; set; } = null!;
}
