using MediatR;

namespace VoucherSystem.Application.Vouchers;

public record GetVoucherByCodeQuery(string Code) : IRequest<VoucherResponse?>;
