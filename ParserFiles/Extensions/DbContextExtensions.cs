using Data;
using Microsoft.EntityFrameworkCore;

namespace ParserFiles.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var connectionString = "Server=.\\SQLEXPRESS;Database=WindowsBackgroundServices;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=True;";
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Data")));

        return services;
    }
}