using Basefy.Domain.Models;

namespace Basefy.Domain.Interfaces
{
    public interface IToolVersionRepository
    {
        Task<ToolVersion?> GetByIdAsync(int id);

        Task<IReadOnlyList<ToolVersion>> GetAllAsync();

        Task<IReadOnlyList<ToolVersion>> GetByToolIdAsync(int toolId);

        Task<int> CreateAsync(ToolVersion toolVersion);

        Task<bool> UpdateAsync(ToolVersion toolVersion);

        Task<bool> DeleteAsync(int id);
    }
}
