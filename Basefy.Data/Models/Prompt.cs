using Basefy.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basefy.Data.Models
{
    public class Prompt
    {
        public int Id { get; set; }
        public LlmModel llmModel { get; set; }
        public List<PromptVersion> versions { get; set; }

    }
}
