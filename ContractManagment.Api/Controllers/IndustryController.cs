using ContractManagment.Api.DTOs.ClientDTOs;
using ContractManagment.Api.Services.IndustryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IndustryController : ControllerBase
{
    private readonly IIndustriesServices _industryService;
    public IndustryController(IIndustriesServices industriesServices)
    {
        _industryService = industriesServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _industryService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _industryService.GetByIdAsync(id);
        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddIndustryDto dto)
    {
        var result = await _industryService.CreateAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateIndustryDto dto)
    {
        var result = await _industryService.UpdateAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
