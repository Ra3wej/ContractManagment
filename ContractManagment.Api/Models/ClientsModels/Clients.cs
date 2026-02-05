using ContractManagment.Api.Models.ContractsModels;
using ContractManagment.Api.Services.ModelInterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagment.Api.Models.ClientsModels;

public class Clients : IDateTimeAuditableEntity
{
    [Key]
    public int Id { get; set; }

    public string ClientName { get; set; }

    [Required]
    public string ContactPerson { get; set; }

    [EmailAddress(ErrorMessage = "Not a valid email.")]
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string? CompanyRegistrationNumber { get; set; }

    public int IndustryId { get; set; }
    [ForeignKey(nameof(IndustryId))]
    public Industry Industry { get; set; }

    public bool StatusIsActive { get; private set; } = true;
    public bool IsDeleted { get; private set; } = false;

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    public ICollection<Contracts> Contracts { get; set; } = [];

    public (bool success, string? errorMessage) DeactivateClient()
    {
        if (!StatusIsActive)
        {
            return (false, "can not deactivate a Clinet which is already deactivated.");
        }

        if (Contracts.Count != 0)
        {
            return (false, "can only deactivate if it has 0 contracts assigned.");
        }

        StatusIsActive = false;
        return (true, null);
    }
}
