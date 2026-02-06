using ContractManagment.Api.DTOs.ClientDTOs;
using ContractManagment.Api.Services.ClientServices;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientsServices _clientsServices;

    public ClientsController(IClientsServices clientsServices)
    {
        _clientsServices = clientsServices;
    }

    // POST: api/clients
    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] AddClientDto dto)
    {
        var result = await _clientsServices.CreateClientAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // GET: api/clients?skip=0&take=10
    [HttpGet]
    public async Task<IActionResult> GetAllClients(
            [FromQuery] int skip = 0, [FromQuery] int take = 10,
            [FromQuery] string? sortBy = null, [FromQuery] string? sortDir = "asc"
          )
    {
        var result = await _clientsServices.GetAllClientsAsync(skip, take, sortBy, sortDir);
        return Ok(result);
    }

    // GET: api/clients/count
    [HttpGet("count")]
    public async Task<IActionResult> GetClientsCount()
    {
        var result = await _clientsServices.GetClientsCountAsync();
        return Ok(result);
    }

    // GET: api/clients/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetClientById(int id)
    {
        var result = await _clientsServices.GetClientByIdAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    // PUT: api/clients/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateClient(
        int id,
        [FromBody] UpdateClientDto dto)
    {
        var result = await _clientsServices.UpdateClientAsync(id, dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // PATCH: api/clients/{id}/status
    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateClientStatus(
        int id)
    {
        var result = await _clientsServices.UpdateClientStatusAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // GET: api/clients/{id}/statistics
    [HttpGet("{id:int}/statistics")]
    public async Task<IActionResult> GetClientStatistics(int id)
    {
        var result = await _clientsServices.GetClientStatisticsAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
