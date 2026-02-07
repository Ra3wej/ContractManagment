using ContractManagment.Api.DTOs.ContractsDTOs;
using ContractManagment.Api.Models.ContractsModels;
using System.ComponentModel.DataAnnotations;

namespace ContractManagment.Api.DTOs;

public class GetOneCategoryDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public List<GetContractsDto> Contracts { get; set; } = [];
}

public class GetCategoriesDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }
}

public class AddCategoriesDto
{

    [Length(3, 100, ErrorMessage = "category name length should be between 3-100")]
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

}

public class UpdateCategoriesDto
{
     [Length(3, 100, ErrorMessage = "category name length should be between 3-100")]
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

}
public class GetCategoryStatisticsDto
{
    public int TotalContracts { get; set; }
    public int ActiveContracts { get; set; }
    public decimal TotalContractValue { get; set; }
}