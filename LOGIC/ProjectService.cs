using LOGIC.Interfaces;
using LOGIC.Models;
using LOGIC.DTOs;

namespace LOGIC
{
    public class ProjectService
    {
        private IProjectRepository repository;
        public ProjectService(IProjectRepository projectRepository, string connectionString)
        {
            repository = projectRepository;
        }

        public List<Project> GetProjects(int UserId, string connectionString)
        {
            List<ProjectDTO> projectDTOList = new List<ProjectDTO>();
            projectDTOList = repository.GetProjects(UserId, connectionString);
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

        public void RemoveProject(int ProjectId, string connectionString)
        {
            repository.RemoveProject(ProjectId, connectionString);
        }

        public string AddProject(string projectName, int ownerId, string connectionString)
        {
            if (projectName == null || projectName == "")
            {
                return "Project name cannot be empty";
            } else if (projectName.Length > 100)
            {
                return "Project name cannot be longer than 100 characters";
            } 

            repository.AddProject(
                new ProjectDTO()
                {
                    OwnerId = ownerId,
                    ProjectName = projectName
                }, connectionString);
            return null;
        }
    }
}
