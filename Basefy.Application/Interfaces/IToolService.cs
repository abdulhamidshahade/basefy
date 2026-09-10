using Basefy.Application.DTOs;
using Basefy.Domain.Models;

namespace Basefy.Application.Interfaces
{
    public interface IToolService
    {
        Task<Tool?> GetByIdAsync(int id);

        Task<IReadOnlyList<Tool>> GetAllAsync();

        Task<IReadOnlyList<Tool>> GetByPromptVersionIdAsync(int promptVersionId);

        Task<Tool> CreateAsync(CreateToolRequest request);

        Task<bool> UpdateAsync(int id, UpdateToolRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
