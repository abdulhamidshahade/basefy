using Basefy.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basefy.Domain.Models
{
    public class Prompt
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        
        public LlmModel llmModel { get; set; }
        public List<PromptVersion> versions { get; set; }
    }
}
