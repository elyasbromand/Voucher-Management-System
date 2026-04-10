using Microsoft.EntityFrameworkCore;
using VoucherSystem.Domain.Entities;

namespace VoucherSystem.Infrastructure.Persistence;

public class VoucherDbContext : DbContext
{
    public VoucherDbContext(DbContextOptions<VoucherDbContext> options)
        : base(options) { }

    public DbSet<Voucher> Vouchers => Set<Voucher>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VoucherDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
