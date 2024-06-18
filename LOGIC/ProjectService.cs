using LOGIC.Interfaces;
using LOGIC.Models;
using LOGIC.DTOs;

namespace LOGIC
{
    public class ProjectService
    {
        private IProjectRepository repository;
        private string connectionString;
        public ProjectService(IProjectRepository _projectRepository, string _connectionString)
        {
            repository = _projectRepository;
            repository.SetConnectionString(_connectionString);

            //connectionString = _connectionString;
        }

        public string InviteUser(ProjectInvitation projectInvitation)
        {
            ProjectInvitationDTO projectInvitationDTO = new ProjectInvitationDTO()
            {
                projectId = projectInvitation.projectId,
                userId = projectInvitation.userId
            };
            if (!repository.UserExists(projectInvitation.userId))
            {
                return "User does not exist";
            }
            if (repository.UserProjectExists(projectInvitationDTO))
            {
                return "User is already in the project";
            }
            if (repository.UserIsInvited(new ProjectInvitationDTO()
            {
                userId = projectInvitation.userId,
                projectId = projectInvitation.projectId
            }))
            {
                return "User is already invited";
            }
            repository.AddProjectInvitation(projectInvitationDTO);
            return "User invited";
        }

        public void AcceptInvite(int userId, int projectId)
        {
            repository.AcceptInvite(userId, projectId);
        }

        public void DeclineInvite(int userId, int projectId)
        {
            repository.DeclineInvite(userId, projectId);
        }

        public UserProjectViewData GetViewData(int UserId)
        {
            UserProjectViewData userProjectViewData = new UserProjectViewData();

            List<ProjectDTO> DTOprojects = new List<ProjectDTO>();
            DTOprojects.AddRange(repository.GetProjects(UserId));
            DTOprojects.AddRange(repository.GetInvitedProjects(UserId));
            List<ProjectInvitation> invites = GetInvites(UserId);
            List<Project> projects = DTOprojects.ConvertAll(p => new Project
            {
                Id = p.Id,
                OwnerId = p.OwnerId,
                ProjectName = p.ProjectName,
                CreationDate = p.CreationDate,
                LastEditDate = p.LastEditDate
            });
            userProjectViewData.projects.AddRange(projects);
            userProjectViewData.invites.AddRange(invites);

            return userProjectViewData;
        }

        private List<ProjectInvitation> GetInvites(int UserId)
        {
            List<ProjectInvitationDTO> projectInvitationDTOList = repository.GetInvites(UserId);
            List<ProjectInvitation> projectInvitationList = projectInvitationDTOList.ConvertAll(p => new ProjectInvitation
            {
                id = p.id,
                projectId = p.projectId,
                userId = p.userId
            });

            return projectInvitationList;
        }

        private List<Project> GetProjects(int UserId)
        {
            List<ProjectDTO> projectDTOList = repository.GetProjects(UserId);
            List<Project> projectList = projectDTOList.ConvertAll(p => new Project
            {
                Id = p.Id,
                OwnerId = p.OwnerId,
                ProjectName = p.ProjectName,
                CreationDate = p.CreationDate,
                LastEditDate = p.LastEditDate
            });

            return projectList;
        }

        public void RemoveProject(int ProjectId)
        {
            repository.RemoveProject(ProjectId);
        }

        public string AddProject(string projectName, int ownerId)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                return "Project name cannot be empty";
            }
            else if (projectName.Length > 100)
            {
                return "Project name cannot be longer than 100 characters";
            }

            repository.AddProject(new ProjectDTO()
            {
                OwnerId = ownerId,
                ProjectName = projectName
            });
            return null;
        }
    }
}
