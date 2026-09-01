using Basefy.Domain.Data;
using Basefy.Domain.Interfaces;
using Basefy.Domain.Models;
using Npgsql;

namespace Besefy.Infrastructure.Repositories
{
    public class PromptRepository : IPromptRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public PromptRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<Prompt?> GetByIdAsync(int id)
        {
            const string sql = "SELECT id, tenant_id, llm_model FROM prompts WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            await using var reader = await command.ExecuteReaderAsync();

            return await reader.ReadAsync() ? Map(reader) : null;
        }

        public async Task<IReadOnlyList<Prompt>> GetAllAsync()
        {
            const string sql = "SELECT id, tenant_id, llm_model FROM prompts ORDER BY id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync();

            var prompts = new List<Prompt>();
            while (await reader.ReadAsync())
            {
                prompts.Add(Map(reader));
            }

            return prompts;
        }

        public async Task<IReadOnlyList<Prompt>> GetByTenantIdAsync(int tenantId)
        {
            const string sql = "SELECT id, tenant_id, llm_model FROM prompts WHERE tenant_id = @tenantId ORDER BY id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("tenantId", tenantId);
            await using var reader = await command.ExecuteReaderAsync();

            var prompts = new List<Prompt>();
            while (await reader.ReadAsync())
            {
                prompts.Add(Map(reader));
            }

            return prompts;
        }

        public async Task<int> CreateAsync(Prompt prompt)
        {
            const string sql = "INSERT INTO prompts (tenant_id, llm_model) VALUES (@tenantId, @llmModel) RETURNING id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("tenantId", prompt.TenantId);
            command.Parameters.AddWithValue("llmModel", prompt.llmModel.ToString());

            var newId = (int)(await command.ExecuteScalarAsync())!;
            prompt.Id = newId;

            return newId;
        }

        public async Task<bool> UpdateAsync(Prompt prompt)
        {
            const string sql = "UPDATE prompts SET tenant_id = @tenantId, llm_model = @llmModel WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("tenantId", prompt.TenantId);
            command.Parameters.AddWithValue("llmModel", prompt.llmModel.ToString());
            command.Parameters.AddWithValue("id", prompt.Id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM prompts WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        private static Prompt Map(NpgsqlDataReader reader)
        {
            return new Prompt
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                TenantId = reader.GetInt32(reader.GetOrdinal("tenant_id")),
                llmModel = Enum.Parse<LlmModel>(reader.GetString(reader.GetOrdinal("llm_model")))
            };
        }
    }
}
