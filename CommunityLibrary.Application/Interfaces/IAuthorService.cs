using CommunityLibrary.Application.Request;
using System.Linq.Expressions;

namespace CommunityLibrary.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDetailsRequest> InsertAsync(AuthorCreateRequest request);
        Task<AuthorDetailsRequest> UpdateAsync(AuthorUpdateRequest request);
        Task<AuthorDetailsRequest> GetByIdAsync(Guid id);
        Task<AuthorDetailsRequest> DeleteAsync(Guid id);
        Task<PaginatedResultService<AuthorDetailsRequest>> GetAllAsync(Expression<Func<AuthorDetailsRequest, bool>>? precate, int pagNumber, int pageSize, CancellationToken cancellationToken);
    }
}
