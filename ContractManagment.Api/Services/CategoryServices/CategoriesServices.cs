using ContractManagment.Api.Data;
using ContractManagment.Api.DTOs;
using ContractManagment.Api.DTOs.ContractsDTOs;
using ContractManagment.Api.Exstensions;
using ContractManagment.Api.Models;
using ContractManagment.Api.Models.ContractsModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContractManagment.Api.Services.CategoryServices
{
    public class CategoriesServices: ICategoriesServices
    {
        private readonly ApplicationDbContext _context;

        public CategoriesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<int>> CreateCategoryAsync(AddCategoriesDto dto)
        {
            var exists = await _context.Categories
                .AnyAsync(c => c.Name == dto.Name && !c.IsDeleted);

            if (exists)
                return ServiceResult<int>.Failure("Category already exists.");

            var category = new Categories
            {
                Name = dto.Name,
                Description = dto.Description,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return ServiceResult<int>.Success(category.Id);
        }

        public async Task<ServiceResult<List<GetCategoriesDto>>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories
                .Where(c => !c.IsDeleted)
                .Select(c => new GetCategoriesDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                })
                .ToListAsync();

            return ServiceResult<List<GetCategoriesDto>>.Success(categories);
        }

        public async Task<ServiceResult<GetOneCategoryDto?>> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Contracts)
                .Where(c => c.Id == id && !c.IsDeleted)
                .Select(c => new GetOneCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Contracts=c.Contracts.Select(s=>new GetContractsDto
                    {
                        Category=c.Name,
                        Clinet=s.Clinet.ClientName,
                        ContractNumber=s.ContractNumber,
                        ContractType=s.ContractType.ToString(),
                        ContractValue=s.ContractValue,
                        EndDate=s.EndDate.ToCustomDateTime(),
                        StartDate=s.StartDate.ToCustomDateTime(),
                        Status=s.Status.ToString(),
                        Title=s.Title,
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (category == null)
                return ServiceResult<GetOneCategoryDto?>.Failure("Category not found.");

            return ServiceResult<GetOneCategoryDto?>.Success(category);
        }

        public async Task<ServiceResult<bool>> UpdateCategoryAsync(int id, UpdateCategoriesDto dto)
        {
            var category = await _context.Categories
                .Include(c => c.Contracts)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (category == null)
                return ServiceResult<bool>.Failure("Category not found.");

            var hasActiveContracts = category.Contracts
                .Any(c => c.Status == ContractStatus.Active);

            if (hasActiveContracts)
                return ServiceResult<bool>.Failure("Cannot update category with active contracts.");

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _context.SaveChangesAsync();
            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<bool>> UpdateCategoryStatusAsync(int id, bool activate)
        {
            var category = await _context.Categories
                .Include(c => c.Contracts)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (category == null)
                return ServiceResult<bool>.Failure("Category not found.");

           
                var result = category.DeactivateCategory();
                if (!result.success)
                    return ServiceResult<bool>.Failure(result.errorMessage!);

            await _context.SaveChangesAsync();
            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<GetCategoryStatisticsDto>> GetCategoryStatisticsAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Contracts)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (category == null)
                return ServiceResult<GetCategoryStatisticsDto>.Failure("Category not found.");

            var totalContracts = category.Contracts.Count;
            var activeContracts = category.Contracts
                .Count(c => c.Status == ContractStatus.Active);

            var totalValue = category.Contracts
                .Where(c => c.Status == ContractStatus.Active)
                .Sum(c => c.ContractValue);

            var stats = new GetCategoryStatisticsDto
            {
                TotalContracts = totalContracts,
                ActiveContracts = activeContracts,
                TotalContractValue = totalValue
            };

            return ServiceResult<GetCategoryStatisticsDto>.Success(stats);
        }
    }
}
