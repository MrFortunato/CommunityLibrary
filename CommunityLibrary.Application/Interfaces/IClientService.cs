using CommunityLibrary.Application.Request;

namespace CommunityLibrary.Application.Interfaces
{
    public interface IClientService
    {
        Task<ClientDetailsRequest> InsertAsync(ClientCreateRequest request);
        Task<ClientDetailsRequest> UpdateAsync(ClientUpdateRequest request);
        Task<ClientDetailsRequest> GetByIdAsync(Guid id);
        Task<ClientDetailsRequest> DeleteAsync(Guid id);
        Task<IEnumerable<ClientDetailsRequest>> GetAllAsync(Func<ClientDetailsRequest, bool>? precate, int pagNumber, int pageSize, CancellationToken cancellationToken);
    }
}
