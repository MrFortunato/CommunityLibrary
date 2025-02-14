using CommunityLibrary.Domain;
using CommunityLibrary.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CommunityLibrary.Infra.Data.Repositories
{
    public class BookRentalRepository : IBookRentalRepository
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
            var existingEntity = await _context.BookRentals
                                               .FirstOrDefaultAsync(x=>x.Id.Equals(entity.Id));
            if (existingEntity == null)
                throw new KeyNotFoundException($"Rental with ID {entity.Id} not found.");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PaginatedResponse<BookRental>> GetAllAsync(Expression<Func<BookRental, bool>>? predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<BookRental> bookRental =  _context.BookRentals
                                                         .Include(c => c.Client)
                                                         .ThenInclude(u => u.User)
                                                         .Include(b => b.Book)
                                                         .Include(u => u.RegisteredByUser)
                                                         .AsNoTracking();

            if (predicate != null)
            {
                bookRental = bookRental.Where(predicate);
            }
            int totalItems = await bookRental.CountAsync(cancellationToken);

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = await bookRental
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResponse<BookRental>
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
