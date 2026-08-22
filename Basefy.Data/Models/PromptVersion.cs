using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basefy.Data.Models
{
    public class PromptVersion
    {
        public int Id { get; set; }
        public Prompt prompt { get; set; }
    }
}
