// My request DTO for creating a voucher
using VoucherSystem.Domain.Enums;

namespace VoucherSystem.Application.Vouchers;

public class CreateVoucherRequest
{
    public string Name { get; set; } = string.Empty;
    public DiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public int MaxUses { get; set; }
    public DateTime ExpiresAt { get; set; }
}
