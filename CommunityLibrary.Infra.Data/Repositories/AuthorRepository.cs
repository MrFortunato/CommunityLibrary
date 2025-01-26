using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CommunityLibrary.Infra.Data.Repositories
{
    public class AuthorRepository : IGenericRepository<Author>
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Author> DeleteAsync(Author entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Authors.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Author>> GetAllAsync(
            Func<Author, bool>? predicate = null,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
       
            var authors = await _context.Authors
                .Include(u => u.RegisteredByUser)
                .Include(b => b.Books)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (predicate != null)
            {
                authors = authors.Where(predicate).ToList();
            }
            return  authors
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<Author> GetByIdAsync(Guid id)
        {
            var author = await _context.Authors.Include(u => u.RegisteredByUser)
                                .Include(b => b.Books)
                                .FirstOrDefaultAsync(a => a.Id == id);
            if (author == null) throw new KeyNotFoundException($"Author with ID {id} not found.");
            return author;
        }

        public async Task<Author> InsertAsync(Author entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _context.Authors.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Author> UpdateAsync(Author entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var existingAuthor = await _context.Authors.FindAsync(entity.Id);
            if (existingAuthor == null) throw new KeyNotFoundException($"Author with ID {entity.Id} not found.");

            _context.Entry(existingAuthor).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingAuthor;
        }
    }
}
