
using CommunityLibrary.Application.Request;

namespace CommunityLibrary.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailsRequest> InsertAsync(UserCreateRequest request);
        Task<UserDetailsRequest> UpdateAsync(UserUpdateRequest request);
        Task<UserDetailsRequest> GetByIdAsync(Guid id);
        Task<UserDetailsRequest> DeleteAsync(Guid id);
        Task<PaginatedResultService<UserDetailsRequest>> GetAllAsync(
           Func<UserDetailsRequest, bool>? predicate = null,
           int pageNumber = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default);
       
    }
}
