using ContractManagment.Api.Services.ModelInterfaces;

namespace ContractManagment.Api.Models;

public class Categories : IDateTimeAuditableEntity
{
    public int Id { get; set; } // PK (lookup / reference table)

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool StatusIsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
