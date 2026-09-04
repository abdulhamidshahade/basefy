using Basefy.Domain.Interfaces;
using Basefy.Domain.Models;
using Npgsql;

namespace Besefy.Infrastructure.Repositories
{
    public class ToolRepository : IToolRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public ToolRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<Tool?> GetByIdAsync(int id)
        {
            const string sql = "SELECT id, name, prompt_version_id FROM tools WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            await using var reader = await command.ExecuteReaderAsync();

            return await reader.ReadAsync() ? Map(reader) : null;
        }

        public async Task<IReadOnlyList<Tool>> GetAllAsync()
        {
            const string sql = "SELECT id, name, prompt_version_id FROM tools ORDER BY id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync();

            var tools = new List<Tool>();
            while (await reader.ReadAsync())
            {
                tools.Add(Map(reader));
            }

            return tools;
        }

        public async Task<IReadOnlyList<Tool>> GetByPromptVersionIdAsync(int promptVersionId)
        {
            const string sql = "SELECT id, name, prompt_version_id FROM tools WHERE prompt_version_id = @promptVersionId ORDER BY id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("promptVersionId", promptVersionId);
            await using var reader = await command.ExecuteReaderAsync();

            var tools = new List<Tool>();
            while (await reader.ReadAsync())
            {
                tools.Add(Map(reader));
            }

            return tools;
        }

        public async Task<int> CreateAsync(Tool tool)
        {
            const string sql = "INSERT INTO tools (name, prompt_version_id) VALUES (@name, @promptVersionId) RETURNING id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("name", tool.Name);
            command.Parameters.AddWithValue("promptVersionId", tool.PromptVersionId);

            var newId = (int)(await command.ExecuteScalarAsync())!;
            tool.Id = newId;

            return newId;
        }

        public async Task<bool> UpdateAsync(Tool tool)
        {
            const string sql = "UPDATE tools SET name = @name, prompt_version_id = @promptVersionId WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("name", tool.Name);
            command.Parameters.AddWithValue("promptVersionId", tool.PromptVersionId);
            command.Parameters.AddWithValue("id", tool.Id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM tools WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        private static Tool Map(NpgsqlDataReader reader)
        {
            return new Tool
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                PromptVersionId = reader.GetInt32(reader.GetOrdinal("prompt_version_id"))
            };
        }
    }
}
