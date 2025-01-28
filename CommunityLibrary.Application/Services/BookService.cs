using AutoMapper;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Pagination;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Domain;
using System.Linq.Expressions;


namespace CommunityLibrary.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IGenericRepository<Book> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDetailsRequest> DeleteAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }

            await _bookRepository.DeleteAsync(book);

            return _mapper.Map<BookDetailsRequest>(book);
        }

        public async Task<PaginatedResultService<BookDetailsRequest>> GetAllAsync(
            Expression<Func<BookDetailsRequest, bool>>? predicate,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            Expression<Func<Book, bool>>? domainPredicate = null;
            if (predicate != null)
            {
                domainPredicate = ExpressionMapper.MapPredicate<BookDetailsRequest, Book>(predicate);
            }

            var paginatedEntities = await _bookRepository.GetAllAsync(
                domainPredicate,
                pageNumber,
                pageSize,
                cancellationToken
            );

            return _mapper.Map<PaginatedResultService<BookDetailsRequest>>(paginatedEntities);

        }

        public async Task<BookDetailsRequest> GetByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }

            return _mapper.Map<BookDetailsRequest>(book);
        }

        public async Task<BookDetailsRequest> InsertAsync(BookCreateRequest request)
        {
            var book = _mapper.Map<Book>(request);
            book.Id = Guid.NewGuid();
            //book.PublishedDate = request.PublishedDate ?? DateTime.UtcNow;

            var entity =  await _bookRepository.InsertAsync(book);

            return _mapper.Map<BookDetailsRequest>(entity); 
        }

        public async Task<BookDetailsRequest> UpdateAsync(BookUpdateRequest request)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.Id} not found.");
            }

            _mapper.Map(request, book);
             var entity =   await _bookRepository.UpdateAsync(book);
            return  _mapper.Map<BookDetailsRequest>(entity);
        }
    }
}
