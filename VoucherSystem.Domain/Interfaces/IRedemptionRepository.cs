using VoucherSystem.Domain.Entities;

namespace VoucherSystem.Domain.Interfaces;

public interface IRedemptionRepository
{
    Task<int> CountByUserAndVoucherAsync(
        Guid userId,
        Guid voucherId,
        CancellationToken cancellationToken = default
    );
    Task AddAsync(Redemption redemption, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Redemption>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    );
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
