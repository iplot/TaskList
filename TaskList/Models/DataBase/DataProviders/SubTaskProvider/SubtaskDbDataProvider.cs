using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TaskList.Models.DataBase.Entities;

namespace TaskList.Models.DataBase.DataProviders.SubTaskProvider
{
    public class SubtaskDbDataProvider : IDbDataProvider
    {
        public async Task<DataBaseResult> GetData()
        {
            try
            {
                using (TaskListContext session = new TaskListContext())
                {
                    var subtasks = await session.SubTasks.ToListAsync();

                    return new DataBaseResult {Errors = "", Success = subtasks};
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
                var subtaskParam = param as TaskParams;
                if (subtaskParam == null)
                {
                    throw new Exception("Invalid type of parameter");
                }

                using (TaskListContext session = new TaskListContext())
                {
                    var subtask = await session.SubTasks.FirstAsync(s => s.SubTaskId == targetId);

                    if (!subtaskParam.UpdateFinishedOnly)
                    {
                        subtask.IsFinished = subtaskParam.IsFinished;
                        subtask.Text = subtask.Text;
                    }
                    else
                    {
                        subtask.IsFinished = subtaskParam.IsFinished;
                    }

                    await session.SaveChangesAsync();

                    return new DataBaseResult {Errors = "", Success = subtask};
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
                    var subtask = await session.SubTasks.FirstAsync(s => s.SubTaskId == targetId);
                    session.SubTasks.Remove(subtask);

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
                var subtask = targetObject as SubTask;

                if (subtask == null)
                {
                    throw new Exception("Invalid type of parameter");
                }

                using (TaskListContext session = new TaskListContext())
                {
                    session.SubTasks.Add(subtask);
                    await session.SaveChangesAsync();

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