using System.Linq.Expressions;

namespace CommunityLibrary.Domain
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<T> AddAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    }
}
