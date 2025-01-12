using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace CommunityLibrary.Infra.Data.Repositories
{
    public class ClientRepository : IGenericRepository<Client>
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }   
        public async Task<Client> DeleteAsync(Client entity)
        {
            _context.Clients.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;  
        }

        public async Task<IEnumerable<Client>> GetAllAsync(
             Func<Client, bool>? predicate = null,
             int pageNumber = 1,
             int pageSize = 10,
             CancellationToken cancellationToken = default)
        {
            var clients = await _context.Clients
                .Include(u => u.User)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (predicate != null)
            {
                clients = clients.Where(predicate).ToList();
            }
            return clients
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        public async Task<Client> GetByIdAsync(Guid id)
        {
            var client = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
            if (client is null)
            {
                throw new KeyNotFoundException($"Client with ID {id} not found."); 
            }

            return client;
        }

        public async Task<Client> InsertAsync(Client entity)
        {
            await _context.Clients.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Client> UpdateAsync(Client entity)
        {
            var existingEntity = _context.Clients.FirstAsync(x => x.Id == entity.Id);
            if (existingEntity is null)
            {
                throw new KeyNotFoundException($"Client with ID {entity.Id} not found.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
