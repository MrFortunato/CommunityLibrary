using CommunityLibrary.Application.Request;
using System.Linq.Expressions;

namespace CommunityLibrary.Application.Interfaces
{
    public interface IBookCategoryService
    {
        Task<BookCategoryDetailsRequest> InsertAsync(BookCategoryCreateRequest request);
        Task<BookCategoryDetailsRequest> UpdateAsync(BookCategoryUpdateRequest request);
        Task<BookCategoryDetailsRequest> GetByIdAsync(Guid id);
        Task<BookCategoryDetailsRequest> DeleteAsync(Guid id);
        Task<PaginatedResultService<BookCategoryDetailsRequest>> GetAllAsync(Expression<Func<BookCategoryDetailsRequest, bool>>? precate, int pagNumber, int pageSize, CancellationToken cancellationToken);
    }
}
