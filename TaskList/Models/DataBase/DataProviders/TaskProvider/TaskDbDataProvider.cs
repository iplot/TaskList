using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models.DataBase.DataProviders.TaskProvider
{
    public class TaskDbDataProvider : IDbDataProvider
    {
        public DataBaseResult GetData()
        {
            try
            {
                using (TaskListContext session = new TaskListContext())
                {
                    var tasks = session.Tasks.ToList();

                    return new DataBaseResult {Errors = "", Success = tasks};
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult {Errors = e.Message, Success = null};
            }
        }

        public DataBaseResult UpdateData(int targetId, IParameters param)
        {
            try
            {
                var taskParam = param as TaskParams;
                if (taskParam == null)
                {
                    throw new Exception("Invalid type of parameter");
                }

                using (TaskListContext session = new TaskListContext())
                {
                    Entities.Task task = session.Tasks.First(t => t.TaskId == targetId);
                    if (!taskParam.UpdateFinishedOnly)
                    {
                        task.Text = taskParam.Text;
                        task.Date = taskParam.Date;
                        task.IsFinished = taskParam.IsFinished;
                    }
                    else
                    {
                        task.IsFinished = taskParam.IsFinished;
                    }

                    session.SaveChanges();

                    return new DataBaseResult {Errors = "", Success = task};
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult {Errors = e.Message, Success = null};
            }
        }

        public DataBaseResult DeleteData(int targetId)
        {
            try
            {
                using (TaskListContext session = new TaskListContext())
                {
                    var target = session.Tasks.First(f => f.TaskId == targetId);
                    session.Tasks.Remove(target);

                    session.SaveChanges();

                    return new DataBaseResult {Errors = "", Success = new object()};
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult {Errors = e.Message, Success = null};
            }
        }

        public DataBaseResult AddData(object targetObject)
        {
            try
            {
                Entities.Task task = targetObject as Entities.Task;
                if (task == null)
                {
                    throw new Exception("Invalid type of parameter");
                }

                using (TaskListContext session = new TaskListContext())
                {
                    session.Tasks.Add(task);
                    session.SaveChanges();

                    return new DataBaseResult {Errors = "", Success = task};
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult {Errors = e.Message, Success = null};
            }
        }
    }
}