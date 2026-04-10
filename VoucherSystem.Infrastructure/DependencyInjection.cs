using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoucherSystem.Application.Interfaces;
using VoucherSystem.Infrastructure.Persistence;
using VoucherSystem.Infrastructure.Repositories;

namespace VoucherSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("VoucherSystem");

        services.AddDbContext<VoucherDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        services.AddScoped<IVoucherRepository, VoucherRepository>();

        return services;
    }
}
