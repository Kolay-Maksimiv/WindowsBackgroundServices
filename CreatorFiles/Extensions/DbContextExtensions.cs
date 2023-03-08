using Data;
using Microsoft.EntityFrameworkCore;

namespace CreatorFiles.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AppConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Data")));

        return services;
    }
}