using AutoMapper;
using CommunityLibrary.Application.DTO;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Domain;
using System.Linq.Expressions;

namespace CommunityLibrary.Application.Services
{
    public class BookCategoryService : IGenerecService<BookCategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<BookCategory> _repository;  
        public BookCategoryService(IMapper mapper, IGenericRepository<BookCategory> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<BookCategoryDto> InsertAsync(BookCategoryDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity cannot be null.");

            var bookCategory = _mapper.Map<BookCategory>(entity);
            var addedEntity = await _repository.UpdateAsync(bookCategory);
            return _mapper.Map<BookCategoryDto>(addedEntity);
        }
        public async Task<BookCategoryDto> UpdateAsync(BookCategoryDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity cannot be null.");

            var bookCategory = _mapper.Map<BookCategory>(entity);
            var addedEntity = await _repository.UpdateAsync(bookCategory);
            return _mapper.Map<BookCategoryDto>(addedEntity);
        }
        public async Task<BookCategoryDto> DeleteAsync(BookCategoryDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity cannot be null.");

            var bookCategory = _mapper.Map<BookCategory>(entity);
            var addedEntity = await _repository.DeleteAsync(bookCategory);
            return _mapper.Map<BookCategoryDto>(addedEntity);
        }

        public async Task<IEnumerable<BookCategoryDto>> GetAllAsync(
         Expression<Func<BookCategoryDto, bool>>? predicate = null,
         int pageNumber = 1,
         int pageSize = 10,
         CancellationToken cancellationToken = default)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than zero.");

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

            var entities = await _repository.GetAllAsync(cancellationToken);

            var query = entities.AsQueryable();

            // Aplica o filtro, se fornecido
            if (predicate != null)
            {
                // Converte o predicate do DTO para a entidade
                //var entityPredicate = ConvertPredicate(predicate);
                //query = query.Where(entityPredicate);
            }

            var paginatedQuery = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return _mapper.Map<IEnumerable<BookCategoryDto>>(paginatedQuery);
        }

       
        public async Task<BookCategoryDto> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("The provided ID cannot be empty.", nameof(id));
            
            var selectedCategory = await _repository.GetByIdAsync(id);

            if (selectedCategory == null)
                throw new KeyNotFoundException($"No category found with ID: {id}");
            return _mapper.Map<BookCategoryDto>(selectedCategory);
        }
    }
}
