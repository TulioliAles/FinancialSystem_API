namespace FinancialSystem.Api.DTOs
{
    public class DashboardDto
    {
        public decimal Receitas { get; set; }
        public decimal Despesas { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal Diferenca { get; set; }
        public decimal PercentualCrescimento { get; set; }
    }
}
