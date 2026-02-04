using System.ComponentModel.DataAnnotations;

namespace ContractManagment.Api.Models;

public class AuditLog
{
    [Key]
    public int Id { get; set; }
    public Guid GroupKey { get; set; }
    public string Action { get; set; } = string.Empty;
    public string TableName { get; set; } = string.Empty;
    public int ModelId { get; set; }
    public string Changes { get; set; } = string.Empty;
    public DateTime InsertDateTime { get; set; } = DateTime.Now;
}