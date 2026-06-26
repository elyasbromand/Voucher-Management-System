using Microsoft.EntityFrameworkCore;
using VoucherSystem.Domain.Entities;

namespace VoucherSystem.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Voucher> Vouchers { get; set; } = null!;
    public DbSet<Campaign> Campaigns { get; set; } = null!;
    public DbSet<Redemption> Redemptions { get; set; } = null!;
    public DbSet<Domain.Entities.FraudFlag> FraudFlags { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
