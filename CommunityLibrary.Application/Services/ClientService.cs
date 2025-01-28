using AutoMapper;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Pagination;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Domain;
using System.Linq.Expressions;

namespace CommunityLibrary.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IGenericRepository<Client> _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IGenericRepository<Client> clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientDetailsRequest> DeleteAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Client with ID {id} not found.");
            }

            await _clientRepository.DeleteAsync(client);

            return _mapper.Map<ClientDetailsRequest>(client);
        }

        public async Task<PaginatedResultService<ClientDetailsRequest>> GetAllAsync(
            Expression<Func<ClientDetailsRequest, bool>>? predicate,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            Expression<Func<Client, bool>>? domainPredicate = null;
            if (predicate != null)
            {
                domainPredicate = ExpressionMapper.MapPredicate<ClientDetailsRequest, Client>(predicate);
            }

            var paginatedEntities = await _clientRepository.GetAllAsync(
                domainPredicate,
                pageNumber,
                pageSize,
                cancellationToken
            );

            return _mapper.Map<PaginatedResultService<ClientDetailsRequest>>(paginatedEntities);  
        }

        public async Task<ClientDetailsRequest> GetByIdAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Client with ID {id} not found.");
            }

            return _mapper.Map<ClientDetailsRequest>(client);
        }

        public async Task<ClientDetailsRequest> InsertAsync(ClientCreateRequest request)
        {
            var client = _mapper.Map<Client>(request);
            client.Id = Guid.NewGuid(); // Assuming the Id should be generated automatically

            await _clientRepository.InsertAsync(client);

            return _mapper.Map<ClientDetailsRequest>(client);
        }

        public async Task<ClientDetailsRequest> UpdateAsync(ClientUpdateRequest request)
        {
            var client = await _clientRepository.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Client with ID {request.Id} not found.");
            }

            _mapper.Map(request, client);
            await _clientRepository.UpdateAsync(client);

            return _mapper.Map<ClientDetailsRequest>(client);
        }
    }
}
