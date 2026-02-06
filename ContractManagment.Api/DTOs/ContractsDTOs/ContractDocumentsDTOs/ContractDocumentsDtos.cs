using ContractManagment.Api.Models.ContractsModels;
using ContractManagment.Api.Models.ContractsModels.ContractDocumentsModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagment.Api.DTOs.ContractsDTOs.ContractDocumentsDTOs;


public class GetSmallContractDocumentDto
{
    public int Id { get; set; }
    public string DocumentName { get; set; }
    public string DocumentType { get; set; }
    public string FilePath { get; set; }

}
public class GetContractDocumentsDto
{
    public int Id { get; set; }
    public string DocumentName { get; set; }
    public string DocumentType { get; set; }
    public string FilePath { get; set; }

    public string UploadDate { get; set; }

    public long FileSizeInBytes { get; set; }

    public string? Description { get; set; }
    public string UploadedBy { get; set; }

    public Guid ContractNumber { get; set; }
}
public class AddContractDocumentsDto
{
    [MaxLength(100)]
    public string DocumentName { get; set; }

    public int DocumentTypeId { get; set; }
    public string FilePath { get; set; }
    public long FileSizeInBytes { get; set; }
    public string? Description { get; set; }
    public string UploadedBy { get; set; }
}
public class UpdateContractDocumentsDto
{
    [MaxLength(100)]
    public string DocumentName { get; set; }
    public int DocumentTypeId { get; set; }
    public string FilePath { get; set; }
    public long FileSizeInBytes { get; set; }
    public string? Description { get; set; }
    public string UploadedBy { get; set; }
}
