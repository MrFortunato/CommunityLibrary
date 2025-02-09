using AutoMapper;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Pagination;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Application.Request.Rental;
using CommunityLibrary.Domain;
using CommunityLibrary.Domain.Repositories;
using System;
using System.Linq.Expressions;

namespace CommunityLibrary.Application.Services
{
    public class BookRentalService : IBookRentalService
    {
        private readonly IBookRentalRepository _bookRentalService;
        private readonly IMapper _mapper;
        public BookRentalService(IBookRentalRepository bookRentalRepository, IMapper mapper)
        {
            _bookRentalService = bookRentalRepository;
            _mapper = mapper;
        }
        public async Task<BookRentalDetailsRequest> DeleteAsync(Guid id)
        {
            var bookRental = await _bookRentalService.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Rental with ID {id} not found.");
            var deletedBookRental = await _bookRentalService.DeleteAsync(bookRental);
            return _mapper.Map<BookRentalDetailsRequest>(deletedBookRental);

        }

        public async Task<PaginatedResultService<BookRentalDetailsRequest>> GetAllAsync(Expression<Func<BookRentalDetailsRequest, bool>>? predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            Expression<Func<BookRental, bool>>? domainPredicate = null;
            if (predicate != null)
            {
                domainPredicate = ExpressionMapper.MapPredicate<BookRentalDetailsRequest, BookRental>(predicate);
            }

            var paginatedEntities = await _bookRentalService.GetAllAsync(
                domainPredicate,
                pageNumber,
                pageSize,
                cancellationToken
            );

            return _mapper.Map<PaginatedResultService<BookRentalDetailsRequest>>(paginatedEntities);
        }

        public async Task<BookRentalDetailsRequest> GetByIdAsync(Guid id)
        {
            var bookRental = await _bookRentalService.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Rental with ID {id} not found.");
            return _mapper.Map<BookRentalDetailsRequest>(bookRental);
        }

        public async Task<BookRentalDetailsRequest> InsertAsync(BookRentalCreateRequest request)
        {
            var bookRental = _mapper.Map<BookRental>(request);
            await _bookRentalService.InsertAsync(bookRental);
            return _mapper.Map<BookRentalDetailsRequest>(bookRental);
        }

        public async Task<BookRentalDetailsRequest> UpdateAsync(BookRentalCreateRequest request)
        {
            var bookRental = await _bookRentalService.GetByIdAsync(request.ClientId) ?? throw new KeyNotFoundException($"Rental with ID {request.ClientId} not found.");
            await _bookRentalService.UpdateAsync(bookRental);
            return _mapper.Map<BookRentalDetailsRequest>(bookRental);
        }
    }
}
