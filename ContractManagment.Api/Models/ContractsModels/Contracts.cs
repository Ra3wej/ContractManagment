using ContractManagment.Api.Models.ClientsModels;
using ContractManagment.Api.Services.ModelInterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagment.Api.Models.ContractsModels;

public class Contracts : IDateTimeAuditableEntity
{
    [Key]
    public int ContractNumber { get; set; }

    public Guid GuidKey { get; set; } = Guid.NewGuid();

    public int ClinetId { get; set; }
    [ForeignKey(nameof(ClinetId))]
    public Clients Clinet { get; set; }
    public string Title { get; set; } = null!;


    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal ContractValue { get; set; }

    public ContractStatus Status { get; set; }

    public ContractType ContractType { get; set; }
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
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
