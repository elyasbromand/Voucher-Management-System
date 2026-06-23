using System;

namespace VoucherSystem.Application.DTOs;

public sealed class RedemptionDto
{
    public Guid Id { get; set; }
    public Guid VoucherId { get; set; }
    public Guid UserId { get; set; }
    public DateTime RedeemedAt { get; set; }
    public decimal CartTotal { get; set; }
    public decimal DiscountApplied { get; set; }
    public string IpAddress { get; set; } = null!;
    public string Result { get; set; } = null!;
}
