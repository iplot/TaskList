using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaskList.Models.DataBase.Entities;

namespace TaskList.Models.DataBase
{
    public class TaskListContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<SubTask> SubTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasMany(t => t.SubTasks)
                .WithRequired(s => s.Task)
                .HasForeignKey(s => s.TaskId)
                .WillCascadeOnDelete();
        }
    }
}