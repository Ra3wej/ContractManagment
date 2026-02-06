using ContractManagment.Api.Data;
using ContractManagment.Api.DTOs;
using ContractManagment.Api.DTOs.ContractsDTOs;
using ContractManagment.Api.DTOs.ContractsDTOs.ContractDocumentsDTOs;
using ContractManagment.Api.Exstensions;
using ContractManagment.Api.Models.ContractsModels;
using ContractManagment.Api.Models.ContractsModels.ContractDocumentsModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

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

    public async Task<ServiceResult<GetOneContractDto?>> GetOneContractAsync(Guid contractNumber)
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

    public async Task<ServiceResult<Guid?>> CreateNewContractAsync(AddContractsDto addDto)
    {
        if (addDto.StartDate >= addDto.EndDate)
            return ServiceResult<Guid?>.Failure("Start Date should be before end date.");
        if (addDto.ContractValue < 0)
            return ServiceResult<Guid?>.Failure("contract value should not be negative.");
        if (!await _context.Categories.AnyAsync(c => c.StatusIsActive && !c.IsDeleted && c.Id == addDto.CategoryId))
            return ServiceResult<Guid?>.Failure("contract category should be an active category.");
        if (addDto.Status is ContractStatus.Active or ContractStatus.Completed)
        {
            if (!await _context.Clients.AnyAsync(c => c.StatusIsActive && c.Id == addDto.ClinetId))
                return ServiceResult<Guid?>.Failure("contract client should be a valid client.");
        }
        var newContract = new Contracts
        {
            ClinetId = addDto.ClinetId,
            CategoryId = addDto.CategoryId,
            ContractType = addDto.ContractType,
            ContractValue = addDto.ContractValue,
            Title = addDto.Title,
            Description = addDto.Description,
            EndDate = addDto.EndDate,
            Notes = addDto.Notes,
            StartDate = addDto.StartDate,
        };
        newContract.UpdateContractStatus(addDto.Status);
        await _context.Contracts.AddAsync(newContract);
        await _context.SaveChangesAsync();

        return ServiceResult<Guid?>.Success(newContract.ContractNumber);
    }

    public async Task<ServiceResult<bool>> UpdateContractAsync(
      Guid contractNumber,
      UpdateContractsDto updateDto)
    {
        if (updateDto.StartDate >= updateDto.EndDate)
            return ServiceResult<bool>.Failure("Start Date should be before end date.");

        if (updateDto.ContractValue < 0)
            return ServiceResult<bool>.Failure("Contract value should not be negative.");

        var contract = await _context.Contracts
            .FirstOrDefaultAsync(c =>
                (c.Status == ContractStatus.Active || c.Status == ContractStatus.Active) &&
                c.ContractNumber == contractNumber &&
                !c.IsDeleted);

        if (contract == null)
            return ServiceResult<bool>.Failure("Contract not found.");

        if (contract.Status is ContractStatus.Completed or ContractStatus.Cancelled)
            return ServiceResult<bool>.Failure("Cancelled or Completed contracts cannot be updated.");



        if (!await _context.Categories.AnyAsync(c =>
                c.Id == updateDto.CategoryId &&
                c.StatusIsActive &&
                !c.IsDeleted))
            return ServiceResult<bool>.Failure("Contract category should be an active category.");




        contract.ClinetId = updateDto.ClinetId;
        contract.Title = updateDto.Title;
        contract.CategoryId = updateDto.CategoryId;
        contract.Description = updateDto.Description;
        contract.StartDate = updateDto.StartDate;
        contract.EndDate = updateDto.EndDate;
        contract.ContractValue = updateDto.ContractValue;
        contract.ContractType = updateDto.ContractType;
        contract.Notes = updateDto.Notes;

        await _context.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<bool>> DeleteContractAsync(Guid contractNumber)
    {
        var contract = await _context.Contracts
              .Include(c => c.ContractDocuments)
              .FirstOrDefaultAsync(c =>
                  c.ContractNumber == contractNumber &&
                  !c.IsDeleted);

        if (contract == null)
            return ServiceResult<bool>.Failure("Contract not found.");
        var deleted = contract.DeleteContractWithAssociatedDocuments();
        if (deleted.success is false)
        {
            return ServiceResult<bool>.Failure(deleted.message);
        }

        await _context.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<bool>> UpdateContractStatusAsync(Guid contractNumber, ContractStatus newContractStatus)
    {
        var contract = await _context.Contracts
       .FirstOrDefaultAsync(c =>
           c.ContractNumber == contractNumber &&
           !c.IsDeleted);

        if (contract == null)
            return ServiceResult<bool>.Failure("Contract not found.");

        if (newContractStatus is ContractStatus.Active or ContractStatus.Completed)
        {
            if (!await _context.Clients.AnyAsync(c =>
              c.Id == contract.ClinetId &&
              c.StatusIsActive ))
                return ServiceResult<bool>.Failure("Contract client should be a valid client.");
        }

        var updateStatusCheck = contract.UpdateContractStatus(newContractStatus);
        if (updateStatusCheck.success is false)
        {
            return ServiceResult<bool>.Failure(updateStatusCheck.message!);
        }
        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<Guid?>> CloneContractAsync(Guid contractNumber)
    {
        var contract = await _context.Contracts
            .AsNoTracking()
            .FirstOrDefaultAsync(c =>
                c.ContractNumber == contractNumber &&
                !c.IsDeleted);

        if (contract == null)
            return ServiceResult<Guid?>.Failure("Contract not found.");

        var clonedContract = new Contracts
        {
            ClinetId = contract.ClinetId,
            Title = contract.Title,
            CategoryId = contract.CategoryId,
            Description = contract.Description,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            ContractValue = contract.ContractValue,
            ContractType = contract.ContractType,
            Notes = contract.Notes,
            ClonedFromContractId = contract.Id,
        };
        /// Not sure if i need to clone the status as well or not?
        clonedContract.UpdateContractStatus(contract.Status);

        await _context.Contracts.AddAsync(clonedContract);
        await _context.SaveChangesAsync();

        return ServiceResult<Guid?>.Success(clonedContract.ContractNumber);
    }

}
