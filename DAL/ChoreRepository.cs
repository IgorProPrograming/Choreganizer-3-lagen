using Microsoft.Data.SqlClient;
using LOGIC;
using LOGIC.DTOs;


namespace DAL
{
    public class ChoreRepository : IChoreRepository
    {
        public List<ChoreDTO> GetChores(int projectId, string _connectionString)
        {
            List<ChoreDTO> choreList = new List<ChoreDTO>();
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Chores WHERE ProjectId = @ProjectId", s);
                cmd.Parameters.AddWithValue("@ProjectId", projectId);
                cmd.CommandType = System.Data.CommandType.Text;
                s.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var chore = new ChoreDTO();
                        chore.Id = (int)rdr["Id"];
                        chore.ChoreName = (string)rdr["Chore"];
                        chore.Finished = (bool)rdr["Completed"];
                        choreList.Add(chore);
                    }
                }
            }
            return choreList;
        }

        public void AddChore(string chore, int projectId, string _connectionString)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Chores] ([Chore], [ProjectId], [Completed]) VALUES (@Chore, @ProjectId, 0)\r\n", s);
                cmd.Parameters.AddWithValue("@Chore", chore);
                cmd.Parameters.AddWithValue("@ProjectId", projectId);
                //cmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveChore(int choreId, string _connectionString)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Chores] WHERE Id = @Id", s);
                cmd.Parameters.AddWithValue("@Id", choreId.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        public void ToggleChoreStatus(int choreId, int finished, string _connectionString)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Chores] SET [Completed] = @Completed WHERE Id = @Id", s);
                cmd.Parameters.AddWithValue("@Id", choreId);
                cmd.Parameters.AddWithValue("@Completed", finished);
                cmd.ExecuteNonQuery();
            }

        }

        public ChoreDTO GetChore(int choreId, string _connectionString)
        {
            ChoreDTO chore = new ChoreDTO();
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Chores WHERE Id = @Id", s);
                cmd.Parameters.AddWithValue("@Id", choreId);
                cmd.CommandType = System.Data.CommandType.Text;
                s.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        chore.Id = (int)rdr["Id"];
                        chore.ChoreName = (string)rdr["Chore"];
                        chore.Finished = (bool)rdr["Completed"];
                    }
                }
            }
            return chore;
        }

        public void UpdateChore(ChoreDTO chore, string _connectionString)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Chores] SET [Chore] = @Chore, [Completed] = @Completed, [Deadline] = @Deadline, WHERE Id = @Id", s);
                cmd.Parameters.AddWithValue("@Id", chore.Id);
                cmd.Parameters.AddWithValue("@Chore", chore.ChoreName);
                cmd.Parameters.AddWithValue("@Completed", chore.Finished);
                cmd.Parameters.AddWithValue("@Deadline", chore.Deadline);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
