using ContractManagment.Api.Data;
using ContractManagment.Api.DTOs.ContractsDTOs.ContractDocumentsDTOs;
using ContractManagment.Api.Models.ContractsModels.ContractDocumentsModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContractManagment.Api.Services.ContractDocumentTypeServices;

public class ContractDocumentTypesService: IContractDocumentTypesService
{
    private readonly ApplicationDbContext _context;

    public ContractDocumentTypesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<List<GetContractDocumentTypesDto>>> GetAllDocumentTypesAsync()
    {
        var types = await _context.ContractDocumentTypes
            .Select(t => new GetContractDocumentTypesDto
            {
                Id = t.Id,
                Name = t.Name
            })
            .ToListAsync();

        return ServiceResult<List<GetContractDocumentTypesDto>>.Success(types);
    }

    public async Task<ServiceResult<GetContractDocumentTypesDto?>> GetByIdAsync(int id)
    {
        var type = await _context.ContractDocumentTypes
            .Where(t => t.Id == id)
            .Select(t => new GetContractDocumentTypesDto
            {
                Id = t.Id,
                Name = t.Name
            })
            .FirstOrDefaultAsync();

        if (type == null)
            return ServiceResult<GetContractDocumentTypesDto?>.Failure("Document type not found.");

        return ServiceResult<GetContractDocumentTypesDto?>.Success(type);
    }

    public async Task<ServiceResult<int>> CreateAsync(AddContractDocumentTypeDto dto)
    {
        var exists = await _context.ContractDocumentTypes
            .AnyAsync(t => t.Name == dto.Name);

        if (exists)
            return ServiceResult<int>.Failure("Document type already exists.");

        var entity = new ContractDocumentType
        {
            Name = dto.Name
        };

        _context.ContractDocumentTypes.Add(entity);
        await _context.SaveChangesAsync();

        return ServiceResult<int>.Success(entity.Id);
    }

    public async Task<ServiceResult<bool>> UpdateAsync(UpdateContractDocumentTypeDto dto)
    {
        var entity = await _context.ContractDocumentTypes
            .FirstOrDefaultAsync(t => t.Id == dto.Id);

        if (entity == null)
            return ServiceResult<bool>.Failure("Document type not found.");

        var duplicate = await _context.ContractDocumentTypes
            .AnyAsync(t => t.Name == dto.Name && t.Id != dto.Id);

        if (duplicate)
            return ServiceResult<bool>.Failure("Document type name already exists.");

        entity.Name = dto.Name;

        await _context.SaveChangesAsync();
        return ServiceResult<bool>.Success(true);
    }

}
