using Microsoft.EntityFrameworkCore;
using VoucherSystem.Domain.Entities;
using VoucherSystem.Domain.Interfaces;
using VoucherSystem.Infrastructure.Persistence;

namespace VoucherSystem.Infrastructure.Persistence.Repositories;

public class RedemptionRepository : IRedemptionRepository
{
    private readonly AppDbContext _context;

    public RedemptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CountByUserAndVoucherAsync(
        Guid userId,
        Guid voucherId,
        CancellationToken cancellationToken = default
    )
    {
        return await _context.Redemptions.CountAsync(
            r => r.UserId == userId && r.VoucherId == voucherId,
            cancellationToken
        );
    }

    public async Task AddAsync(Redemption redemption, CancellationToken cancellationToken = default)
    {
        await _context.Redemptions.AddAsync(redemption, cancellationToken);
    }

    public async Task<IReadOnlyList<Redemption>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Redemptions.Where(r => r.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
