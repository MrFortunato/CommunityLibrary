using AutoMapper;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Domain;

namespace CommunityLibrary.Application.Services
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<BookCategory> _repository;  
        public BookCategoryService(IMapper mapper, IGenericRepository<BookCategory> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<BookCategoryCreateRequest> InsertAsync(BookCategoryCreateRequest entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The category cannot be null.");

            var bookCategory = _mapper.Map<BookCategory>(entity);
            var addedEntity = await _repository.InsertAsync(bookCategory);
            return _mapper.Map<BookCategoryCreateRequest>(addedEntity);
        }
        public async Task<BookCategoryUpdateRequest> UpdateAsync(BookCategoryUpdateRequest entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The category cannot be null.");

            var bookCategory = _mapper.Map<BookCategory>(entity);
            bookCategory.LastModifiedDate = DateTime.UtcNow;
            var addedEntity = await _repository.UpdateAsync(bookCategory);
            return _mapper.Map<BookCategoryUpdateRequest>(addedEntity);
        }
        public async Task<BookCategoryDetailsRequest> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("The provided ID cannot be empty.", nameof(id));

            var selectedBook = await _repository.GetByIdAsync(id);

            if (selectedBook == null)
            {
                throw new KeyNotFoundException($"No category found with ID: {id}");
            }
            var addedEntity = await _repository.DeleteAsync(selectedBook);
            return _mapper.Map<BookCategoryDetailsRequest>(addedEntity);
        }

        public async Task<IEnumerable<BookCategoryDetailsRequest>> GetAllAsync(
            Func<BookCategoryDetailsRequest, bool>? predicate = null,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than 0.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");

            bool domainPredicate(BookCategory bookCategory)
            {
                if(predicate == null)
                {
                    return true;
                }
                return predicate(_mapper.Map<BookCategoryDetailsRequest>(bookCategory));
            }
            // Fetch paginated entities from the repository
            var entities = await _repository.GetAllAsync(domainPredicate, pageNumber, pageSize, cancellationToken);
            return entities.Select(e => _mapper.Map<BookCategoryDetailsRequest>(e)); 
        }


        public async Task<BookCategoryDetailsRequest> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("The provided ID cannot be empty.", nameof(id));
            
            var selectedCategory = await _repository.GetByIdAsync(id);

            if (selectedCategory == null)
                throw new KeyNotFoundException($"No category found with ID: {id}");
            return _mapper.Map<BookCategoryDetailsRequest>(selectedCategory);
        }
    }
}
