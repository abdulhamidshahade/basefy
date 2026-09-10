using Basefy.Application.DTOs;
using Basefy.Domain.Models;

namespace Basefy.Application.Interfaces
{
    public interface IPromptService
    {
        Task<Prompt?> GetByIdAsync(int id);

        Task<IReadOnlyList<Prompt>> GetAllAsync();

        Task<IReadOnlyList<Prompt>> GetByTenantIdAsync(int tenantId);

        Task<Prompt> CreateAsync(CreatePromptRequest request);

        Task<bool> UpdateAsync(int id, UpdatePromptRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
