using Basefy.Domain.Interfaces;
using Basefy.Domain.Models;
using Npgsql;

namespace Besefy.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public TenantRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<Tenant?> GetByIdAsync(int id)
        {
            const string sql = "SELECT id, name FROM tenants WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            await using var reader = await command.ExecuteReaderAsync();

            return await reader.ReadAsync() ? Map(reader) : null;
        }

        public async Task<IReadOnlyList<Tenant>> GetAllAsync()
        {
            const string sql = "SELECT id, name FROM tenants ORDER BY id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync();

            var tenants = new List<Tenant>();
            while (await reader.ReadAsync())
            {
                tenants.Add(Map(reader));
            }

            return tenants;
        }

        public async Task<int> CreateAsync(Tenant tenant)
        {
            const string sql = "INSERT INTO tenants (name) VALUES (@name) RETURNING id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("name", tenant.Name);

            var newId = (int)(await command.ExecuteScalarAsync())!;
            tenant.Id = newId;

            return newId;
        }

        public async Task<bool> UpdateAsync(Tenant tenant)
        {
            const string sql = "UPDATE tenants SET name = @name WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("name", tenant.Name);
            command.Parameters.AddWithValue("id", tenant.Id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM tenants WHERE id = @id;";

            await using var connection = await _dataSource.OpenConnectionAsync();
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        private static Tenant Map(NpgsqlDataReader reader)
        {
            return new Tenant
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name"))
            };
        }
    }
}
