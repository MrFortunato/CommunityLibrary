using CommunityLibrary.Application.Request;
using System.Linq.Expressions;

namespace CommunityLibrary.Application.Interfaces
{
    public interface IClientService
    {
        Task<ClientDetailsRequest> InsertAsync(ClientCreateRequest request);
        Task<ClientDetailsRequest> UpdateAsync(ClientUpdateRequest request);
        Task<ClientDetailsRequest> GetByIdAsync(Guid id);
        Task<ClientDetailsRequest> DeleteAsync(Guid id);
        Task<PaginatedResultService<ClientDetailsRequest>> GetAllAsync(Expression<Func<ClientDetailsRequest, bool>>? precate, int pagNumber, int pageSize, CancellationToken cancellationToken);
    }
}
