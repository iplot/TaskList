using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TaskList.Models.DataBase.DataProviders.TaskProvider
{
    public class TaskDbDataProvider : IDbDataProvider
    {
        public async Task<DataBaseResult> GetData()
        {
            try
            {
                using (TaskListContext session = new TaskListContext())
                {
                    var tasks = await session.Tasks.ToListAsync();

                    return new DataBaseResult {Errors = "", Success = tasks};
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult {Errors = e.Message, Success = null};
            }
        }

        public async Task<DataBaseResult> UpdateData(int targetId, IParameters param)
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
                    Entities.Task task = await session.Tasks.FirstAsync(t => t.TaskId == targetId);
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

                    await session.SaveChangesAsync();

                    return new DataBaseResult {Errors = "", Success = task};
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult {Errors = e.Message, Success = null};
            }
        }

        public async Task<DataBaseResult> DeleteData(int targetId)
        {
            try
            {
                using (TaskListContext session = new TaskListContext())
                {
                    var target = await session.Tasks.FirstAsync(f => f.TaskId == targetId);
                    session.Tasks.Remove(target);

                    await session.SaveChangesAsync();

                    return new DataBaseResult {Errors = "", Success = new object()};
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult {Errors = e.Message, Success = null};
            }
        }

        public async Task<DataBaseResult> AddData(object targetObject)
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
                    await session.SaveChangesAsync();

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