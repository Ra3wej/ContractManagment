using ContractManagment.Api.DTOs.ContractsDTOs;
using ContractManagment.Api.Models.ClientsModels;
using ContractManagment.Api.Models.ContractsModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagment.Api.DTOs.ClientDTOs;

public class GetOneClientDto
{
    public int Id { get; set; }

    public string ClientName { get; set; }

    public string ContactPerson { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string? CompanyRegistrationNumber { get; set; }

    public string Industry { get; set; }

    public bool StatusIsActive { get; set; }

    public List<GetContractsDto> Contracts { get; set; }
}

public class GetClientDto
{
    public int Id { get; set; }

    public string ClientName { get; set; }

    public string Industry { get; set; }

    public int NumberOfContracts { get; set; }
}

public class AddClientDto
{
    public string ClientName { get; set; }

    public string ContactPerson { get; set; }

    [EmailAddress(ErrorMessage = "Not a valid email.")]
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string? CompanyRegistrationNumber { get; set; }

    public int IndustryId { get; set; }

}

public class UpdateClientDto
{
    public int Id { get; set; }

    public string ClientName { get; set; }

    public string ContactPerson { get; set; }

    [EmailAddress(ErrorMessage = "Not a valid email.")]
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string? CompanyRegistrationNumber { get; set; }

    public int IndustryId { get; set; }
}
