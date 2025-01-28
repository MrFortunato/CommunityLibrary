using AutoMapper;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Pagination;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Domain;
using System.Linq.Expressions;

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
        public async Task<BookCategoryDetailsRequest> InsertAsync(BookCategoryCreateRequest entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The category cannot be null.");

            var bookCategory = _mapper.Map<BookCategory>(entity);
            var addedEntity = await _repository.InsertAsync(bookCategory);
            return _mapper.Map<BookCategoryDetailsRequest>(addedEntity);
        }
        public async Task<BookCategoryDetailsRequest> UpdateAsync(BookCategoryUpdateRequest entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The category cannot be null.");

            var bookCategory = _mapper.Map<BookCategory>(entity);
            bookCategory.LastModifiedDate = DateTime.UtcNow;
            var addedEntity = await _repository.UpdateAsync(bookCategory);
            return _mapper.Map<BookCategoryDetailsRequest>(addedEntity);
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

        public async Task<PaginatedResultService<BookCategoryDetailsRequest>> GetAllAsync(
            Expression<Func<BookCategoryDetailsRequest, bool>>? predicate = null,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            Expression<Func<BookCategory, bool>>? domainPredicate = null;
            if (predicate != null)
            {
                domainPredicate = ExpressionMapper.MapPredicate<BookCategoryDetailsRequest, BookCategory>(predicate);
            }

            var paginatedEntities = await _repository.GetAllAsync(
                domainPredicate,
                pageNumber,
                pageSize,
                cancellationToken
            );

            return  _mapper.Map<PaginatedResultService<BookCategoryDetailsRequest>>(paginatedEntities);
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
