using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskList.Models.DataBase.Entities;

namespace TaskList.Models.DataBase.DataProviders.SubTaskProvider
{
    public class SubtaskDbDataProvider : IDbDataProvider
    {
        public DataBaseResult GetData()
        {
            try
            {
                using (TaskListContext session = new TaskListContext())
                {
                    var subtasks = session.SubTasks.ToList();

                    return new DataBaseResult {Errors = "", Success = subtasks};
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
                var subtaskParam = param as TaskParams;
                if (subtaskParam == null)
                {
                    throw new Exception("Invalid type of parameter");
                }

                using (TaskListContext session = new TaskListContext())
                {
                    var subtask = session.SubTasks.First(s => s.SubTaskId == targetId);

                    if (!subtaskParam.UpdateFinishedOnly)
                    {
                        subtask.IsFinished = subtaskParam.IsFinished;
                        subtask.Text = subtask.Text;
                    }
                    else
                    {
                        subtask.IsFinished = subtaskParam.IsFinished;
                    }

                    session.SaveChanges();

                    return new DataBaseResult {Errors = "", Success = subtask};
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
                    var subtask = session.SubTasks.First(s => s.SubTaskId == targetId);
                    session.SubTasks.Remove(subtask);

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
                var subtask = targetObject as SubTask;

                if (subtask == null)
                {
                    throw new Exception("Invalid type of parameter");
                }

                using (TaskListContext session = new TaskListContext())
                {
                    session.SubTasks.Add(subtask);
                    session.SaveChanges();

                    return new DataBaseResult {Errors = "", Success = subtask};
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult {Errors = e.Message, Success = null};
            }
        }
    }
}