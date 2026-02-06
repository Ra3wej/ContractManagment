using ContractManagment.Api.Data;
using ContractManagment.Api.DTOs.ContractsDTOs.ContractDocumentsDTOs;
using ContractManagment.Api.Exstensions;
using ContractManagment.Api.Models.ContractsModels;
using ContractManagment.Api.Models.ContractsModels.ContractDocumentsModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;

namespace ContractManagment.Api.Services.ContractDocumentServices;

public class ContractDocumentsServices : IContractDocumentsServices
{
    private readonly ApplicationDbContext _context;

    public ContractDocumentsServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<int>> AddDocumentToContractAsync(Guid contractNumber, AddContractDocumentsDto addDto)
    {
        var contract = await _context.Contracts.Where(c => c.ContractNumber == contractNumber && !c.IsDeleted)
                                               .Include(c => c.ContractDocuments).FirstOrDefaultAsync();
        if (contract == null)
            return ServiceResult<int>.Failure("Contract not found.");
        if (contract.ContractDocuments.Any(c => c.DocumentName == addDto.DocumentName))
            return ServiceResult<int>.Failure("Can not have duplicate file name for the same contract.");

        var newDocument = new ContractDocuments
        {
            DocumentName = addDto.DocumentName,
            ContractId = contract.Id,
            Description = contract.Description,
            UploadedBy = addDto.UploadedBy,
            FileSizeInBytes = addDto.FileSizeInBytes,
            DocumentTypeId = addDto.DocumentTypeId,
            FilePath = addDto.FilePath,
        };
        await _context.ContractDocuments.AddAsync(newDocument);
        await _context.SaveChangesAsync();

        return ServiceResult<int>.Success(newDocument.Id);
    }

    public async Task<ServiceResult<bool>> DeleteDocumentAsync(int documentId)
    {
        var document = await _context.ContractDocuments.Where(c => c.Id == documentId && !c.IsDeleted)
                                                       .Include(c => c.Contract).FirstOrDefaultAsync();
        if (document == null)
            return ServiceResult<bool>.Failure("document not found.");
        if (document.Contract.Status == ContractStatus.Completed)
            return ServiceResult<bool>.Failure("can not delete documents from completed contracts.");

        document.IsDeleted = true;

        await _context.SaveChangesAsync();
        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<GetContractDocumentsDto?>> GetDocumentByIdAsync(int documentId)
    {
        var document = await _context.ContractDocuments.Where(c => c.Id == documentId && !c.IsDeleted)
                                                      .Select(c => new GetContractDocumentsDto
                                                      {
                                                          ContractNumber = c.Contract.ContractNumber,
                                                          Description = c.Description,
                                                          DocumentName = c.DocumentName,
                                                          DocumentType = c.DocumentType.Name,
                                                          FilePath = c.FilePath,
                                                          FileSizeInBytes = c.FileSizeInBytes,
                                                          Id = c.Id,
                                                          UploadDate = c.UploadDate.ToCustomDateTime(),
                                                          UploadedBy = c.UploadedBy
                                                      }).FirstOrDefaultAsync();
        if (document == null)
            return ServiceResult<GetContractDocumentsDto?>.Failure("document not found.");

        return ServiceResult<GetContractDocumentsDto?>.Success(document);
    }

    public async Task<ServiceResult<List<GetContractDocumentsDto>>> GetDocumentsByContractAsync(Guid contractNumber)
    {
        var documents = await _context.ContractDocuments.Where(c => c.Contract.ContractNumber == contractNumber && !c.IsDeleted)
                                                   .Select(c => new GetContractDocumentsDto
                                                   {
                                                       ContractNumber = c.Contract.ContractNumber,
                                                       Description = c.Description,
                                                       DocumentName = c.DocumentName,
                                                       DocumentType = c.DocumentType.Name,
                                                       FilePath = c.FilePath,
                                                       FileSizeInBytes = c.FileSizeInBytes,
                                                       Id = c.Id,
                                                       UploadDate = c.UploadDate.ToCustomDateTime(),
                                                       UploadedBy = c.UploadedBy
                                                   }).ToListAsync();

        return ServiceResult<List<GetContractDocumentsDto>>.Success(documents);
    }

    public async Task<ServiceResult<bool>> UpdateDocumentAsync(int documentId, UpdateContractDocumentsDto updateDto)
    {
        var document = await _context.ContractDocuments.Where(c => c.Id == documentId && !c.IsDeleted)
                                                         .Include(c => c.Contract).FirstOrDefaultAsync();
        if (document == null)
            return ServiceResult<bool>.Failure("document not found.");
        /// Not sure if this will be true here?
        //if (document.Contract.Status == ContractStatus.Completed)
        //    return ServiceResult<bool>.Failure("can not update documents from completed contracts.");

        document.FilePath = updateDto.FilePath;
        document.FileSizeInBytes = updateDto.FileSizeInBytes;
        document.UploadedBy = updateDto.UploadedBy;
        document.DocumentName = updateDto.DocumentName;
        document.DocumentTypeId = updateDto.DocumentTypeId;
        document.Description = updateDto.Description;

        await _context.SaveChangesAsync();
        return ServiceResult<bool>.Success(true);
    }
}
