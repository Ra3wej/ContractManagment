using ContractManagment.Api.Services.ModelInterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace ContractManagment.Api.Models.ContractsModels.ContractDocumentsModels;

public class ContractDocuments : IDateTimeAuditableEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string DocumentName { get; set; } 
    public string DocumentRandomName { get; set; }

    public int DocumentTypeId { get; set; }
    [ForeignKey(nameof(DocumentTypeId))]
    public ContractDocumentType DocumentType { get; set; }

    [Required]
    public string FilePath { get; set; }

    public DateTime UploadDate { get; set; } = DateTime.Now;

    public long FileSizeInBytes { get; set; }

    public string? Description { get; set; }
    [Required]
    public string UploadedBy { get; set; }

    public int ContractId { get; set; }
    [ForeignKey(nameof(ContractId))]
    public Contracts Contract { get; set; } = null!;

    public bool IsDeleted { get; set; }=false;
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
