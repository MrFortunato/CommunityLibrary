using CommunityLibrary.Application.Request;
using CommunityLibrary.Application.Request.Rental;
using System.Linq.Expressions;

namespace CommunityLibrary.Application.Interfaces
{
    public interface IBookRentalService
    {
        Task<BookRentalDetailsRequest> InsertAsync(BookRentalCreateRequest request);
        Task<BookRentalDetailsRequest> UpdateAsync(BookRentalCreateRequest request);
        Task<BookRentalDetailsRequest> GetByIdAsync(Guid id);
        Task<BookRentalDetailsRequest> DeleteAsync(Guid id);
        Task<PaginatedResultService<BookRentalDetailsRequest>> GetAllAsync(Expression<Func<BookRentalDetailsRequest, bool>>? predicate, int pagNumber, int pageSize, CancellationToken cancellationToken);
    }
}
