using ContractManagment.Api.DTOs.ContractsDTOs.ContractDocumentsDTOs;
using ContractManagment.Api.Services.ContractDocumentTypeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractDocumentTypesController : ControllerBase
{
    private readonly IContractDocumentTypesService _services;

    public ContractDocumentTypesController(IContractDocumentTypesService services)
    {
        _services = services;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _services.GetAllDocumentTypesAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _services.GetByIdAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddContractDocumentTypeDto dto)
    {
        var result = await _services.CreateAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateContractDocumentTypeDto dto)
    {
        var result = await _services.UpdateAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    //[HttpDelete("{id:int}")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    var result = await _services.DeleteAsync(id);

    //    if (!result.IsSuccess)
    //        return BadRequest(result);

    //    return Ok(result);
    //}
}
