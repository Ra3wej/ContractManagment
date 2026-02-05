namespace ContractManagment.Api.DTOs.ContractsDTOs.ContractDocumentsDTOs;

public class GetContractDocumentTypesDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}
public class AddContractDocumentTypeDto
{
    public string Name { get; set; }
}
public class UpdateContractDocumentTypeDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}
