using System.ComponentModel.DataAnnotations;

namespace FinancialSystem.Api.DTOs
{
    public class TransactionCreateDto
    {
        [Required]
        public char Type { get; set; } // R ou D
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? Description { get; set; }
    }
}
