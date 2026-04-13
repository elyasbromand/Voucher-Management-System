using VoucherSystem.Domain.Entities;

namespace VoucherSystem.Domain.Interfaces;

public interface IVoucherRepository
{
    Task<Voucher?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Voucher>> GetByCampaignIdAsync(
        Guid campaignId,
        CancellationToken cancellationToken = default
    );
    Task AddAsync(Voucher voucher, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
