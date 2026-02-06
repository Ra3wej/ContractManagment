using ContractManagment.Api.DTOs.ContractsDTOs;
using ContractManagment.Api.Models.ContractsModels;

namespace ContractManagment.Api.Services.ContractServices;

public interface IContractsServices
{
    Task<ServiceResult<List<GetContractsDto>>> GetAllContractsAsync(int skip, int take);
    Task<ServiceResult<int>> GetContractsCountAsync();
    public Task<ServiceResult<GetOneContractDto?>> GetOneContractAsync(Guid contractNumber);
    public Task<ServiceResult<Guid?>> CreateNewContractAsync(AddContractsDto addDto);
    public Task<ServiceResult<bool>> UpdateContractAsync(Guid contractNumber,UpdateContractsDto updateDto);
    public Task<ServiceResult<bool>> DeleteContractAsync(Guid contractNumber);
    public Task<ServiceResult<bool>> UpdateContractStatusAsync(Guid contractNumber, ContractStatus newContractStatus);
    public Task<ServiceResult<Guid?>> CloneContractAsync(Guid contractNumber);
}


