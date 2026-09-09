using FinancialSystem.Api.DTOs;
using FinancialSystem.Api.Models;
using FinancialSystem.Api.Services;
using Microsoft.AspNetCore.Mvc;


namespace FinancialSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IFinanceService _service;

        public TransactionsController(IFinanceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? month, [FromQuery] int? year, [FromQuery] char? type)
        {
            var result = await _service.GetTransactionsAsync(month, year, type);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetTransactionByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionCreateDto dto)
        {
            var t = new Transaction
            {
                Type = dto.Type,
                Amount = dto.Amount,
                Date = dto.Date,
                CategoryId = dto.CategoryId,
                Description = dto.Description
            };

            try
            {
                await _service.AddTransactionAsync(t);
                return CreatedAtAction(nameof(GetById), new { id = t.Id }, t);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TransactionCreateDto dto)
        {
            var existing = await _service.GetTransactionByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Type = dto.Type;
            existing.Amount = dto.Amount;
            existing.Date = dto.Date;
            existing.CategoryId = dto.CategoryId;
            existing.Description = dto.Description;

            try
            {
                await _service.UpdateTransactionAsync(existing);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetTransactionByIdAsync(id);
            if (existing == null) return NotFound();
            try
            {
                await _service.DeleteTransactionAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}