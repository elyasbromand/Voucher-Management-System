using VoucherSystem.Domain.Enums;

namespace VoucherSystem.Application.Vouchers;

public class VoucherResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public int MaxUses { get; set; }
    public int CurrentUses { get; set; }
    public DateTime ExpiresAt { get; set; }
    public VoucherStatus Status { get; set; }
}
