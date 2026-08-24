namespace Basefy.Domain.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Prompt> prompts { get; set; }
    }
}
