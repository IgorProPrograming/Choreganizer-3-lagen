using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOGIC.DTOs;
using LOGIC.Interfaces;
       
namespace DAL
{
    public class FakeAuthenticationRepository : IAuthenticationRepository
    {
        private List<UserProfileDTO> users = new List<UserProfileDTO>();

        public FakeAuthenticationRepository()
        {
            users.Add(new UserProfileDTO { Id = 1, Username = "user1", PasswordHash = "hash" });
            users.Add(new UserProfileDTO { Id = 2, Username = "user2", PasswordHash = "hash" });
        }

        public UserProfileDTO GetUserData(string username, string _connectionString)
        {
            foreach (var user in users)
            {
                if (user.Username == username)
                {
                    return user;
                }
            }
            return null;
        }

        public bool CheckIfUserExists(UserProfileDTO userProfile, string _connectionString)
        {
            foreach (var user in users)
            {
                if (user.Username == userProfile.Username)
                {
                    return true;
                }
            }
            return false;
        }

        public void RegisterUser(UserProfileDTO userProfile, string _connectionString)
        {
            
                int nextId = 1;
                foreach (var user in users)
                {
                    if (user.Id >= nextId)
                    {
                        nextId = user.Id + 1;
                    }
                }
                userProfile.Id = nextId;
                users.Add(userProfile);
            
        }
    }
}
