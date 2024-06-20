using Microsoft.Data.SqlClient;
using LOGIC.Interfaces;
using LOGIC.Models;
using LOGIC.DTOs;

namespace DAL
{
    public class AuthenticationRepository// : IAuthenticationRepository
    {
        public UserProfileDTO GetUserData(string username, string _connectionString)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Users] WHERE [UserName] = @UserName", s);
                cmd.Parameters.AddWithValue("@UserName", username);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        return new UserProfileDTO
                        {
                            Id = rdr.GetInt32(0),
                            Username = rdr.GetString(1),
                            PasswordHash = rdr.GetString(2)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public bool CheckIfUserExists(UserProfileDTO userProfile, string _connectionString)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Users] WHERE [UserName] = @UserName", s);
                cmd.Parameters.AddWithValue("@UserName", userProfile.Username);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    return rdr.HasRows;
                }
            }
        }

        public void RegisterUser(UserProfileDTO userProfile, string _connectionString)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Users] ([UserName], [PasswordHash]) VALUES (@UserName, @PasswordHash)", s);
                cmd.Parameters.AddWithValue("@UserName", userProfile.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", userProfile.PasswordHash);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
