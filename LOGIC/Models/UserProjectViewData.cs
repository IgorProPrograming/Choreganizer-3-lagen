using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Models
{
    public class UserProjectViewData
    {
        public List<Project> projects { get; set; }
        public List<ProjectInvitation> invites { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public UserProjectViewData()
        {
            projects = new List<Project>();
            invites = new List<ProjectInvitation>();
        }
    }
}
