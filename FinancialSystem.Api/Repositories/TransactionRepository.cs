using Dapper;
using FinancialSystem.Api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinancialSystem.Api.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<int> AddAsync(Transaction t)
        {
            using var conn = CreateConnection();
            var sql = @"INSERT INTO Transactions (Type, CategoryId, Description, Amount, Date)
                        VALUES(@Type, @CategoryId, @Description, @Amount, @Date)";
            return await conn.ExecuteAsync(sql, t);
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var conn = CreateConnection();
            var sql = "DELETE FROM Transactions WHERE Id = @Id";
            return await conn.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            using var conn = CreateConnection();
            var sql = "SELECT * FROM Transactions";
            return await conn.QueryAsync<Transaction>(sql);
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            using var conn = CreateConnection();
            var sql = "SELECT * FROM Transactions WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Transaction>(sql, new { Id = id });
        }

        public async Task<int> UpdateAsync(Transaction t)
        {
            using var conn = CreateConnection();
            var sql = @"UPDATE Transactions SET
                          Type = @Type,
                          CategoryId = @CategoryId,
                          Description = @Description,
                          Amount = @Amount,
                          Date = @Date
                        WHERE Id = @Id";

            return await conn.ExecuteAsync(sql, t);
        }
    }
}
