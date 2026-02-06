using ContractManagment.Api.DTOs.ContractsDTOs.ContractDocumentsDTOs;

namespace ContractManagment.Api.Services.ContractDocumentTypeServices
{
    public interface IContractDocumentTypesService
    {
        public Task<ServiceResult<List<GetContractDocumentTypesDto>>> GetAllDocumentTypesAsync();

        public Task<ServiceResult<GetContractDocumentTypesDto?>> GetByIdAsync(int id);

        public Task<ServiceResult<int>> CreateAsync(AddContractDocumentTypeDto dto);

        public Task<ServiceResult<bool>> UpdateAsync(UpdateContractDocumentTypeDto dto);

        //public Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
