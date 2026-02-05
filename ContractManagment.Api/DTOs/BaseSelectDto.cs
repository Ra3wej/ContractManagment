namespace ContractManagment.Api.DTOs;

public class BaseSelectDto<T>
{
    public required T Id { get; set; }
    public required string Name { get; set; }
}
