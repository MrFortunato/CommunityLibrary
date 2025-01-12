using AutoMapper;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Domain;

namespace CommunityLibrary.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _repository;

        public UserService(IMapper mapper, IGenericRepository<User> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UserDetailsRequest> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            await _repository.DeleteAsync(entity);
            return _mapper.Map<UserDetailsRequest>(entity);
        }

        public async Task<IEnumerable<UserDetailsRequest>> GetAllAsync(
            Func<UserDetailsRequest, bool>? predicate = null,
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
                return predicate(_mapper.Map<UserDetailsRequest>(user));
            }

            var entities = await _repository.GetAllAsync(domainPredicate, pageNumber, pageSize, cancellationToken);
            return entities.Select(e => _mapper.Map<UserDetailsRequest>(e));
        }

        public async Task<UserDetailsRequest> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return _mapper.Map<UserDetailsRequest>(entity);
        }

        public async Task<UserCreateRequest> InsertAsync(UserCreateRequest entity)
        {
            var domainEntity = _mapper.Map<User>(entity);
            domainEntity.Id = Guid.NewGuid();
            var result = await _repository.InsertAsync(domainEntity);

            return _mapper.Map<UserCreateRequest>(result);
        }

        public async Task<UserUpdateRequest> UpdateAsync(UserUpdateRequest entity)
        {
            var domainEntity = await _repository.GetByIdAsync(entity.Id);
            if (domainEntity == null)
            {
                throw new KeyNotFoundException($"User with ID {entity.Id} not found.");
            }
            var user = _mapper.Map<User>(entity);
            user.LastModifiedDate = DateTime.UtcNow;
            var updatedEntity = await _repository.UpdateAsync(user);
            return _mapper.Map<UserUpdateRequest>(updatedEntity);
        }
    }
}
