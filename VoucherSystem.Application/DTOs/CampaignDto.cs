using System;

namespace VoucherSystem.Application.DTOs;

public sealed class CampaignDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? BudgetCap { get; set; }
    public decimal TotalDiscountIssued { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
