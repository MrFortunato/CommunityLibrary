using AutoMapper;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Domain;

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

        public async Task<IEnumerable<ClientDetailsRequest>> GetAllAsync(
            Func<ClientDetailsRequest, bool>? predicate,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetAllAsync(
                predicate: null,
                pageNumber: pageNumber,
                pageSize: pageSize,
                cancellationToken: cancellationToken
            );

            var result = _mapper.Map<IEnumerable<ClientDetailsRequest>>(clients);

            return predicate == null ? result : result.Where(predicate);
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
