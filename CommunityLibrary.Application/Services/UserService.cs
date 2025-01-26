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


        public async Task<UserDetailsRequest> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return _mapper.Map<UserDetailsRequest>(entity);
        }

        public async Task<UserDetailsRequest> InsertAsync(UserCreateRequest entity)
        {
            var domainEntity = _mapper.Map<User>(entity);
            domainEntity.Id = Guid.NewGuid();
            var result = await _repository.InsertAsync(domainEntity);

            return _mapper.Map<UserDetailsRequest>(result);
        }

        public async Task<UserDetailsRequest> UpdateAsync(UserUpdateRequest entity)
        {
            var domainEntity = await _repository.GetByIdAsync(entity.Id);
            if (domainEntity == null)
            {
                throw new KeyNotFoundException($"User with ID {entity.Id} not found.");
            }
            var user = _mapper.Map<User>(entity);
            user.LastModifiedDate = DateTime.UtcNow;
            var updatedEntity = await _repository.UpdateAsync(user);
            return _mapper.Map<UserDetailsRequest>(updatedEntity);
        }

        public async Task<PaginatedResultService<UserDetailsRequest>> GetAllAsync(Func<UserDetailsRequest, bool>? predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            // Define uma função para aplicar o filtro no domínio
            bool domainPredicate(User user)
            {
                if (predicate == null)
                {
                    return true;
                }
                return predicate(_mapper.Map<UserDetailsRequest>(user));
            }

            // Obtém todas as entidades que atendem ao filtro
            var allEntities = await _repository.GetAllAsync(domainPredicate, pageNumber, pageSize, cancellationToken);
            var mappedEntities = _mapper.Map<PaginatedResultService<UserDetailsRequest>>(allEntities);

            return mappedEntities;
        }
    }
}
