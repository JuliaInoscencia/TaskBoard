using Microsoft.AspNetCore.Mvc;
using TasksBoard.Models;

namespace TasksBoard.Controllers;

public class TaskController : Controller
{
    private static List<TaskBoard> tasks = new List<TaskBoard>()
    {
        new TaskBoard { Id = 1, Description = "Task 1", Status = TaskStatusEnum.ToDo },
        new TaskBoard { Id = 2, Description = "Task 2", Status = TaskStatusEnum.InProgress },
        new TaskBoard { Id = 3, Description = "Task 3", Status = TaskStatusEnum.Done }
    };

    public IActionResult Index()
    {
        if (tasks == null)
        {
            tasks = new List<TaskBoard>();
        }

        return View(tasks);
    }

    public IActionResult AddTask()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddTask(TaskBoard task)
    {
        if (ModelState.IsValid)
        {
            if (tasks.Count == 0)
            {
                task.Id = 1;
            }
            else
            {
                task.Id = tasks[tasks.Count - 1].Id + 1;
            }
            task.StartDate = DateTime.Now;
            task.Status = TaskStatusEnum.ToDo;
            tasks.Add(task);
            TempData["Message"] = "Tarefa adicionada com sucesso.";
            return RedirectToAction("Index");
        }
        return View(task);
    }

    public IActionResult EditTask(int id)
    {
        TaskBoard task = tasks.Find(t => t.Id == id);
        if (task == null)
        {
            TempData["ErrorMessage"] = "Tarefa não encontrada.";
            return RedirectToAction("Index");
        }
        return View(task);
    }

    [HttpPost]
    public IActionResult EditTask(TaskBoard task)
    {
        if (ModelState.IsValid)
        {
            TaskBoard existingTask = tasks.Find(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Description = task.Description;
                TempData["Message"] = "Tarefa atualizada com sucesso.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Tarefa não encontrada.";
            }
        }
        return View(task);
    }

    public IActionResult DeleteTask(int id)
    {
        TaskBoard task = tasks.Find(t => t.Id == id);
        if (task == null)
        {
            TempData["ErrorMessage"] = "Tarefa não encontrada.";
            return RedirectToAction("Index");
        }
        return View(task);
    }

    [HttpPost, ActionName("DeleteTask")]
    public IActionResult ConfirmDeleteTask(int id)
    {
        TaskBoard task = tasks.Find(t => t.Id == id);
        if (task != null)
        {
            tasks.Remove(task);
            TempData["Message"] = "Tarefa excluída com sucesso.";
        }
        else
        {
            TempData["ErrorMessage"] = "Tarefa não encontrada.";
        }
        return RedirectToAction("Index");
    }

    public IActionResult ToDo()
    {
        var todoTasks = tasks.Where(t => t.Status == TaskStatusEnum.ToDo).ToList();
        return View("ToDo", todoTasks);
    }

    public IActionResult InProgress()
    {
        var inProgressTasks = tasks.Where(t => t.Status == TaskStatusEnum.InProgress).ToList();
        return View("InProgress", inProgressTasks);
    }

    public IActionResult Done()
    {
        var doneTasks = tasks.Where(t => t.Status == TaskStatusEnum.Done).ToList();
        return View("Done", doneTasks);
    }


    [HttpPost]
    public IActionResult ChangeStatus(int id, string status)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            switch (status)
            {
                case "ToDo":
                    task.Status = TaskStatusEnum.ToDo;
                    break;
                case "InProgress":
                    task.Status = TaskStatusEnum.InProgress;
                    break;
                case "Done":
                    task.Status = TaskStatusEnum.Done;
                    task.CompleteDate = DateTime.Now;
                    break;
                default:
                    break;
            }
            TempData["Message"] = "Status da tarefa alterado com sucesso.";
        }
        else
        {
            TempData["ErrorMessage"] = "Tarefa não encontrada.";
        }
        return RedirectToAction("Index");
    }
}
