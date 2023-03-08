using Data.Entities;
using Data.IGenericRepository;
using Data.IRepositories;

namespace Data.Repositories;

public class FileDataRepository : GenericRepository<FileData>, IFileDataRepository
{
	public FileDataRepository(ApplicationDbContext context) : base(context) { }
}
