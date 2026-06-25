using MediatR;
using VoucherSystem.Domain.Entities;
using VoucherSystem.Domain.Exceptions;
using VoucherSystem.Domain.Interfaces;

namespace VoucherSystem.Application.Vouchers.Commands;

public class CreateVoucherCommandHandler : IRequestHandler<CreateVoucherCommand, Guid>
{
    private readonly IVoucherRepository _voucherRepository;
    private readonly ICampaignRepository _campaignRepository;

    public CreateVoucherCommandHandler(
        IVoucherRepository voucherRepository,
        ICampaignRepository campaignRepository
    )
    {
        _voucherRepository = voucherRepository;
        _campaignRepository = campaignRepository;
    }

    public async Task<Guid> Handle(
        CreateVoucherCommand request,
        CancellationToken cancellationToken
    )
    {
        var campaign = await _campaignRepository.GetByIdAsync(
            request.CampaignId,
            cancellationToken
        );
        if (campaign == null)
            throw new KeyNotFoundException("Campaign not found.");

        // Prevent creation for closed campaigns
        if (campaign.Status == VoucherSystem.Domain.Enums.CampaignStatus.Closed)
            throw new CampaignInactiveException(campaign.Name);

        var voucher = Voucher.Create(
            request.CampaignId,
            request.DiscountType,
            request.DiscountValue,
            request.MaxUses,
            request.ExpiresAt,
            request.MinimumSpend,
            request.AllowedUserTier
        );

        await _voucherRepository.AddAsync(voucher, cancellationToken);
        await _voucherRepository.SaveChangesAsync(cancellationToken);

        return voucher.Id;
    }
}
