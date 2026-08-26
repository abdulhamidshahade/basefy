using Basefy.Domain.Models;

namespace Basefy.Domain.Interfaces
{
    public interface IToolRepository
    {
        Task<Tool?> GetByIdAsync(int id);

        Task<IReadOnlyList<Tool>> GetAllAsync();

        Task<IReadOnlyList<Tool>> GetByPromptVersionIdAsync(int promptVersionId);

        Task<int> CreateAsync(Tool tool);

        Task<bool> UpdateAsync(Tool tool);

        Task<bool> DeleteAsync(int id);
    }
}
