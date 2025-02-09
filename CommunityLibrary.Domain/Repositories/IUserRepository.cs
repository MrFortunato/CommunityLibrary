using System.Linq.Expressions;

namespace CommunityLibrary.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> InsertAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task<User> DeleteAsync(User entity);
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> SignInUserAsync(User entity);
        Task<PaginatedResponse<User>> GetAllAsync(Expression<Func<User, bool>>? predicate = null, int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);
    }
}
