using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basefy.Data.Models
{
    public class ToolVersion
    {
        public int Id { get; set; }
        public int ToolId { get; set; }
        public Tool Tool { get; set; }
    }
}
