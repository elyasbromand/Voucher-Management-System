using AutoMapper;
using MediatR;
using VoucherSystem.Domain.Interfaces;

namespace VoucherSystem.Application.Vouchers.Queries;

public class GetVouchersByCampaignQueryHandler
    : IRequestHandler<
        GetVouchersByCampaignQuery,
        IReadOnlyList<VoucherSystem.Application.DTOs.VoucherDto>
    >
{
    private readonly IVoucherRepository _voucherRepository;
    private readonly IMapper _mapper;

    public GetVouchersByCampaignQueryHandler(IVoucherRepository voucherRepository, IMapper mapper)
    {
        _voucherRepository = voucherRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<VoucherSystem.Application.DTOs.VoucherDto>> Handle(
        GetVouchersByCampaignQuery request,
        CancellationToken cancellationToken
    )
    {
        var vouchers = await _voucherRepository.GetByCampaignIdAsync(
            request.CampaignId,
            cancellationToken
        );
        return _mapper.Map<IReadOnlyList<VoucherSystem.Application.DTOs.VoucherDto>>(vouchers);
    }
}
