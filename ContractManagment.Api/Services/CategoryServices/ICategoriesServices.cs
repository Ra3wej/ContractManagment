using ContractManagment.Api.DTOs;

namespace ContractManagment.Api.Services.CategoryServices;

public interface ICategoriesServices
{
    public Task<ServiceResult<int>> CreateCategoryAsync(AddCategoriesDto dto);

    public Task<ServiceResult<List<GetCategoriesDto>>> GetAllCategoriesAsync();

    public Task<ServiceResult<GetOneCategoryDto?>> GetCategoryByIdAsync(int id);

    public Task<ServiceResult<bool>> UpdateCategoryAsync(int id, UpdateCategoriesDto dto);

    public Task<ServiceResult<bool>> UpdateCategoryStatusAsync(int id);

    public Task<ServiceResult<GetCategoryStatisticsDto>> GetCategoryStatisticsAsync(int id);
}
