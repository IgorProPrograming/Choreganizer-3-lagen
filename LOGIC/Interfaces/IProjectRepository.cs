using LOGIC.DTOs;

namespace LOGIC.Interfaces
{
    public interface IProjectRepository
    {
        void SetConnectionString(string _connectionString);
        List<ProjectDTO> GetProjects(int id);
        List<ProjectInvitationDTO> GetInvites(int userId);
        void AddProject(ProjectDTO project);
        void RemoveProject(int projectId);
        void AcceptInvite(int userId, int projectId);
        void DeclineInvite(int userId, int projectId);
        void AddProjectInvitation(ProjectInvitationDTO projectInvitation);
        bool UserExists(int userId);
        bool UserProjectExists(ProjectInvitationDTO projectInvitation);
        bool UserIsInvited(ProjectInvitationDTO projectInvitation);

        List<ProjectDTO> GetInvitedProjects(int userId);



    }

}
