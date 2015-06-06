using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TaskList.Models.DataBase.Entities;

namespace TaskList.Models.DataBase.DataProviders
{
    public class CategoryDbDataProvider : IDbDataProvider
    {
        public DataBaseResult GetData()
        {
            try
            {
                using (TaskListContext session = new TaskListContext())
                {
                    var categories = session.Categories.ToList();

                    return new DataBaseResult{Errors = "", Success = categories};
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
                var catParam = param as CategoryParams;
                if (catParam == null)
                {
                    throw new Exception("Invalid type of parameter");
                }

                using (TaskListContext session = new TaskListContext())
                {
                    var category = session.Categories.First(c => c.CategoryId == targetId);
                    category.Name = catParam.NewName;

                    session.SaveChanges();

                    return new DataBaseResult { Errors = "", Success = category };
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult { Errors = e.Message, Success = null };
            }
        }

        public DataBaseResult DeleteData(int targetId)
        {
            try
            {
                using (TaskListContext session = new TaskListContext())
                {
                    var category = session.Categories.First(c => c.CategoryId == targetId);
                    session.Categories.Remove(category);

                    session.SaveChanges();

                    return new DataBaseResult {Errors = "", Success = new object()};
                }
            }
            catch (Exception e)
            {
                return new DataBaseResult{Errors = e.Message, Success = null};
            }
        }

        public DataBaseResult AddData(object targetData)
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
                    session.SaveChanges();

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