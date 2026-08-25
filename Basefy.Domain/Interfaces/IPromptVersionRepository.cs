using Basefy.Domain.Models;

namespace Basefy.Domain.Interfaces
{
    public interface IPromptVersionRepository
    {
        Task<PromptVersion?> GetByIdAsync(int id);

        Task<IReadOnlyList<PromptVersion>> GetAllAsync();

        Task<IReadOnlyList<PromptVersion>> GetByPromptIdAsync(int promptId);

        Task<int> CreateAsync(PromptVersion promptVersion);

        Task<bool> UpdateAsync(PromptVersion promptVersion);

        Task<bool> DeleteAsync(int id);
    }
}
