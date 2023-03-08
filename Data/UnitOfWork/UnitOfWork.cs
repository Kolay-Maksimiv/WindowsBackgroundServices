using Data.IRepositories;
using Data.Repositories;

namespace Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    private IFileDataRepository _fileData;
    private IFileInfoRepository _fileInfo;

    public IFileDataRepository FileData
    {
        get
        {
            _fileData ??= new FileDataRepository(_context);

            return _fileData;
        }
    }

    public IFileInfoRepository FileInfo
    {
        get
        {
            _fileInfo ??= new FileInfoRepository(_context);

            return _fileInfo;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
