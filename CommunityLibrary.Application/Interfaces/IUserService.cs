using CommunityLibrary.Application.Request;

namespace CommunityLibrary.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailsRequest> InsertAsync(UserCreateRequest request);
        Task<UserDetailsRequest> UpdateAsync(UserUpdateRequest request);
        Task<UserDetailsRequest> GetByIdAsync(Guid id);
        Task<UserDetailsRequest> DeleteAsync(Guid id);  
        Task<IEnumerable<UserDetailsRequest>> GetAllAsync(Func<UserDetailsRequest, bool>? precate, int pagNumber, int pageSize, CancellationToken cancellationToken);
    }
}
