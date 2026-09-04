namespace FinancialSystem.Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public char Type { get; set; } // R ou D
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
    }
}
