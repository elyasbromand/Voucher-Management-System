using Microsoft.EntityFrameworkCore;
using VoucherSystem.Domain.Entities;
using VoucherSystem.Domain.Interfaces;
using VoucherSystem.Infrastructure.Persistence;

namespace VoucherSystem.Infrastructure.Persistence.Repositories;

public class FraudFlagRepository : IFraudFlagRepository
{
    private readonly AppDbContext _context;

    public FraudFlagRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FraudFlag?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return await _context.FraudFlags.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<FraudFlag>> GetUnresolvedAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await _context.FraudFlags.Where(f => !f.Resolved).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FraudFlag flag, CancellationToken cancellationToken = default)
    {
        await _context.FraudFlags.AddAsync(flag, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
