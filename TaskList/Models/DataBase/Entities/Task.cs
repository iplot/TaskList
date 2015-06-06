using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models.DataBase.Entities
{
    public class Task : ITask
    {
        public int TaskId { get; set; }

        public string Text { get; set; }

        public bool IsFinished { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<SubTask> SubTasks { get; set; }
    }
}