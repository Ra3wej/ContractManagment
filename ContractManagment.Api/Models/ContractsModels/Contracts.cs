using ContractManagment.Api.Models.ClientsModels;
using ContractManagment.Api.Models.ContractsModels.ContractDocumentsModels;
using ContractManagment.Api.Services.ModelInterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagment.Api.Models.ContractsModels;

public class Contracts : IDateTimeAuditableEntity
{

    [Key]
    public int Id { get; set; }
    public Guid ContractNumber { get; set; } = Guid.NewGuid();

    public int ClinetId { get; set; }
    [ForeignKey(nameof(ClinetId))]
    public Clients Clinet { get; set; }

    public string Title { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Categories Category { get; set; }

    public string? Description { get; set; }

    /// Start date should be before end date ( specfied as a check in DB)
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal ContractValue { get; set; }

    public ContractStatus Status { get; set; }

    public ContractType ContractType { get; set; }
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    public ICollection<ContractDocuments> ContractDocuments { get; set; } = [];
}

public enum ContractStatus
{
    Draft = 0,
    Active = 1,
    Expired = 2,
    Completed = 3,
    Cancelled = 4
}
public enum ContractType
{
    FixedPrice = 0,
    TimeAndMaterial = 1,
    Retainer = 2,
    Consulting = 3,
    Support = 4
}
