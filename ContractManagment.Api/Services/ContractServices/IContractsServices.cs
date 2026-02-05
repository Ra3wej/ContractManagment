using ContractManagment.Api.DTOs.ContractsDTOs;

namespace ContractManagment.Api.Services.ContractServices;

public interface IContractsServices
{
    Task<ServiceResult<List<GetContractsDto>>> GetAllContractsAsync(int skip, int take);
    Task<ServiceResult<int>> GetContractsCountAsync();
    public Task<ServiceResult<GetOneContractDto?>> GetOneContract(Guid contractNumber);
    public Task<ServiceResult<Guid?>> CreateNewContract(AddContractsDto addDto);
    public Task<ServiceResult<Guid?>> UpdateContract(Guid contractNumber,UpdateContractsDto updateDto);
    public Task<ServiceResult<Guid?>> DeleteContract(Guid contractNumber);
    public Task<ServiceResult<Guid?>> UpdateContractStatus(Guid contractNumber);
    public Task<ServiceResult<Guid?>> CloneContract(Guid contractNumber);
}


