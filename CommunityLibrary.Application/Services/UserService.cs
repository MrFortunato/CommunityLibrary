using AutoMapper;
using CommunityLibrary.Application.DTO;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Domain;

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
            Func<UserDto, bool>? predicate = null,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            bool domainPredicate(User user)
            {
                if (predicate == null)
                {
                    return true;
                }
                return predicate(_mapper.Map<UserDto>(user));
            }

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
            entity.Id = Guid.NewGuid(); 
            entity.LastModifiedDate = null;
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
            entity.LastModifiedDate = DateTime.UtcNow;
            var user = _mapper.Map<User>(entity);
            var updatedEntity = await _repository.UpdateAsync(user);
            return _mapper.Map<UserDto>(updatedEntity);
        }
    }
}
