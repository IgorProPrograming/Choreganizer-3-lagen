using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Models
{
    public class ProjectInvitation
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public int userId { get; set; }
    }
}
