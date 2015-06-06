using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskList.Infrastructure;
using TaskList.Models;
using TaskList.Models.DataBase;
using TaskList.Models.DataBase.DataProviders;
using TaskList.Models.DataBase.DataProviders.SubTaskProvider;
using TaskList.Models.DataBase.DataProviders.TaskProvider;
using TaskList.Models.DataBase.Entities;

namespace TaskList.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            IDbDataProvider dataProvider = new CategoryDbDataProvider();
            DataBaseResult result = dataProvider.GetData();


            if (result.Success != null)
            {
                var data = (List<Category>)result.Success;
                return View(data);
            }
            else
            {
                return View("ErrorPage", result.Errors);
            }
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            IDbDataProvider dataProvider = new CategoryDbDataProvider();
            DataBaseResult result = dataProvider.AddData(category);

            if (result.Success != null)
            {
                var data = result.Success as Category;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View("ErrorPage", result.Errors);
            }
        }

        public ActionResult GetTasks(int categoryId)
        {
            IDbDataProvider provider = new TaskDbDataProvider();
            DataBaseResult result = provider.GetTasksByCategory(categoryId);

            if (result.Success != null)
            {
                var data = (List<Task>)result.Success;

                return PartialView(data);
            }
            else
            {
                return View("ErrorPage", result.Errors);
            }
        }

        [HttpPost]
        public ActionResult AddTask(Task newTask)
        {
            if (newTask.CategoryId == 0)
            {
                return Index();
            }

            IDbDataProvider provider = new TaskDbDataProvider();
            DataBaseResult result = provider.AddData(newTask);

            if (result.Success != null)
            {
                newTask = result.Success as Task;
                newTask.SubTasks = new List<SubTask>();

                return PartialView("Task", newTask);
            }
            else
            {
                return View("ErrorPage", result.Errors);
            }
        }

        [HttpPost]
        public ActionResult AddSubTask(SubTask subtask)
        {
            IDbDataProvider provider = new SubtaskDbDataProvider();
            DataBaseResult result = provider.AddData(subtask);

            if (result.Success != null)
            {
                var newSubTask = result.Success as SubTask;
                return Json(
                    new {TaskId = newSubTask.TaskId, Text = newSubTask.Text, IsFinished = newSubTask.IsFinished,
                        Id = newSubTask.SubTaskId},
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View("ErrorPage", result.Errors);
            }
        }

        [HttpPost]
        public void SaveTasks(List<TaskSaveViewModel> saveData)
        {
            if (saveData == null)
            {
                return;
            }

            var tasks = saveData.Where(d => d.TaskType == 0);
            var subtasks = saveData.Except(tasks);

            IDbDataProvider taskProvider = new TaskDbDataProvider();
            foreach (TaskSaveViewModel task in tasks)
            {
                taskProvider.UpdateData(task.Id,
                    new TaskParams {IsFinished = task.IsFinished, UpdateFinishedOnly = true});
            }

            IDbDataProvider subtaskProvider = new SubtaskDbDataProvider();
            foreach (TaskSaveViewModel subtask in subtasks)
            {
                subtaskProvider.UpdateData(subtask.Id,
                    new TaskParams {IsFinished = subtask.IsFinished, UpdateFinishedOnly = true});
            }
        }

        [HttpPost]
        public ActionResult DeleteTasks(List<TaskSaveViewModel> deleteData)
        {
            if (deleteData == null)
            {
                return new EmptyResult();
            }

            var tasks = deleteData.Where(d => d.TaskType == 0);
            var subtasks = deleteData.Except(tasks);

            IDbDataProvider taskProvider = new TaskDbDataProvider();
            foreach (TaskSaveViewModel task in tasks)
            {
                taskProvider.DeleteData(task.Id);
            }

            IDbDataProvider subtaskProvider = new SubtaskDbDataProvider();
            foreach (TaskSaveViewModel subtask in subtasks)
            {
                subtaskProvider.DeleteData(subtask.Id);
            }

            return Json("Ok", JsonRequestBehavior.AllowGet);

        }
    }
}