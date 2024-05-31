using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOGIC.Models;

namespace DAL
{
    public class FakeProjectRepo// : IProjectRepository
    {
        public List<Project> GetProjects(int id)
        {
            List<Project> projectDTOs = new List<Project>();

            projectDTOs.Add(new Project
            {
                Id = 1,
                OwnerId = 2,
                ProjectName = "test1",
                CreationDate = DateTime.Now,
                LastEditDate = DateTime.Now
            });

            projectDTOs.Add(new Project
            {
                Id = 2,
                OwnerId = 3,
                ProjectName = "test2",
                CreationDate = DateTime.Now,
                LastEditDate = DateTime.Now
            });

            return projectDTOs;
        }

        public void AddProject(Project project)
        {
            throw new NotImplementedException();
        }

        public void RemoveProject(int projectId)
        {
            throw new NotImplementedException();
        }
    }
}
