using ContractManagment.Api.DTOs.ClientDTOs;

namespace ContractManagment.Api.Services.ClientServices;

public interface IClientsServices
{

    Task<ServiceResult<List<GetClientDto>>> GetAllClientsAsync(int skip, int take);

    Task<ServiceResult<int>> GetClientsCountAsync();
    Task<ServiceResult<GetClientStatisticsDto>> GetClientStatisticsAsync(int clientId);

    Task<ServiceResult<GetOneClientDto?>> GetClientByIdAsync(int clientId);

    Task<ServiceResult<int?>> CreateClientAsync(AddClientDto addDto);
    Task<ServiceResult<bool>> UpdateClientAsync(int clientId, UpdateClientDto updateDto);

    Task<ServiceResult<bool>> UpdateClientStatusAsync(int clientId);
}
