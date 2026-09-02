using Basefy.Domain.Interfaces;
using Basefy.Domain.Models;
using Npgsql;

namespace Besefy.Infrastructure.Repositories
{
    public class PromptVersionRepository : IPromptVersionRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public PromptVersionRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<PromptVersion?> GetByIdAsync(int id)
        {
            const string sql = "SELECT id, prompt_id FROM prompt_versions WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            await using var reader = await command.ExecuteReaderAsync();

            return await reader.ReadAsync() ? Map(reader) : null;
        }

        public async Task<IReadOnlyList<PromptVersion>> GetAllAsync()
        {
            const string sql = "SELECT id, prompt_id FROM prompt_versions ORDER BY id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync();

            var promptVersions = new List<PromptVersion>();
            while (await reader.ReadAsync())
            {
                promptVersions.Add(Map(reader));
            }

            return promptVersions;
        }

        public async Task<IReadOnlyList<PromptVersion>> GetByPromptIdAsync(int promptId)
        {
            const string sql = "SELECT id, prompt_id FROM prompt_versions WHERE prompt_id = @promptId ORDER BY id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("promptId", promptId);
            await using var reader = await command.ExecuteReaderAsync();

            var promptVersions = new List<PromptVersion>();
            while (await reader.ReadAsync())
            {
                promptVersions.Add(Map(reader));
            }

            return promptVersions;
        }

        public async Task<int> CreateAsync(PromptVersion promptVersion)
        {
            const string sql = "INSERT INTO prompt_versions (prompt_id) VALUES (@promptId) RETURNING id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("promptId", promptVersion.PromptId);

            var newId = (int)(await command.ExecuteScalarAsync())!;
            promptVersion.Id = newId;

            return newId;
        }

        public async Task<bool> UpdateAsync(PromptVersion promptVersion)
        {
            const string sql = "UPDATE prompt_versions SET prompt_id = @promptId WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("promptId", promptVersion.PromptId);
            command.Parameters.AddWithValue("id", promptVersion.Id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM prompt_versions WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        private static PromptVersion Map(NpgsqlDataReader reader)
        {
            return new PromptVersion
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                PromptId = reader.GetInt32(reader.GetOrdinal("prompt_id"))
            };
        }
    }
}
