using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CommunityLibrary.Infra.Data.Repositories
{
    public class BookRepository : IGenericRepository<Book>
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Book> DeleteAsync(Book entity)
        {
            _context.Books.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(
           Expression<Func<Book, bool>>? predicate = null,
           int pageNumber = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default)
        {
            IQueryable<Book> query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookCategory)
                .Include(b => b.RegisteredUser)
                .AsNoTracking();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = query
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
          var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookCategory) 
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }

            return book;
        }

        public async Task<Book> InsertAsync(Book entity)
        {
            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Book> UpdateAsync(Book entity)
        {
            var existingEntity = await _context.Books.FindAsync(entity.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Book with ID {entity.Id} not found.");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
