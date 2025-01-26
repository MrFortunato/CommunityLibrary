﻿using CommunityLibrary.Domain;
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

            var allData = await _context.BookCategories
                                        .Include(c => c.RegisteredByUser)
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

        Task<Domain.PaginatedResponse<BookCategory>> IGenericRepository<BookCategory>.GetAllAsync(Func<BookCategory, bool>? predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
