namespace ContractManagment.Api.DTOs.ClientDTOs;

public class GetIndustryDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}
public class AddIndustryDto
{
    public string Name { get; set; }
}

public class UpdateIndustryDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}
