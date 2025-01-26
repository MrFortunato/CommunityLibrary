using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;

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
          Func<BookRental, bool>? predicate = null,
          int pageNumber = 1,
          int pageSize = 10,
          CancellationToken cancellationToken = default)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than 0.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");

            var allData = await _context.BookRentals
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var filteredData = predicate != null
                ? allData.Where(predicate)
                : allData;

            var paginatedData = filteredData
                .OrderBy(x => x.Id) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return paginatedData;
        }


        public async Task<BookRental> GetByIdAsync(Guid id)
        {
            var bookRental = await _context.BookRentals
                .Include(br => br.Book)
                .Include(br => br.Client)
                .Include(br => br.RegisteredByUser)
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

        Task<Domain.PaginatedResponse<BookRental>> IGenericRepository<BookRental>.GetAllAsync(Func<BookRental, bool>? predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
