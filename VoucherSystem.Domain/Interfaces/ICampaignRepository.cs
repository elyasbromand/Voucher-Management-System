using VoucherSystem.Domain.Entities;

namespace VoucherSystem.Domain.Interfaces;

public interface ICampaignRepository
{
    Task<Campaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Campaign>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Campaign campaign, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
