using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<PaginatedResponse<Client>> GetAllAsync(Expression<Func<Client, bool>>? predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<Client> clients =  _context.Clients
                                                  .Include(u => u.User)
                                                  .AsNoTracking(); 

            if (predicate != null)
            {
                clients = clients.Where(predicate);
            }
            int totalItems = await clients.CountAsync(cancellationToken);

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = await clients
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResponse<Client>
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
