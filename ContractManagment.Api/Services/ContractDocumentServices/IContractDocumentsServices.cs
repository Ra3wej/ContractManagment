using ContractManagment.Api.DTOs.ContractsDTOs.ContractDocumentsDTOs;

namespace ContractManagment.Api.Services.ContractDocumentServices;

public interface IContractDocumentsServices
{
   public Task<ServiceResult<int>> AddDocumentToContractAsync(
     Guid contractNumber,
     AddContractDocumentsDto addDto);

    public Task<ServiceResult<List<GetContractDocumentsDto>>> GetDocumentsByContractAsync(
        Guid contractNumber);

    public Task<ServiceResult<GetContractDocumentsDto?>> GetDocumentByIdAsync(int documentId);

    public Task<ServiceResult<bool>> UpdateDocumentAsync(
        int documentId,
        UpdateContractDocumentsDto updateDto);

    public Task<ServiceResult<bool>> DeleteDocumentAsync(int documentId);
}
