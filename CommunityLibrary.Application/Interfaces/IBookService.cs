using CommunityLibrary.Application.Request;


namespace CommunityLibrary.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookDetailsRequest> InsertAsync(BookCreateRequest request);
        Task<BookDetailsRequest> UpdateAsync(BookUpdateRequest request);
        Task<BookDetailsRequest> GetByIdAsync(Guid id);
        Task<BookDetailsRequest> DeleteAsync(Guid id);
        Task<IEnumerable<BookDetailsRequest>> GetAllAsync(Func<BookDetailsRequest, bool>? precate, int pagNumber, int pageSize, CancellationToken cancellationToken);
    }
}
