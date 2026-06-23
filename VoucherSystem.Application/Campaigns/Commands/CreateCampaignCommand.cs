using MediatR;

namespace VoucherSystem.Application.Campaigns.Commands;

public sealed class CreateCampaignCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? BudgetCap { get; set; }
}
