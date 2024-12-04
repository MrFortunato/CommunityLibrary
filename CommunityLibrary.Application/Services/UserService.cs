using AutoMapper;
using CommunityLibrary.Application.DTO;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Domain;
using System.Linq.Expressions;

namespace CommunityLibrary.Application.Services
{
    public class UserService : IGenerecService<UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _repository;

        public UserService(IMapper mapper, IGenericRepository<User> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UserDto> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            await _repository.DeleteAsync(entity);
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync(
            Expression<Func<UserDto, bool>>? predicate = null,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var domainPredicate = _mapper.Map<Expression<Func<User, bool>>>(predicate);

            var entities = await _repository.GetAllAsync(domainPredicate, pageNumber, pageSize, cancellationToken);
            return entities.Select(e => _mapper.Map<UserDto>(e));
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> InsertAsync(UserDto entity)
        {
            var domainEntity = _mapper.Map<User>(entity);

            var result = await _repository.InsertAsync(domainEntity);

            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> UpdateAsync(UserDto entity)
        {
            var domainEntity = await _repository.GetByIdAsync(entity.Id);
            if (domainEntity == null)
            {
                throw new KeyNotFoundException($"User with ID {entity.Id} not found.");
            }

            _mapper.Map(entity, domainEntity);
            var updatedEntity = await _repository.UpdateAsync(domainEntity);

            return _mapper.Map<UserDto>(updatedEntity);
        }
    }
}
