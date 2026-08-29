namespace Basefy.Domain.Models
{
    public class ToolVersion
    {
        public int Id { get; set; }
        public int ToolId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Tool Tool { get; set; }
    }
}