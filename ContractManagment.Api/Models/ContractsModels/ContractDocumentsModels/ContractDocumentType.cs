using System.ComponentModel.DataAnnotations;

namespace ContractManagment.Api.Models.ContractsModels.ContractDocumentsModels;

public class ContractDocumentType
{
    [Key]
    public int Id { get; set; } 

    public string Name { get; set; }

    public ICollection<ContractDocuments> ContractDocuments { get; set; } = [];
}
