using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models.DataBase.DataProviders
{
    public class TaskParams : IParameters
    {
        public string Text { get; set; }

        public bool IsFinished { get; set; }

        public DateTime Date { get; set; }

        public bool UpdateFinishedOnly { get; set; }
    }
}