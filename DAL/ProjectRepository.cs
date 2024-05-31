using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using LOGIC.Interfaces;
using LOGIC.DTOs;

namespace DAL
{
    public class ProjectRepository : IProjectRepository
    {
        public List<ProjectDTO> GetProjects(int userId, string _connectionString)
        {
            List<ProjectDTO> projectList = new List<ProjectDTO>();
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Projects WHERE OwnerId = @Id", s);
                cmd.Parameters.AddWithValue("@Id", userId);
                cmd.CommandType = System.Data.CommandType.Text;
                s.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var project = new ProjectDTO();
                        project.Id = (int)rdr["Id"];
                        project.ProjectName = (string)rdr["ProjectName"];
                        //project.CreationDate = (DateTime)rdr["CreationDate"];
                        //project.LastEditDate = (DateTime)rdr["LastEditDate"];
                        projectList.Add(project);
                    }
                }
                return projectList;
            }
        }

        public void AddProject(ProjectDTO project, string _connectionString)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Projects] ([ProjectName], [OwnerId]) VALUES (@ProjectName, @OwnerId)", s);
                cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                cmd.Parameters.AddWithValue("@OwnerId", project.OwnerId);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveProject(int projectId, string _connectionString)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Projects] WHERE Id = @Id", s);
                cmd.Parameters.AddWithValue("@Id", projectId.ToString());
                cmd.ExecuteNonQuery();
            }
        }
    }
}
