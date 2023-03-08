using Data.IGenericRepository;
using Data.IRepositories;
using FileInfo = Data.Entities.FileInfo;

namespace Data.Repositories;

public class FileInfoRepository: GenericRepository<FileInfo>, IFileInfoRepository
{
    public FileInfoRepository(ApplicationDbContext context) : base(context) { }
}
