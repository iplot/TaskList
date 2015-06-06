using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models.DataBase.Entities
{
    public class SubTask : ITask
    {
        public int SubTaskId { get; set; }

        public string Text { get; set; }

        public bool IsFinished { get; set; }

        public int TaskId { get; set; }

        public virtual Task Task { get; set; }
    }
}