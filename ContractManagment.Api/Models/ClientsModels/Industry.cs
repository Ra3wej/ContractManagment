using System.ComponentModel.DataAnnotations;

namespace ContractManagment.Api.Models.ClientsModels;

public class Industry
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Clients> Clients { get; set; } = [];
}
