using Basefy.Domain.Interfaces;
using Basefy.Domain.Models;
using Npgsql;

namespace Besefy.Infrastructure.Repositories
{
    public class ToolVersionRepository : IToolVersionRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public ToolVersionRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<ToolVersion?> GetByIdAsync(int id)
        {
            const string sql = "SELECT id, tool_id FROM tool_versions WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            await using var reader = await command.ExecuteReaderAsync();

            return await reader.ReadAsync() ? Map(reader) : null;
        }

        public async Task<IReadOnlyList<ToolVersion>> GetAllAsync()
        {
            const string sql = "SELECT id, tool_id FROM tool_versions ORDER BY id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync();

            var toolVersions = new List<ToolVersion>();
            while (await reader.ReadAsync())
            {
                toolVersions.Add(Map(reader));
            }

            return toolVersions;
        }

        public async Task<IReadOnlyList<ToolVersion>> GetByToolIdAsync(int toolId)
        {
            const string sql = "SELECT id, tool_id FROM tool_versions WHERE tool_id = @toolId ORDER BY id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("toolId", toolId);
            await using var reader = await command.ExecuteReaderAsync();

            var toolVersions = new List<ToolVersion>();
            while (await reader.ReadAsync())
            {
                toolVersions.Add(Map(reader));
            }

            return toolVersions;
        }

        public async Task<int> CreateAsync(ToolVersion toolVersion)
        {
            const string sql = "INSERT INTO tool_versions (tool_id) VALUES (@toolId) RETURNING id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("toolId", toolVersion.ToolId);

            var newId = (int)(await command.ExecuteScalarAsync())!;
            toolVersion.Id = newId;

            return newId;
        }

        public async Task<bool> UpdateAsync(ToolVersion toolVersion)
        {
            const string sql = "UPDATE tool_versions SET tool_id = @toolId WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("toolId", toolVersion.ToolId);
            command.Parameters.AddWithValue("id", toolVersion.Id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM tool_versions WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        private static ToolVersion Map(NpgsqlDataReader reader)
        {
            return new ToolVersion
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                ToolId = reader.GetInt32(reader.GetOrdinal("tool_id"))
            };
        }
    }
}
