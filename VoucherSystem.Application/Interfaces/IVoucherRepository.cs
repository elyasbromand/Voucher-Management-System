using VoucherSystem.Domain.Entities;

namespace VoucherSystem.Application.Interfaces;

public interface IVoucherRepository
{
    Task AddAsync(Voucher voucher, CancellationToken cancellationToken = default);
    Task<Voucher?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<Voucher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
