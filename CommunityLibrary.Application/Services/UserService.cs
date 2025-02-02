﻿using AutoMapper;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Pagination;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Domain;
using System.Linq.Expressions;

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

        public async Task<PaginatedResultService<UserDetailsRequest>> GetAllAsync(Expression<Func<UserDetailsRequest, bool>>? predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>>? domainPredicate = null;

            if (predicate != null)
            {
                domainPredicate = ExpressionMapper.MapPredicate<UserDetailsRequest, User>(predicate);
            }

            var paginatedEntities = await _repository.GetAllAsync(
                domainPredicate,
                pageNumber,
                pageSize,
                cancellationToken
            );

            return _mapper.Map<PaginatedResultService<UserDetailsRequest>>(paginatedEntities);

        }
    }
}
