namespace Basefy.Domain.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PromptVersionId { get; set; }
        public PromptVersion PromptVersion { get; set; }
        public List<ToolVersion> ToolVersions { get; set; }
    }
}
