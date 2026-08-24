namespace Basefy.Domain.Models
{
    public class PromptVersion
    {
        public int Id { get; set; }
        public int PromptId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Prompt Prompt { get; set; }
        public List<Tool> Tools { get; set; }
    }
}
