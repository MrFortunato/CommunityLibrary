using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<BookCategory>> GetAllAsync(
            Func<BookCategory, bool> predicate,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
  
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "A filtering condition is required.");
            }

            // Obtém todos os dados sem rastreamento.
            var allData = await _context.BookCategories
                .AsNoTracking()
                .ToListAsync(cancellationToken);

   
            var filteredData = allData.Where(predicate);

  
            var paginatedData = filteredData
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return paginatedData;
        }


        public async Task<BookCategory> GetByIdAsync(Guid id)
        {
            var categoryBook = await _context.BookCategories
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
            var existingEntity = await _context.BookCategories.FindAsync(entity.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"BookCategory with ID {entity.Id} not found.");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
