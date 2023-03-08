using Data.IRepositories;

namespace Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IFileDataRepository FileData { get; }

    IFileInfoRepository FileInfo { get; }

    Task<int> SaveAsync();
}
