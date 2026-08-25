using Basefy.Domain.Models;

namespace Basefy.Domain.Interfaces
{
    public interface IPromptRepository
    {
        Task<Prompt?> GetByIdAsync(int id);

        Task<IReadOnlyList<Prompt>> GetAllAsync();

        Task<IReadOnlyList<Prompt>> GetByTenantIdAsync(int tenantId);

        Task<int> CreateAsync(Prompt prompt);

        Task<bool> UpdateAsync(Prompt prompt);

        Task<bool> DeleteAsync(int id);
    }
}
