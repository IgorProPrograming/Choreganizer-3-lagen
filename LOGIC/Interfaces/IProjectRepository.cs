using LOGIC.DTOs;

namespace LOGIC.Interfaces
{
    public interface IProjectRepository
    {
        List<ProjectDTO> GetProjects(int id, string connectionString);
        void AddProject(ProjectDTO project, string connectionString);
        void RemoveProject(int projectId, string connectionString);

    }

}
