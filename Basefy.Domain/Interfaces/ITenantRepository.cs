using Basefy.Domain.Models;

namespace Basefy.Domain.Interfaces
{
    public interface ITenantRepository
    {
        Task<Tenant?> GetByIdAsync(int id);

        Task<IReadOnlyList<Tenant>> GetAllAsync();

        Task<int> CreateAsync(Tenant tenant);

        Task<bool> UpdateAsync(Tenant tenant);

        Task<bool> DeleteAsync(int id);
    }
}
