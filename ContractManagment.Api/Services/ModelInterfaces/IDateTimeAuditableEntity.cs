namespace ContractManagment.Api.Services.ModelInterfaces;

public interface IDateTimeAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
