using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models.DataBase
{
    public class CategoryParams : IParameters
    {
        public string NewName { get; set; }
    }
}