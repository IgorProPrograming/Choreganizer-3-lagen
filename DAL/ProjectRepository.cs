using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using LOGIC.Interfaces;
using LOGIC.DTOs;

namespace DAL
{
    public class ProjectRepository : IProjectRepository
    {
        private string connectionString;

        public void SetConnectionString(string _connectionString)
        {
            connectionString = _connectionString;
        }

        public List<ProjectDTO> GetInvitedProjects (int userId)
        {
            List<ProjectDTO> projectList = new List<ProjectDTO>();
            using (SqlConnection s = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ProjectsAccess WHERE UserId = @Id", s);
                cmd.Parameters.AddWithValue("@Id", userId);
                cmd.CommandType = System.Data.CommandType.Text;
                s.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var project = new ProjectDTO();
                        project.Id = (int)rdr["ProjectId"];
                        projectList.Add(project);
                    }
                }
                foreach (var project in projectList)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM Projects WHERE Id = @Id", s);
                    cmd2.Parameters.AddWithValue("@Id", project.Id);
                    cmd2.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader rdr = cmd2.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            project.ProjectName = (string)rdr["ProjectName"];
                            project.OwnerId = (int)rdr["OwnerId"];
                        }
                    }
                }
                return projectList;
            }
        }


        public bool UserIsInvited(ProjectInvitationDTO projectInvitation)
        {
            using (SqlConnection s = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Invites WHERE UserId = @UserId AND ProjectId = @ProjectId", s);
                cmd.Parameters.AddWithValue("@UserId", projectInvitation.userId);
                cmd.Parameters.AddWithValue("@ProjectId", projectInvitation.projectId);
                cmd.CommandType = System.Data.CommandType.Text;
                s.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    return rdr.HasRows;
                }
            }
        }

        public bool UserProjectExists(ProjectInvitationDTO projectInvitation)
        {
            using (SqlConnection s = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ProjectsAccess WHERE UserId = @UserId AND ProjectId = @ProjectId", s);
                cmd.Parameters.AddWithValue("@UserId", projectInvitation.userId);
                cmd.Parameters.AddWithValue("@ProjectId",  projectInvitation.projectId);
                cmd.CommandType = System.Data.CommandType.Text;
                s.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    return rdr.HasRows;
                }
            }
        }

        public bool UserExists(int userId)
        {
            using (SqlConnection s = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Id = @Id", s);
                cmd.Parameters.AddWithValue("@Id", userId);
                cmd.CommandType = System.Data.CommandType.Text;
                s.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    return rdr.HasRows;
                }
            }
        }
        public void AddProjectInvitation(ProjectInvitationDTO projectInvitation)
        {
            using (SqlConnection s = new SqlConnection(connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Invites] ([ProjectId], [UserId]) VALUES (@ProjectId, @UserId)", s);
                cmd.Parameters.AddWithValue("@ProjectId", projectInvitation.projectId);
                cmd.Parameters.AddWithValue("@UserId", projectInvitation.userId);
                cmd.ExecuteNonQuery();
            }
        }


        public void AcceptInvite (int UserId, int projectId)
        {
            using (SqlConnection s = new SqlConnection(connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[ProjectsAccess] ([UserId], [ProjectId]) VALUES (@UserId, @ProjectId)", s);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ProjectId", projectId);
                cmd.ExecuteNonQuery();
            }

            using (SqlConnection s = new SqlConnection(connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Invites] WHERE UserId = @UserId AND ProjectId = @ProjectId", s);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ProjectId", projectId);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeclineInvite (int UserId, int projectId)
        {
            using (SqlConnection s = new SqlConnection(connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Invites] WHERE UserId = @UserId AND ProjectId = @ProjectId", s);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ProjectId", projectId);
                cmd.ExecuteNonQuery();
            }
        }
        public List<ProjectInvitationDTO> GetInvites (int userId)
        {
            List<ProjectInvitationDTO> projectInvitationList = new List<ProjectInvitationDTO>();
            using (SqlConnection s = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Invites WHERE UserId = @Id", s);
                cmd.Parameters.AddWithValue("@Id", userId);
                cmd.CommandType = System.Data.CommandType.Text;
                s.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var projectInvitation = new ProjectInvitationDTO();
                        projectInvitation.id = (int)rdr["Id"];
                        projectInvitation.projectId = (int)rdr["ProjectId"];
                        projectInvitation.userId = (int)rdr["UserId"];
                        projectInvitationList.Add(projectInvitation);
                    }
                }
                return projectInvitationList;
            }
        }
        public List<ProjectDTO> GetProjects(int userId)
        {
            List<ProjectDTO> projectList = new List<ProjectDTO>();
            using (SqlConnection s = new SqlConnection(connectionString))
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

        public void AddProject(ProjectDTO project)
        {
            using (SqlConnection s = new SqlConnection(connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Projects] ([ProjectName], [OwnerId]) VALUES (@ProjectName, @OwnerId)", s);
                cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                cmd.Parameters.AddWithValue("@OwnerId", project.OwnerId);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveProject(int projectId)
        {
            using (SqlConnection s = new SqlConnection(connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Projects] WHERE Id = @Id", s);
                cmd.Parameters.AddWithValue("@Id", projectId.ToString());
                cmd.ExecuteNonQuery();
            }
        }


    }
}
