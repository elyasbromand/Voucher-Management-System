using Microsoft.EntityFrameworkCore;
using VoucherSystem.Domain.Entities;
using VoucherSystem.Domain.Interfaces;
using VoucherSystem.Infrastructure.Persistence;

namespace VoucherSystem.Infrastructure.Persistence.Repositories;

public class VoucherRepository : IVoucherRepository
{
    private readonly AppDbContext _context;

    public VoucherRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Voucher?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default
    )
    {
        return await _context.Vouchers.FirstOrDefaultAsync(
            v => v.Code.Value == code,
            cancellationToken
        );
    }

    public async Task<IReadOnlyList<Voucher>> GetByCampaignIdAsync(
        Guid campaignId,
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Vouchers.Where(v => v.CampaignId == campaignId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Voucher voucher, CancellationToken cancellationToken = default)
    {
        await _context.Vouchers.AddAsync(voucher, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
