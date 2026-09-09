using FinancialSystem.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IFinanceService _service;

        public DashBoardController(IFinanceService service)
        {
            _service = service;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> Summary([FromQuery] int month, [FromQuery] int year)
        {
            var result = await _service.GetDashBoardAsync(month, year);
            return Ok(result);
        }

        [HttpGet("by-category")]
        public async Task<IActionResult> ByCategory([FromQuery] int? month, [FromQuery] int? year)
        {
            var result = await _service.GetTotalsByCategoryAsync(month, year);
            return Ok(result);
        }
    }
}
