using ClosedXML.Excel;
using FinancialSystem.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace FinancialSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IFinanceService _service;

        public ReportsController(IFinanceService service)
        {
            _service = service;
        }

        [HttpGet("csv")]
        public async Task<IActionResult> ExportCsv([FromQuery] int? month, [FromQuery] int? year, [FromQuery] char? type)
        {
            var transaction = await _service.GetTransactionsAsync(month, year, type);

            var sb = new StringBuilder();
            sb.AppendLine("Id, Type, CategoryId, Description, Amount, Date");

            foreach (var t in transaction)
            {
                var line = string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}, \"{3}\", {4}, {5}", 
                            t.Id, 
                            t.Type, 
                            t.CategoryId, 
                            t.Description?.Replace("\"", "\"\""), 
                            t.Amount, 
                            t.Date.ToString("dd-MM-yyyy"));
                sb.AppendLine(line);
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", $"transactions_{month ?? 0}_{year ?? 0}.csv");
        }

        [HttpGet("excel")]
        public async Task<IActionResult> ExportExcel([FromQuery] int? month, [FromQuery] int? year, [FromQuery] char? type)
        {
            var transaction = await _service.GetTransactionsAsync(month, year, type);

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Transactions");

            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Type";
            ws.Cell(1, 3).Value = "CategoryId";
            ws.Cell(1, 4).Value = "Description";
            ws.Cell(1, 5).Value = "Amount";
            ws.Cell(1, 6).Value = "Date";

            int row = 2;

            foreach (var t in transaction)
            {
                ws.Cell(row, 1).Value = t.Id;
                ws.Cell(row, 2).Value = t.Type;
                ws.Cell(row, 3).Value = t.CategoryId;
                ws.Cell(row, 4).Value = t.Description;
                ws.Cell(row, 5).Value = t.Amount;
                ws.Cell(row, 6).Value = t.Date.ToString("dd-MM-yyyy");
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);
            var bytes = stream.ToArray();

            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"transactions_{month ?? 0}_{year ?? 0}.xlsx");
        }
    }
}
