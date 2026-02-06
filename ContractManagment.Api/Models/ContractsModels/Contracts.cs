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

    public ContractStatus Status { get; private set; }=ContractStatus.Draft;

    public bool IsDeleted { get; private set; } = false;

    public ContractType ContractType { get; set; }
    public string? Notes { get; set; }

    public int? ClonedFromContractId { get; set; }
    [ForeignKey(nameof(ClonedFromContractId))]
    public Contracts? ClonedFromContract { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public ICollection<ContractDocuments> ContractDocuments { get; set; } = [];

    public (bool success, string message) UpdateContractStatus(ContractStatus newStatus)
    {
        if (Status == newStatus)
            return (true, "Contract is already in the specified status.");

        switch (Status)
        {
            case ContractStatus.Draft:
                if (newStatus != ContractStatus.Active)
                    return (false, "Draft contracts can only be moved to Active.");
                break;

            case ContractStatus.Active:
                if (newStatus != ContractStatus.Completed &&
                    newStatus != ContractStatus.Cancelled)
                    return (false, "Active contracts can only be Completed or Cancelled.");
                break;

            case ContractStatus.Completed:
                if (newStatus != ContractStatus.Cancelled)
                    return (false, "Finalized contracts can only be canceled.");
                break;

            case ContractStatus.Cancelled:
                return (false, "Cancelled contracts cannot change status.");

            default:
                return (false, "Invalid contract status transition.");
        }

        Status = newStatus;

        return (true, "");
    }
    public (bool success, string message) DeleteContractWithAssociatedDocuments()
    {
        if (IsDeleted)
            return (false, "Contract is already deleted.");

        if (Status is ContractStatus.Active or ContractStatus.Completed)
            return (false, "Active or Completed contracts cannot be deleted.");

        IsDeleted = true;

        foreach (var item in ContractDocuments)
        {
            item.IsDeleted = true;
        }

        return (true, "");
    }

}

public enum ContractStatus
{
    Draft = 0,
    Active = 1,
    Completed = 2,
    Cancelled = 3,
    Expired = 5,
}
public enum ContractType
{
    FixedPrice = 0,
    TimeAndMaterial = 1,
    Retainer = 2,
    Consulting = 3,
    Support = 4
}
