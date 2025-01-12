using AutoMapper;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Domain;

namespace CommunityLibrary.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IGenericRepository<Author> authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorDetailsRequest> DeleteAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with ID {id} not found.");
            }

            await _authorRepository.DeleteAsync(author);

            return _mapper.Map<AuthorDetailsRequest>(author);
        }

        public async Task<IEnumerable<AuthorDetailsRequest>> GetAllAsync(
            Func<AuthorDetailsRequest, bool>? predicate,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var authors = await _authorRepository.GetAllAsync(
                predicate: null,
                pageNumber: pageNumber,
                pageSize: pageSize,
                cancellationToken: cancellationToken
            );

            var result = _mapper.Map<IEnumerable<AuthorDetailsRequest>>(authors);

            return predicate == null ? result : result.Where(predicate);
        }

        public async Task<AuthorDetailsRequest> GetByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with ID {id} not found.");
            }

            return _mapper.Map<AuthorDetailsRequest>(author);
        }

        public async Task<AuthorDetailsRequest> InsertAsync(AuthorCreateRequest request)
        {
            var author = _mapper.Map<Author>(request);
            author.Id = Guid.NewGuid();
            await _authorRepository.InsertAsync(author);

            return _mapper.Map<AuthorDetailsRequest>(author);
        }

        public async Task<AuthorDetailsRequest> UpdateAsync(AuthorUpdateRequest request)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.Id} not found.");
            }

            _mapper.Map(request, author);
            await _authorRepository.UpdateAsync(author);

            return _mapper.Map<AuthorDetailsRequest>(author);
        }
    }
}
