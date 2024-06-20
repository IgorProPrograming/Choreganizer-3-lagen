using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOGIC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using LOGIC.Models;
using LOGIC.Interfaces;

namespace LOGIC.Tests
{
    [TestClass()]
    public class AuthenticationServiceTests
    {
        private AuthenticationService authenticationService;
        private IAuthenticationRepository fakeAuthenticationRepository;

        [TestInitialize]
        public void SetUp()
        {
            fakeAuthenticationRepository = new FakeAuthenticationRepository();
            authenticationService = new AuthenticationService(fakeAuthenticationRepository, "fakeConnectionString");
        }

        [TestMethod()]
        public void Login_UserNotFound_ReturnsUserNotFoundMessage()
        {
            // Arrange
            UserProfile profile = new UserProfile { Username = "nonexistent", Password = "password" };

            // Act
            var result = authenticationService.Login(profile, "fakeConnectionString");

            // Assert
            Assert.AreEqual("User not found", result);
        }

        [TestMethod()]
        public void Login_IncorrectPassword_ReturnsIncorrectPasswordMessage()
        {
            // Arrange
            UserProfile profile = new UserProfile { Username = "user1", Password = "wronghash" };

            // Act
            var result = authenticationService.Login(profile, "fakeConnectionString");
            Console.WriteLine(result);


            // Assert
            Assert.AreEqual("Incorrect password", result);
        }

        [TestMethod()]
        public void CheckIfUserExists_UserExists_ReturnsTrue()
        {
            // Arrange
            string username = "user1";

            // Act
            var exists = authenticationService.CheckIfUserExists(username, "fakeConnectionString");

            // Assert
            Assert.IsTrue(exists);
        }

        [TestMethod()]
        public void GetUserId_ValidUser_ReturnsUserId()
        {
            // Arrange
            string username = "user1";

            // Act
            var userId = authenticationService.GetUserId(username, "fakeConnectionString");

            // Assert
            Assert.IsNotNull(userId);
            Assert.AreNotEqual("0", userId); // Assuming "0" would not be a valid user ID
        }

        [TestMethod()]
        public void Register_PasswordsDoNotMatch_ReturnsPasswordsDontMatchMessage()
        {
            // Arrange
            string username = "newuser";
            string password = "password";
            string passwordConfirmation = "differentpassword";

            // Act
            var result = authenticationService.Register(username, password, passwordConfirmation, "fakeConnectionString");

            // Assert
            Assert.AreEqual("Passwords don't match", result);
        }

        [TestMethod()]
        public void Register_UserAlreadyExists_ReturnsUserAlreadyExistsMessage()
        {
            // Arrange
            string username = "user1"; // Assuming this user already exists in the fake repository
            string password = "password";
            string passwordConfirmation = "password";

            // Act
            var result = authenticationService.Register(username, password, passwordConfirmation, "fakeConnectionString");

            // Assert
            Assert.AreEqual("User already exists", result);
        }

        [TestMethod()]
        public void Register_ValidData_ReturnsRegisteredSuccessfullyMessage()
        {
            // Arrange
            string username = "newuser";
            string password = "password";
            string passwordConfirmation = "password";

            // Act
            var result = authenticationService.Register(username, password, passwordConfirmation, "fakeConnectionString");

            // Assert
            Assert.AreEqual("Registered successfully", result);
        }
    }
}