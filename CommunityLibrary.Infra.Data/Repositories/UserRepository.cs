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

        public async Task<IEnumerable<User>> GetAllAsync(
        Func<User, bool>? predicate = null,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
        {
            
            var users = await _context.Users.AsNoTracking().ToListAsync(cancellationToken);

            if (predicate != null)
            {
                users = users.Where(predicate).ToList();
            }
            return users
                .OrderBy(u => u.Id) 
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize) 
                .ToList(); 
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
    }
}
