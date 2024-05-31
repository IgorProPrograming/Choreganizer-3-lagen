using System.Text;
using System.Security.Cryptography;
using LOGIC.Models;
using LOGIC.Interfaces;
using LOGIC.DTOs;

namespace LOGIC
{
    public class AuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationService(IAuthenticationRepository authenticationRepository, string _connectionString)
        {
            _authenticationRepository = authenticationRepository;
        }
        public string Login(UserProfile profile, string _connectionString)
        {
            UserProfileDTO userProfileDTO = _authenticationRepository.GetUserData(profile.Username, _connectionString);
            if (userProfileDTO == null)
            {
                return "User not found";
            }
            else
            {
                if (PasswordCheck(profile.Password, userProfileDTO.PasswordHash))
                {
                    return userProfileDTO.Id.ToString();
                }
                else
                {
                    return "Incorrect password";
                }
            }
        }

        public bool PasswordCheck(string password, string passwordHash)
        {
            using (SHA256 hashAlgorithm = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] passwordHashBytes = Convert.FromBase64String(passwordHash);
                byte[] computedPasswordHash = hashAlgorithm.ComputeHash(passwordBytes);
                return computedPasswordHash.SequenceEqual(passwordHashBytes);
            }
        }

        public string Register(string username, string password, string passwordConfirmation, string _connectionString)
        {
            if (password == passwordConfirmation)
            {
                using (SHA256 hashAlgorithm = SHA256.Create())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    byte[] passwordHash = hashAlgorithm.ComputeHash(passwordBytes);
                    string passwordHashString = Convert.ToBase64String(passwordHash);

                    UserProfileDTO userProfileDTO = new UserProfileDTO();
                    userProfileDTO.Username = username;
                    userProfileDTO.PasswordHash = passwordHashString;

                    if (!_authenticationRepository.CheckIfUserExists(userProfileDTO, _connectionString))
                    {
                        _authenticationRepository.RegisterUser(userProfileDTO, _connectionString);
                        return "Registered successfully";
                    }
                    else
                    {
                        return "User already exists";
                    }
                }
            }
            else
            {
                return "Passwords don't match";
            }
        }
    }
    }

