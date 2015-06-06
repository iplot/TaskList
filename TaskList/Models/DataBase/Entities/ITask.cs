using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Models.DataBase.Entities
{
    public interface ITask
    {
        string Text { get; set; }
        bool IsFinished { get; set; }
    }
}
