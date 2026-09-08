using Dapper;
using FinancialSystem.Api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinancialSystem.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using var conn = CreateConnection();
            var sql = "SELECT * FROM Categories";
            return await conn.QueryAsync<Category>(sql);
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            using var conn = CreateConnection();
            var sql = "SELECT * FROM Categories WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id});
        }
    }
}
