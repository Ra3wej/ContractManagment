using ContractManagment.Api.DTOs.ClientDTOs;

namespace ContractManagment.Api.Services.IndustryServices;

public interface IIndustriesServices
{
    public Task<ServiceResult<List<GetIndustryDto>>> GetAllAsync();
    public Task<ServiceResult<GetIndustryDto>> GetByIdAsync(int id);
    public Task<ServiceResult<int>> CreateAsync(AddIndustryDto dto);
    public Task<ServiceResult<bool>> UpdateAsync(UpdateIndustryDto dto);
}
