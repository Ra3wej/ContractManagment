using ContractManagment.Api.DTOs.ContractsDTOs.ContractDocumentsDTOs;
using ContractManagment.Api.Models;
using ContractManagment.Api.Models.ClientsModels;
using ContractManagment.Api.Models.ContractsModels;
using ContractManagment.Api.Models.ContractsModels.ContractDocumentsModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagment.Api.DTOs.ContractsDTOs;

public class GetOneContractDto
{
    public Guid ContractNumber { get; set; } 

    public BaseSelectDto<int> Clinet { get; set; }
    public string Title { get; set; }
    public BaseSelectDto<int> Category { get; set; }
    public string? Description { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }

    public decimal ContractValue { get; set; }

    public string Status { get; set; }

    public string ContractType { get; set; }
    public string? Notes { get; set; }
    public List<GetSmallContractDocumentDto> ContractDocuments { get; set; }
}
public class GetContractsDto
{
    public Guid ContractNumber { get; set; } 

    public string Clinet { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }

    public decimal ContractValue { get; set; }

    public string Status { get; set; }

    public string ContractType { get; set; }
}

public class AddContractsDto
{
    public int ClinetId { get; set; }

    public string Title { get; set; }

    public int CategoryId { get; set; }

    public string? Description { get; set; }

    /// Start date should be before end date ( specfied as a check in DB)
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal ContractValue { get; set; }

    public ContractStatus Status { get; set; }

    public ContractType ContractType { get; set; }
    public string? Notes { get; set; }


}
public class UpdateContractsDto
{
    
    public int ClinetId { get; set; }

    public string Title { get; set; }

    public int CategoryId { get; set; }
    public string? Description { get; set; }

    /// Start date should be before end date ( specfied as a check in DB)
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal ContractValue { get; set; }

    public ContractType ContractType { get; set; }
    public string? Notes { get; set; }

}
