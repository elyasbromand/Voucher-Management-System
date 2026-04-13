using VoucherSystem.Domain.Enums;
using VoucherSystem.Domain.Exceptions;

namespace VoucherSystem.Domain.Entities;

public sealed class Campaign
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal? BudgetCap { get; private set; }
    public decimal TotalDiscountIssued { get; private set; }
    public CampaignStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Campaign() { }

    public static Campaign Create(
        string name,
        DateTime startDate,
        DateTime endDate,
        decimal? budgetCap = null
    )
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Campaign name cannot be empty.", nameof(name));

        if (endDate <= startDate)
            throw new ArgumentException("End date must be after start date.", nameof(endDate));

        if (budgetCap.HasValue && budgetCap.Value <= 0)
            throw new ArgumentException("Budget cap must be greater than zero.", nameof(budgetCap));

        return new Campaign
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            StartDate = startDate,
            EndDate = endDate,
            BudgetCap = budgetCap,
            TotalDiscountIssued = 0m,
            Status = CampaignStatus.Draft,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public bool IsActive()
    {
        var now = DateTime.UtcNow;
        return Status == CampaignStatus.Active && now >= StartDate && now <= EndDate;
    }

    public void Activate()
    {
        if (Status != CampaignStatus.Draft && Status != CampaignStatus.Paused)
            throw new InvalidOperationException(
                $"Cannot activate a campaign in '{Status}' status."
            );

        Status = CampaignStatus.Active;
    }

    public void RecordRedemption(decimal discountAmount)
    {
        if (!IsActive())
            throw new CampaignInactiveException(Name);

        TotalDiscountIssued += discountAmount;

        if (BudgetCap.HasValue && TotalDiscountIssued >= BudgetCap.Value)
            Status = CampaignStatus.BudgetExhausted;
    }

    public void Pause()
    {
        if (Status != CampaignStatus.Active)
            throw new InvalidOperationException("Only active campaigns can be paused.");

        Status = CampaignStatus.Paused;
    }

    public void Resume()
    {
        if (Status != CampaignStatus.Paused)
            throw new InvalidOperationException("Only paused campaigns can be resumed.");

        Status = CampaignStatus.Active;
    }

    public void Close()
    {
        Status = CampaignStatus.Closed;
    }
}
