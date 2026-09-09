using Basefy.Domain.Data;

namespace Basefy.Application.DTOs
{
    public record CreatePromptRequest(int TenantId, LlmModel LlmModel);

    public record UpdatePromptRequest(int TenantId, LlmModel LlmModel);
}
