using Basefy.Application.DTOs;
using Basefy.Domain.Models;

namespace Basefy.Application.Interfaces
{
    public interface ITenantService
    {
        Task<Tenant?> GetByIdAsync(int id);

        Task<IReadOnlyList<Tenant>> GetAllAsync();

        Task<Tenant> CreateAsync(CreateTenantRequest request);

        Task<bool> UpdateAsync(int id, UpdateTenantRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
