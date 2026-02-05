using ContractManagment.Api.Models.ContractsModels;
using ContractManagment.Api.Services.ModelInterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagment.Api.Models;

public class Categories : IDateTimeAuditableEntity
{
    public int Id { get; set; }

    [Length(3, 100, ErrorMessage = "category name length should be between 3-100")]
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public bool StatusIsActive { get; private set; } = true;
    public bool IsDeleted { get; private set; } = false;

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    public ICollection<Contracts> Contracts { get; set; } = [];

    public (bool success,string? errorMessage) DeactivateCategory()
    {
        if (!StatusIsActive)
        {
            return (false, "can not deactivate a category which is already deactivated.");
        }


        if (Contracts.Count != 0)
        {
            return (false, "can only deactivate if it has 0 contracts assigned.");
        }

        StatusIsActive = false;
        return (true, null);
    }
}
