using FinancialSystem.Api.DTOs;
using FinancialSystem.Api.Models;

namespace FinancialSystem.Api.Services
{
    public interface IFinanceService
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync(int? month = null, int? year = null, char? type = null);
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task AddTransactionAsync(Transaction t);
        Task UpdateTransactionAsync(Transaction t);
        Task DeleteTransactionAsync(int id);
        Task<DashboardDto> GetDashBoardAsync(int month, int year);
        Task<IEnumerable<CategoryTotalDto>> GetTotalsByCategoryAsync(int? month = null, int? year = null);
    }
}
