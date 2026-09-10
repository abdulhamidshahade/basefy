using Basefy.Application.DTOs;
using Basefy.Domain.Models;

namespace Basefy.Application.Interfaces
{
    public interface IPromptVersionService
    {
        Task<PromptVersion?> GetByIdAsync(int id);

        Task<IReadOnlyList<PromptVersion>> GetAllAsync();

        Task<IReadOnlyList<PromptVersion>> GetByPromptIdAsync(int promptId);

        Task<PromptVersion> CreateAsync(CreatePromptVersionRequest request);

        Task<bool> UpdateAsync(int id, UpdatePromptVersionRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
