using AutoMapper;
using MediatR;
using VoucherSystem.Domain.Interfaces;

namespace VoucherSystem.Application.Vouchers.Queries;

public class GetVoucherByCodeQueryHandler
    : IRequestHandler<GetVoucherByCodeQuery, VoucherSystem.Application.DTOs.VoucherDto?>
{
    private readonly IVoucherRepository _voucherRepository;
    private readonly IMapper _mapper;

    public GetVoucherByCodeQueryHandler(IVoucherRepository voucherRepository, IMapper mapper)
    {
        _voucherRepository = voucherRepository;
        _mapper = mapper;
    }

    public async Task<VoucherSystem.Application.DTOs.VoucherDto?> Handle(
        GetVoucherByCodeQuery request,
        CancellationToken cancellationToken
    )
    {
        var voucher = await _voucherRepository.GetByCodeAsync(request.Code, cancellationToken);
        if (voucher == null)
            return null;
        return _mapper.Map<VoucherSystem.Application.DTOs.VoucherDto>(voucher);
    }
}
