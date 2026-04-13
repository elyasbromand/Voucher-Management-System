using VoucherSystem.Domain.Entities;

namespace VoucherSystem.Domain.Interfaces;

public interface IFraudFlagRepository
{
    Task<FraudFlag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FraudFlag>> GetUnresolvedAsync(
        CancellationToken cancellationToken = default
    );
    Task AddAsync(FraudFlag flag, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
