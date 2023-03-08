using Data.Entities;
using Data.IGenericRepository;
using FileInfo = Data.Entities.FileInfo;

namespace Data.IRepositories;

public interface IFileInfoRepository : IGenericRepository<FileInfo>
{
}
