using System.Linq.Expressions;

namespace CommunityLibrary.Domain.Repositories
{
    public interface IBookRentalRepository
    {
        Task<BookRental> InsertAsync(BookRental entity);
        Task<BookRental> UpdateAsync(BookRental entity);
        Task<BookRental> DeleteAsync(BookRental entity);
        Task<BookRental> GetByIdAsync(Guid id);
        Task<PaginatedResponse<BookRental>> GetAllAsync(Expression<Func<BookRental, bool>>? predicate = null, int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);
    }
}
