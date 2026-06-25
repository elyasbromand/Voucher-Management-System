using MediatR;
using VoucherSystem.Application.DTOs;

namespace VoucherSystem.Application.Vouchers.Queries;

public sealed class GetVouchersByCampaignQuery : IRequest<IReadOnlyList<VoucherDto>>
{
    public Guid CampaignId { get; set; }
}
