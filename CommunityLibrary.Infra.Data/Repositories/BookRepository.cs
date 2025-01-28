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

        public async Task<PaginatedResponse<Book>> GetAllAsync(
           Expression<Func<Book, bool>>? predicate = null,
           int pageNumber = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default)
        {
            IQueryable<Book> books = _context.Books
                .Include(a => a.Author)
                .Include(c => c.BookCategory)
                .Include(c => c.RegisteredByUser)
                .AsNoTracking();

            if (predicate != null)
            {
                books = books.Where(predicate);
            }
            int totalItems = await books.CountAsync(cancellationToken);

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = await books
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResponse<Book>
            {
                Items = result,
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
          var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookCategory)
                .Include(c => c.RegisteredByUser)
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
            var existingEntity = await _context.Books
                .Include(c => c.RegisteredByUser)
                .FirstOrDefaultAsync(u => u.Id == entity.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Book with ID {entity.Id} not found.");
            entity.RegisteredByUser = existingEntity.RegisteredByUser;
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
