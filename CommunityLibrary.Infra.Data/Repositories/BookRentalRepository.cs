using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace CommunityLibrary.Infra.Data.Repositories
{
    public class BookRentalRepository : IGenericRepository<BookRental>
    {
        private readonly AppDbContext _context;

        public BookRentalRepository(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<BookRental> DeleteAsync(BookRental entity)
        {
            _context.BookRentals.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;  
        }

        public async Task<IEnumerable<BookRental>> GetAllAsync(
          Expression<Func<BookRental, bool>>? predicate = null,
          int pageNumber = 1,
          int pageSize = 10,
          CancellationToken cancellationToken = default)
        {
            IQueryable<BookRental> query = _context.BookRentals
                .Include(br => br.Book)
                .Include(br => br.Client)
                .Include(br => br.User)
                .AsNoTracking().AsNoTracking();
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

        public async Task<BookRental> GetByIdAsync(Guid id)
        {
            var bookRental = await _context.BookRentals
                .Include(br => br.Book)
                .Include(br => br.Client)
                .Include(br => br.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(br => br.Id.Equals(id));

            if (bookRental == null)
            {
                throw new KeyNotFoundException($"Rental with ID {id} not found.");
            }

            return bookRental;
        }


        public async Task<BookRental> InsertAsync(BookRental entity)
        {
            _context.BookRentals.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BookRental> UpdateAsync(BookRental entity)
        {
            var existingEntity = await _context.BookRentals.FindAsync(entity.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Rental with ID {entity.Id} not found.");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
