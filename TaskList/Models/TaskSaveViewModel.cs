using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class TaskSaveViewModel
    {
        public bool IsFinished { get; set; }

        public int TaskType { get; set; }

        public int Id { get; set; }
    }
}