using LOGIC;
using LOGIC.Interfaces; // Ensure this is included for IProjectRepository
using LOGIC.Models;
using DAL;

namespace LOGIC.Tests
{
    [TestClass()]
    public class ProjectServiceTests
    {
        private IProjectRepository fakeRepository;
        private ProjectService projectService;

        [TestInitialize]
        public void SetUp()
        {
            fakeRepository = new FakeProjectRepository();
            projectService = new ProjectService(fakeRepository, "fakeConnectionString");
        }

        [TestMethod()]
        public void InviteUser_UserDoesNotExist_ReturnsUserDoesNotExistMessage()
        {
            // Arrange
            var invitation = new ProjectInvitation { projectId = 1, userId = 999 };

            // Act
            var result = projectService.InviteUser(invitation);

            // Assert
            Assert.AreEqual("User does not exist", result);
        }

        [TestMethod()]
        public void AcceptInvite_InvitationExists_InvitationRemoved()
        {
            // Arrange
            int userId = 2, projectId = 1;

            // Act
            projectService.AcceptInvite(userId, projectId);

            // Assert
            var invitesAfterAcceptance = fakeRepository.GetInvites(userId);
            Assert.IsFalse(invitesAfterAcceptance.Exists(invite => invite.projectId == projectId && invite.userId == userId));
        }

        [TestMethod()]
        public void GetViewData_ReturnsCorrectData()
        {
            // Arrange
            int userId = 1;

            // Act
            var result = projectService.GetViewData(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UserProjectViewData));

        }

        [TestMethod()]
        public void AddProject_ValidData_AddsProjectSuccessfully()
        {
            // Arrange
            string projectName = "New Project";
            int ownerId = 1;

            // Act
            projectService.AddProject(projectName, ownerId);

            // Assert
            var projects = fakeRepository.GetProjects(ownerId);
            Assert.IsTrue(projects.Any(p => p.ProjectName == projectName && p.OwnerId == ownerId));
        }

        [TestMethod()]
        public void RemoveProject_ProjectExists_ProjectRemoved()
        {
            // Arrange
            int projectIdToRemove = 1; // Assuming this project ID exists in the fake repository's initial mock data

            // Act
            projectService.RemoveProject(projectIdToRemove);

            // Assert
            var project = fakeRepository.GetProjects(1).FirstOrDefault(p => p.Id == projectIdToRemove);
            Assert.IsNull(project);
        }

        [TestMethod()]
        public void DeclineInvite_InvitationExists_InvitationRemoved()
        {
            // Arrange
            int userId = 2, projectId = 1; // Use existing invitation data from FakeProjectRepo initialization

            // Act
            projectService.DeclineInvite(userId, projectId);

            // Assert
            var invitesAfterDecline = fakeRepository.GetInvites(userId);
            Assert.IsFalse(invitesAfterDecline.Exists(invite => invite.projectId == projectId && invite.userId == userId));
        }

    }
}