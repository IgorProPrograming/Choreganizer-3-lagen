using LOGIC.Interfaces;
using LOGIC.Models;
using LOGIC;
using Microsoft.AspNetCore.Mvc;


namespace Choreganizer_webapp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly string _connectionString;
        private readonly IProjectRepository _projectRepository;
        private readonly ProjectService _projectService;

        public ProjectController(IConfiguration configuration, IProjectRepository projectRepository)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _projectRepository = projectRepository;
            _projectService = new ProjectService(_projectRepository, _connectionString);
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Index", "Authentication");
            }

            int ownerId = int.Parse(HttpContext.Session.GetString("UserId"));
            TempData["UserId"] = ownerId;
            UserProjectViewData projectViewData = _projectService.GetViewData(ownerId);
            return View(projectViewData);
        }
        
        public IActionResult AddUser(int projectId)
        {
            return View(projectId);
        }

        public IActionResult InviteUser(int projectId, int userId)
        {
            ProjectInvitation projectInvitation = new ProjectInvitation()
            {
                projectId = projectId,
                userId = userId
            };
            string message = _projectService.InviteUser(projectInvitation);
            switch (message)
            {
                case "User does not exist":
                    TempData["Error"] = "User does not exist";
                    break;
                case "User is already in the project":
                    TempData["Error"] = "User is already in the project";
                    break;
                case "User is already invited":
                    TempData["Error"] = "User is already invited";
                    break;
                case "User invited":
                    TempData["Message"] = "User invited";
                    return RedirectToAction("Index");
            }
            return RedirectToAction("AddUser");
        }
        public IActionResult AcceptInvite(int projectId)
        {
            _projectService.AcceptInvite(int.Parse(HttpContext.Session.GetString("UserId")), projectId);
            return RedirectToAction("Index");
        }

        public IActionResult DeclineInvite(int projectId)
        {
            _projectService.DeclineInvite(int.Parse(HttpContext.Session.GetString("UserId")), projectId);
            return RedirectToAction("Index");
        }

        public IActionResult OpenProject(int projectId)
        {
            HttpContext.Session.SetString("CurrentProjectId", projectId.ToString());
            return RedirectToAction("Index", "ChoreTable");
        }

        public IActionResult Remove(int projectId)
        {
            _projectService.RemoveProject(projectId);
            return RedirectToAction("Index");
        }

        public IActionResult Add(string projectName)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            string message = _projectService.AddProject(projectName, userId);
            switch (message)
            {
                case "Project name cannot be empty":
                    TempData["Error"] = "Project name cannot be empty";
                    break;
                case "Project name cannot be longer than 100 characters":
                    TempData["Error"] = "Project name cannot be longer than 100 characters";
                    break;
            }
            return RedirectToAction("Index");
        }
    }
}
