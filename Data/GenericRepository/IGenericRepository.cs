using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.IGenericRepository;

public interface IGenericRepository<T> where T : class
{
    public Task<List<T>> GetListAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "",
        bool isTracking = true);

    public Task<T?> GetFirstAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "",
        bool isTracking = true);

    public Task<T?> GetByIdAsync(int id);

    public Task InsertAsync(T entity);

    public Task DeleteAsync(int id);

    public void Delete(T? entityToDelete);

    public void Update(T entityToUpdate);

    public DbSet<T> CustomQuery();
    public Task<int> Count(Expression<Func<T, bool>> filter = null);
}