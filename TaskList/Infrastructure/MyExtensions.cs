﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TaskList.Models.DataBase;

namespace TaskList.Infrastructure
{
    public static class MyExtensions
    {
        public static async Task<DataBaseResult> GetTasksByCategory(this IDbDataProvider provider, int categoryId)
        {
            try
            {
                using (TaskListContext session = new TaskListContext())
                {
                    var tasks = await session.Tasks
                        .Where(t => t.CategoryId == categoryId)
                        .Include(t => t.SubTasks)
//                        .Include(t => t.Category)
                        .ToListAsync();

                    return new DataBaseResult { Errors = "", Success = tasks };
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult { Errors = e.Message, Success = null };
            }
        }
    }
}