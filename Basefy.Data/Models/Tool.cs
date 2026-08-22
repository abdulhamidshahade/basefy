using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basefy.Data.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PromptVersionId { get; set; }
        public PromptVersion PromptVersion { get; set; }
        public List<ToolVersion> ToolVersions { get; set; }
    }
}
