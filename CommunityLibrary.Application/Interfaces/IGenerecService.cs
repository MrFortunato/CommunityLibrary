using System.Linq.Expressions;

namespace CommunityLibrary.Application.Interfaces
{
    public interface IGenerecService<T> where T : class
    {
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    }
}
