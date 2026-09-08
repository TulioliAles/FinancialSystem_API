using FinancialSystem.Api.Models;

namespace FinancialSystem.Api.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<IEnumerable<Transaction>> GetbyIdAsync(int id);
        Task<int> AddAsync(Transaction t);
        Task<int> UpdateAsync(Transaction t);
        Task<int> DeleteAsync(int id);
    }
}
