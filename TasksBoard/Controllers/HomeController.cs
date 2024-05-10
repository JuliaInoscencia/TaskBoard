using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TasksBoard.Models;

namespace TasksBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static List<TaskBoard> tasks = new List<TaskBoard>()
        {
            new TaskBoard { Id = 1, Description = "Task 1", Status = TaskStatusEnum.ToDo },
            new TaskBoard { Id = 2, Description = "Task 2", Status = TaskStatusEnum.InProgress },
            new TaskBoard { Id = 3, Description = "Task 3", Status = TaskStatusEnum.Done }
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(tasks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
