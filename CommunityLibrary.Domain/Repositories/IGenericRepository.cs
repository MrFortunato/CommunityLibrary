﻿using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System.Linq.Expressions;

namespace CommunityLibrary.Domain
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<PaginatedResponse<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);
    }
}
