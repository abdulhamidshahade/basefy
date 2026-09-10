namespace Basefy.Application.DTOs
{
    public record CreateToolRequest(string Name, int PromptVersionId);

    public record UpdateToolRequest(string Name, int PromptVersionId);
}
