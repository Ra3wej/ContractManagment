using ContractManagment.Api.Models.ContractsModels;
using ContractManagment.Api.Services.ModelInterfaces;

namespace ContractManagment.Api.Models;

public class Categories : IDateTimeAuditableEntity
{
    public int Id { get; set; } 

    public string Name { get; set; }  

    public string? Description { get; set; }

    public bool StatusIsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    public ICollection<Contracts> Contracts { get; set; } = [];
}
