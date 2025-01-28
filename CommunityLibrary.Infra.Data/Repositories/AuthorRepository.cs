using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using static System.Reflection.Metadata.BlobBuilder;

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

        public async Task<PaginatedResponse<Author>> GetAllAsync(Expression<Func<Author, bool>>? predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<Author> authors =  _context.Authors
                                                  .Include(u => u.RegisteredByUser)
                                                  .Include(b => b.Books)
                                                  .AsNoTracking();

            if (predicate != null)
            {
                authors = authors.Where(predicate);
            }
            int totalItems = await authors.CountAsync(cancellationToken);

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = await authors
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResponse<Author>
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
