using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
           Func<Book, bool>? predicate = null,
           int pageNumber = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default)
        {
            var books = await _context.Books.AsNoTracking().ToListAsync(cancellationToken);

            if (predicate != null)
            {
                books = books.Where(predicate).ToList();
            }
            return books
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
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
