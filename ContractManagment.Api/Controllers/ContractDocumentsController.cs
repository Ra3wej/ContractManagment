using ContractManagment.Api.DTOs.ContractsDTOs.ContractDocumentsDTOs;
using ContractManagment.Api.Services.ContractDocumentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractDocumentsController : ControllerBase
    {
        private readonly IContractDocumentsServices _documentsServices;

        public ContractDocumentsController(IContractDocumentsServices documentsServices)
        {
            _documentsServices = documentsServices;
        }

        // POST: api/contracts/{contractId}/documents
        [HttpPost("contracts/{contractNumber:guid}/documents")]
        public async Task<IActionResult> AddDocumentToContract(
            Guid contractNumber,
            [FromBody] AddContractDocumentsDto dto)
        {
            var result = await _documentsServices.AddDocumentToContractAsync(
                contractNumber, dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // GET: api/contracts/{contractId}/documents
        [HttpGet("contracts/{contractNumber:guid}/documents")]
        public async Task<IActionResult> GetDocumentsByContract(
            Guid contractNumber)
        {
            var result = await _documentsServices
                .GetDocumentsByContractAsync(contractNumber);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // GET: api/documents/{id}
        [HttpGet("documents/{id:int}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var result = await _documentsServices.GetDocumentByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        // PUT: api/documents/{id}
        [HttpPut("documents/{id:int}")]
        public async Task<IActionResult> UpdateDocument(
            int id,
            [FromBody] UpdateContractDocumentsDto dto)
        {
            var result = await _documentsServices.UpdateDocumentAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: api/documents/{id}
        [HttpDelete("documents/{id:int}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var result = await _documentsServices.DeleteDocumentAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}

