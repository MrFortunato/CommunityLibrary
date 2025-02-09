using CommunityLibrary.Domain;
using CommunityLibrary.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CommunityLibrary.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
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
            IQueryable<User> user =  _context.Users
                                             .AsNoTracking();
            return await user.FirstAsync(u => u.Id == id);     
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


    public async Task<PaginatedResponse<User>> GetAllAsync(
    Expression<Func<User, bool>>? predicate,
    int pageNumber,
    int pageSize,
    CancellationToken cancellationToken)
        {
            IQueryable<User> query = _context.Users.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
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

        public async Task<User> GetUserByEmailAsync(string email)
        {
            IQueryable<User> query = _context.Users.AsNoTracking();
            query = query.Where(u => u.Email == email);
            var user = await query.FirstOrDefaultAsync();
            return user ?? new User();
        }


        public async Task<User> SignInUserAsync(User entity)
        {
            IQueryable<User> query = _context.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(entity.Name))
            {
                query = query.Where(u => u.Email == entity.Name);
            }
            if (!string.IsNullOrEmpty(entity.Password))
            {
                query = query.Where(u => u.Password == entity.Password);
            }
            var user = await query.FirstOrDefaultAsync();

            return user ?? new User(); 
        }

    }
}
