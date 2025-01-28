using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CommunityLibrary.Infra.Data
{
    public class BookCategoryRepository : IGenericRepository<BookCategory>
    {
        private readonly AppDbContext _context;

        public BookCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BookCategory> DeleteAsync(BookCategory entity)
        {
            _context.BookCategories.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BookCategory> GetByIdAsync(Guid id)
        {
            var categoryBook = await _context.BookCategories
                                             .Include(c => c.RegisteredByUser)
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(bc => bc.Id.Equals(id));

            if (categoryBook == null) 
            {
                throw new KeyNotFoundException($"BookCategory with ID {id} not found.");
            }

            return categoryBook;
        }

        public async Task<BookCategory> InsertAsync(BookCategory entity)
        {
            await _context.BookCategories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BookCategory> UpdateAsync(BookCategory entity)
        {
            var existingEntity = await _context.BookCategories
                                               .Include(bc => bc.RegisteredByUser)
                                               .FirstOrDefaultAsync(u => u.Id == entity.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"BookCategory with ID {entity.Id} not found.");
            entity.RegisteredByUser = existingEntity.RegisteredByUser;
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PaginatedResponse<BookCategory>> GetAllAsync(Expression<Func<BookCategory, bool>>? predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<BookCategory> bookCategory =  _context.BookCategories
                                                             .Include(c => c.RegisteredByUser)
                                                             .AsNoTracking();

            if (predicate != null)
            {
                bookCategory = bookCategory.Where(predicate);
            }
            int totalItems = await bookCategory.CountAsync(cancellationToken);

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = await bookCategory
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResponse<BookCategory>
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
