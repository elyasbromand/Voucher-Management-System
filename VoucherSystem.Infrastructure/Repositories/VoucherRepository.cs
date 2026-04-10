using Microsoft.EntityFrameworkCore;
using VoucherSystem.Application.Interfaces;
using VoucherSystem.Domain.Entities;
using VoucherSystem.Infrastructure.Persistence;

namespace VoucherSystem.Infrastructure.Repositories;

public class VoucherRepository : IVoucherRepository
{
    private readonly VoucherDbContext _dbContext;

    public VoucherRepository(VoucherDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Voucher voucher, CancellationToken cancellationToken = default)
    {
        await _dbContext.Vouchers.AddAsync(voucher, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<Voucher?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var normalizedCode = code.Trim().ToUpperInvariant();

        return await _dbContext.Vouchers.FirstOrDefaultAsync(
            voucher => voucher.Code!.Value == normalizedCode,
            cancellationToken
        );
    }

    public Task<Voucher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Vouchers.FirstOrDefaultAsync(voucher => voucher.Id == id, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
