using CommunityLibrary.Application.Request;
using System.Linq.Expressions;


namespace CommunityLibrary.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookDetailsRequest> InsertAsync(BookCreateRequest request);
        Task<BookDetailsRequest> UpdateAsync(BookUpdateRequest request);
        Task<BookDetailsRequest> GetByIdAsync(Guid id);
        Task<BookDetailsRequest> DeleteAsync(Guid id);
        Task<PaginatedResultService<BookDetailsRequest>> GetAllAsync(Expression<Func<BookDetailsRequest, bool>>? precate, int pagNumber, int pageSize, CancellationToken cancellationToken);
    }
}
