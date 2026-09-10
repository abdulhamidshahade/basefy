using Basefy.Application.DTOs;
using Basefy.Domain.Models;

namespace Basefy.Application.Interfaces
{
    public interface IToolVersionService
    {
        Task<ToolVersion?> GetByIdAsync(int id);

        Task<IReadOnlyList<ToolVersion>> GetAllAsync();

        Task<IReadOnlyList<ToolVersion>> GetByToolIdAsync(int toolId);

        Task<ToolVersion> CreateAsync(CreateToolVersionRequest request);

        Task<bool> UpdateAsync(int id, UpdateToolVersionRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
