using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace CommunityLibrary.Infra.Data.Repositories
{
    public class UserRepository : IGenericRepository<User>
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> DeleteAsync(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return user;    
        }

        public async Task<User> InsertAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            var existingEntity = await _context.Users.FindAsync(entity.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"User with ID {entity.Id} not found.");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        //public async Task<PaginatedResponse<User>> GetAllAsync(Func<User, bool>? predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        //{

        //    var users = await _context.Users
        //                              .AsNoTracking()
        //                              .ToListAsync(cancellationToken);

        //    if (predicate != null)
        //    {
        //        users = users.Where(predicate).ToList();
        //    }


        //    int totalItems = users.Count;


        //    int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        //    var result =  users
        //        .OrderBy(u => u.Id) 
        //        .Skip((pageNumber - 1) * pageSize) 
        //        .Take(pageSize) 
        //        .ToList();

        //    return new PaginatedResponse<User>
        //    {
        //        Items = result,
        //        TotalItems = totalItems,
        //        TotalPages = totalPages,
        //        PageSize = pageSize,
        //        CurrentPage = (pageNumber - 1) * pageSize

        //    };
        //}
        public async Task<PaginatedResponse<User>> GetAllAsync(
    Func<User, bool>? predicate,
    int pageNumber,
    int pageSize,
    CancellationToken cancellationToken)
        {
            var query = _context.Users.AsNoTracking();

            if (predicate != null)
            {
                // Para permitir o uso de Linq-to-SQL, convertendo o predicado em IQueryable
                query = query.Where(u => predicate(u));
            }

            int totalItems = await query.CountAsync(cancellationToken);

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = await query
                .OrderBy(u => u.Id) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResponse<User>
            {
                Items = result,
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

    }
}
