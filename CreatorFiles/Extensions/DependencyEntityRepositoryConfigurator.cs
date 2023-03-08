using Data.IGenericRepository;
using Data.IRepositories;
using Data.Repositories;
using Data.UnitOfWork;

namespace CreatorFiles.Extensions;

public static partial class DependencyСonfigurator
{
    public static void AddEntityRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFileDataRepository, FileDataRepository>();
        services.AddScoped<IFileInfoRepository, IFileInfoRepository>();
    }

    public static void AddGenericRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }

    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
