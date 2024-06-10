using Microsoft.AspNetCore.Mvc;
using LOGIC;
using LOGIC.Models;

namespace Choreganizer_webapp.Controllers
{
    public class ChoreTableController : Controller
    {
        private readonly string _connectionString;
        private readonly IChoreRepository _choreRepository;
        private readonly ChoreService _choreService;

        public ChoreTableController(IConfiguration configuration, IChoreRepository choreRepository)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _choreRepository = new DAL.ChoreRepository();
            _choreService = new ChoreService(_choreRepository, _connectionString);
        }


        public IActionResult Index()
        {
            int projectId = int.Parse(HttpContext.Session.GetString("CurrentProjectId"));
            List<Chore> choreList = _choreService.GetChores(projectId, _connectionString);

            return View(choreList);
        }

        public ActionResult Remove(int choreId)
        {
            _choreService.RemoveChore(choreId, _connectionString);
            return RedirectToAction("Index", "ChoreTable");
        }

        public ActionResult Add(string choreName)
        {
            int projectId = int.Parse(HttpContext.Session.GetString("CurrentProjectId"));
            string message = _choreService.AddChore(choreName, projectId, _connectionString);
            switch (message)
            {
                case "Chore added":
                    break;
                case "Chore name is too long":
                    TempData["Message"] = "Chore name is too long";
                    break;
                case "Chore name cannot be empty":
                    TempData["Message"] = "Chore name cannot be empty";
                    break;
            }
            return RedirectToAction("Index");
        }

        public ActionResult ToggleState(int choreId, bool lastState)
        {
            _choreService.ToggleChoreStatus(choreId, lastState, _connectionString);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int choreId)
        {
            Chore chore = _choreService.GetChore(choreId, _connectionString);
            return View(chore);
        }


        public ActionResult BackToList()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(int choreId, string choreName, string choreDescription, DateTime deadlineDate)
        {
            Chore chore = new Chore()
            {
                Id = choreId,
                ChoreName = choreName,
                Deadline = deadlineDate
            };

            _choreService.UpdateChore(chore, _connectionString);
            return RedirectToAction("Index");
        }

    }
}
