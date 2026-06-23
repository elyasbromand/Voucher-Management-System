using MediatR;
using VoucherSystem.Domain.Entities;
using VoucherSystem.Domain.Interfaces;

namespace VoucherSystem.Application.Campaigns.Commands;

public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, Guid>
{
    private readonly ICampaignRepository _campaignRepository;

    public CreateCampaignCommandHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<Guid> Handle(
        CreateCampaignCommand request,
        CancellationToken cancellationToken
    )
    {
        var campaign = Campaign.Create(
            request.Name,
            request.StartDate,
            request.EndDate,
            request.BudgetCap
        );
        campaign.Activate();

        await _campaignRepository.AddAsync(campaign, cancellationToken);
        await _campaignRepository.SaveChangesAsync(cancellationToken);

        return campaign.Id;
    }
}
