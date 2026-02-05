using ContractManagment.Api.Data;
using ContractManagment.Api.DTOs;
using ContractManagment.Api.DTOs.ContractsDTOs;
using ContractManagment.Api.DTOs.ContractsDTOs.ContractDocumentsDTOs;
using ContractManagment.Api.Exstensions;
using ContractManagment.Api.Models.ContractsModels;
using Microsoft.EntityFrameworkCore;

namespace ContractManagment.Api.Services.ContractServices;

public class ContractsServices : IContractsServices
{
    private readonly ApplicationDbContext _context;

    public ContractsServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<List<GetContractsDto>>> GetAllContractsAsync(int skip, int take)
    {
        var contracts = await _context.Contracts
            .Skip(skip)
            .Take(take)
            .Select(c => new GetContractsDto
            {
                Category = c.Category.Name,
                Clinet = c.Clinet.ClientName,
                ContractNumber = c.ContractNumber,
                ContractType = c.ContractType.ToString(),
                ContractValue = c.ContractValue,
                Status = c.Status.ToString(),
                Title = c.Title,
                EndDate = c.EndDate.ToCustomDateTime(),
                StartDate = c.StartDate.ToCustomDateTime(),
            })
            .ToListAsync();

        return ServiceResult<List<GetContractsDto>>.Success(contracts);
    }

    public async Task<ServiceResult<int>> GetContractsCountAsync()
    {
        var count = await _context.Contracts.CountAsync();
        return ServiceResult<int>.Success(count);
    }

    public async Task<ServiceResult<GetOneContractDto?>> GetOneContract(Guid contractNumber)
    {
        var contract = await _context.Contracts
            .Where(c => c.ContractNumber == contractNumber)
            .Select(c => new GetOneContractDto
            {
                Category = new BaseSelectDto<int>
                {
                    Id = c.CategoryId,
                    Name = c.Category.Name
                },
                Clinet = new BaseSelectDto<int>
                {
                    Id = c.ClinetId,
                    Name = c.Clinet.ClientName
                },
                ContractNumber = c.ContractNumber,
                ContractType = c.ContractType.ToString(),
                ContractValue = c.ContractValue,
                Status = c.Status.ToString(),
                Title = c.Title,
                Description = c.Description,
                EndDate = c.EndDate.ToCustomDateTime(),
                StartDate = c.StartDate.ToCustomDateTime(),
                Notes = c.Notes,
                ContractDocuments = c.ContractDocuments.Select(d => new GetSmallContractDocumentDto
                {
                    Id = d.Id,
                    DocumentName = d.DocumentName,
                    DocumentType = d.DocumentType.Name,
                    FilePath = d.FilePath
                }).ToList()
            })
            .SingleOrDefaultAsync();

        if (contract == null)
            return ServiceResult<GetOneContractDto?>.Failure("Contract not found");

        return ServiceResult<GetOneContractDto?>.Success(contract);
    }

    public async Task<ServiceResult<Guid?>> CreateNewContract(AddContractsDto addDto)
    {
        if (addDto.StartDate! < addDto.EndDate)
            return ServiceResult<Guid?>.Failure("Start Date should be before end date.");
        if (addDto.ContractValue < 0)
            return ServiceResult<Guid?>.Failure("contract value should not be negative.");
        if (!await _context.Categories.AnyAsync(c => c.StatusIsActive && !c.IsDeleted && c.Id == addDto.CategoryId))
            return ServiceResult<Guid?>.Failure("contract category should be an active category.");
        if (!await _context.Clients.AnyAsync(c => c.StatusIsActive && !c.IsDeleted && c.Id == addDto.ClinetId))
            return ServiceResult<Guid?>.Failure("contract client should be a valid client.");
        var newContract = new Contracts
        {
            ClinetId = addDto.ClinetId,
            CategoryId = addDto.CategoryId,
            ContractType = addDto.ContractType,
            ContractValue = addDto.ContractValue,
            Description = addDto.Description,
            EndDate = addDto.EndDate,
            Notes = addDto.Notes,
            StartDate = addDto.StartDate,
        };
        await _context.Contracts.AddAsync(newContract);
        await _context.SaveChangesAsync();

        return ServiceResult<Guid?>.Success(newContract.ContractNumber);
    }

    public async Task<ServiceResult<Guid?>> UpdateContract(UpdateContractsDto updateDto)
    {
        // TODO: fetch contract, apply changes, save
        return ServiceResult<Guid?>.Failure("Not implemented yet");
    }

    public async Task<ServiceResult<Guid?>> DeleteContract(Guid contractNumber)
    {
        // TODO: soft delete / validation based on status
        return ServiceResult<Guid?>.Failure("Not implemented yet");
    }

    public async Task<ServiceResult<Guid?>> UpdateContractStatus(Guid contractNumber)
    {
        // TODO: apply state rules (Draft → Active → Completed, etc.)
        return ServiceResult<Guid?>.Failure("Not implemented yet");
    }

    public async Task<ServiceResult<Guid?>> CloneContract(Guid contractNumber)
    {
        // TODO: duplicate contract + documents with new ContractNumber
        return ServiceResult<Guid?>.Failure("Not implemented yet");
    }
}
