using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.DTOs
{
    public class ProjectInvitationDTO
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public int userId { get; set; }
    }
}
