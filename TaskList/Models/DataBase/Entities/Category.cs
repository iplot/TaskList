using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskList.Models.DataBase.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual List<Task> Tasks { get; set; }
    }
}