using ContractManagment.Api.DTOs.ContractsDTOs;
using ContractManagment.Api.Models.ContractsModels;
using ContractManagment.Api.Services.ContractServices;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagment.Api.Controllers;

[Route("api/contracts")]
[ApiController]
public class ContractsController : ControllerBase
{
    private readonly IContractsServices _contractsServices;

    public ContractsController(IContractsServices contractsServices)
    {
        _contractsServices = contractsServices;
    }

    // POST /api/contracts
    [HttpPost]
    public async Task<IActionResult> CreateNewContract([FromBody] AddContractsDto dto)
    {
        var result = await _contractsServices.CreateNewContractAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // GET /api/contracts?skip=0&take=10
    [HttpGet]
    public async Task<IActionResult> GetAllContracts(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        
        var result = await _contractsServices.GetAllContractsAsync(skip, take);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // GET /api/contracts/{id}
    [HttpGet("{contractNumber:guid}")]
    public async Task<IActionResult> GetContractById(Guid contractNumber)
    {
        var result = await _contractsServices.GetOneContractAsync(contractNumber);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    // PUT /api/contracts/{id}
    [HttpPut("{contractNumber:guid}")]
    public async Task<IActionResult> UpdateContract(
        Guid contractNumber,
        [FromBody] UpdateContractsDto dto)
    {
        var result = await _contractsServices.UpdateContractAsync(contractNumber, dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // DELETE /api/contracts/{id}
    [HttpDelete("{contractNumber:guid}")]
    public async Task<IActionResult> DeleteContract(Guid contractNumber)
    {
        var result = await _contractsServices.DeleteContractAsync(contractNumber);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // PATCH /api/contracts/{id}/status
    [HttpPatch("{contractNumber:guid}/{newContractStatus:int}/status")]
    public async Task<IActionResult> UpdateContractStatus(Guid contractNumber, ContractStatus newContractStatus)
    {
        var result = await _contractsServices.UpdateContractStatusAsync(contractNumber, newContractStatus);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // POST /api/contracts/{id}/clone
    [HttpPost("{contractNumber:guid}/clone")]
    public async Task<IActionResult> CloneContract(Guid contractNumber)
    {
        var result = await _contractsServices.CloneContractAsync(contractNumber);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
