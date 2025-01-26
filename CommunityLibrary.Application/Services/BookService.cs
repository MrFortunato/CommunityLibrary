using AutoMapper;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Domain;


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

        public async Task<IEnumerable<BookDetailsRequest>> GetAllAsync(
            Func<BookDetailsRequest, bool>? predicate,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync(
                predicate: null,
                pageNumber: pageNumber,
                pageSize: pageSize,
                cancellationToken: cancellationToken
            );

            var result = _mapper.Map<IEnumerable<BookDetailsRequest>>(books);

            return predicate == null ? result : result.Where(predicate);
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
