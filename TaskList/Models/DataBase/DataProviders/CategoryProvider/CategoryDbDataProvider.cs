using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using TaskList.Models.DataBase.Entities;

namespace TaskList.Models.DataBase.DataProviders
{
    public class CategoryDbDataProvider : IDbDataProvider
    {
        public async Task<DataBaseResult> GetData()
        {
            try
            {
                using (TaskListContext session = new TaskListContext())
                {
                    var categories = await session.Categories.ToListAsync();

                    return new DataBaseResult{Errors = "", Success = categories};
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
                var catParam = param as CategoryParams;
                if (catParam == null)
                {
                    throw new Exception("Invalid type of parameter");
                }

                using (TaskListContext session = new TaskListContext())
                {
                    var category = await session.Categories.FirstAsync(c => c.CategoryId == targetId);
                    category.Name = catParam.NewName;

                    await session.SaveChangesAsync();

                    return new DataBaseResult { Errors = "", Success = category };
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult { Errors = e.Message, Success = null };
            }
        }

        public async Task<DataBaseResult> DeleteData(int targetId)
        {
            try
            {
                using (TaskListContext session = new TaskListContext())
                {
                    var category = await session.Categories.FirstAsync(c => c.CategoryId == targetId);
                    session.Categories.Remove(category);

                    await session.SaveChangesAsync();

                    return new DataBaseResult {Errors = "", Success = new object()};
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult{Errors = e.Message, Success = null};
            }
        }

        public async Task<DataBaseResult> AddData(object targetData)
        {
            try
            {
                Category newCategory = targetData as Category;
                if (newCategory == null)
                {
                    throw new Exception("Invalid type of parameter");
                }

                using (TaskListContext session = new TaskListContext())
                {
                    session.Categories.Add(newCategory);
                    await session.SaveChangesAsync();

                    return new DataBaseResult {Errors = "", Success = newCategory};
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult {Errors = e.Message, Success = null};
            }
        }
    }
}