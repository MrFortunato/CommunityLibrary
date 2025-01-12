using CommunityLibrary.Application.Request;

namespace CommunityLibrary.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDetailsRequest> InsertAsync(AuthorCreateRequest request);
        Task<AuthorDetailsRequest> UpdateAsync(AuthorUpdateRequest request);
        Task<AuthorDetailsRequest> GetByIdAsync(Guid id);
        Task<AuthorDetailsRequest> DeleteAsync(Guid id);
        Task<IEnumerable<AuthorDetailsRequest>> GetAllAsync(Func<AuthorDetailsRequest, bool>? precate, int pagNumber, int pageSize, CancellationToken cancellationToken);
    }
}
