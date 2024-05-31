using LOGIC.DTOs;


namespace LOGIC.Interfaces
{
    public interface IAuthenticationRepository
    {
        UserProfileDTO GetUserData(string username, string _connectionString);
        bool CheckIfUserExists(UserProfileDTO userProfile, string _connectionString);
        void RegisterUser(UserProfileDTO userProfile, string _connectionString);
        
    }
}
