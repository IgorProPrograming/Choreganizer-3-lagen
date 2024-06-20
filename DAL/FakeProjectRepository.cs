using LOGIC.DTOs;
using LOGIC.Interfaces;

namespace DAL
{
    public class FakeProjectRepository : IProjectRepository
    {
        private List<ProjectDTO> projects = new List<ProjectDTO>();
        private List<ProjectInvitationDTO> projectInvitations = new List<ProjectInvitationDTO>();
        private List<int> users = new List<int>();

        public FakeProjectRepository()
        {
            // Initialize with some mock data
            projects.Add(new ProjectDTO { Id = 1, ProjectName = "Project 1", OwnerId = 1 });
            projects.Add(new ProjectDTO { Id = 2, ProjectName = "Project 2", OwnerId = 2 });

            projectInvitations.Add(new ProjectInvitationDTO { id = 1, projectId = 1, userId = 2 });
            projectInvitations.Add(new ProjectInvitationDTO { id = 2, projectId = 2, userId = 1 });

            users.AddRange(new[] { 1, 2, 3 });
        }

        public void SetConnectionString(string _connectionString)
        {
            // Not applicable for mock
        }

        public List<ProjectDTO> GetInvitedProjects(int userId)
        {
            List<ProjectDTO> invitedProjects = new List<ProjectDTO>();
            foreach (var invite in projectInvitations)
            {
                if (invite.userId == userId)
                {
                    foreach (var project in projects)
                    {
                        if (project.Id == invite.projectId)
                        {
                            invitedProjects.Add(project);
                        }
                    }
                }
            }
            return invitedProjects;
        }

        public bool UserIsInvited(ProjectInvitationDTO projectInvitation)
        {
            foreach (var invite in projectInvitations)
            {
                if (invite.userId == projectInvitation.userId && invite.projectId == projectInvitation.projectId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UserProjectExists(ProjectInvitationDTO projectInvitation)
        {
            foreach (var project in projects)
            {
                if (project.Id == projectInvitation.projectId && project.OwnerId == projectInvitation.userId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UserExists(int userId)
        {
            foreach (var user in users)
            {
                if (user == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddProjectInvitation(ProjectInvitationDTO projectInvitation)
        {
            projectInvitations.Add(projectInvitation);
        }

        public void AcceptInvite(int UserId, int projectId)
        {
            projectInvitations.RemoveAll(invite => invite.userId == UserId && invite.projectId == projectId);
        }

        public void DeclineInvite(int UserId, int projectId)
        {
            projectInvitations.RemoveAll(invite => invite.userId == UserId && invite.projectId == projectId);
        }

        public List<ProjectInvitationDTO> GetInvites(int userId)
        {
            List<ProjectInvitationDTO> userInvites = new List<ProjectInvitationDTO>();
            foreach (var invite in projectInvitations)
            {
                if (invite.userId == userId)
                {
                    userInvites.Add(invite);
                }
            }
            return userInvites;
        }

        public List<ProjectDTO> GetProjects(int userId)
        {
            List<ProjectDTO> userProjects = new List<ProjectDTO>();
            foreach (var project in projects)
            {
                if (project.OwnerId == userId)
                {
                    userProjects.Add(project);
                }
            }
            return userProjects;
        }

        public void AddProject(ProjectDTO project)
        {
            projects.Add(project);
        }

        public void RemoveProject(int projectId)
        {
            projects.RemoveAll(project => project.Id == projectId);
        }
    }
}
