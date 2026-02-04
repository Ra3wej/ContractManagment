using ContractManagment.Api.Services.ModelInterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace ContractManagment.Api.Models.ContractsModels.ContractDocumentsModels;

public class ContractDocuments : IDateTimeAuditableEntity
{
    [Key]
    public int Id { get; set; }

    public string DocumentName { get; set; } = null!;

    public int DocumentTypeId { get; set; }
    [ForeignKey(nameof(DocumentTypeId))]
    public ContractDocumentType DocumentType { get; set; } = null!;

    public string FilePath { get; set; }

    public DateTime UploadDate { get; set; }

    public long FileSizeInBytes { get; set; }

    public string? Description { get; set; }

    public string UploadedBy { get; set; }

    public int ContractId { get; set; }
    [ForeignKey(nameof(ContractId))]
    public Contracts Contract { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
