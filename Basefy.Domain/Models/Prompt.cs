using Basefy.Domain.Data;

namespace Basefy.Domain.Models
{
    public class Prompt
    {
        public int Id { get; set; }
        public LlmModel llmModel { get; set; }
        public List<PromptVersion> versions { get; set; }
    }
}
