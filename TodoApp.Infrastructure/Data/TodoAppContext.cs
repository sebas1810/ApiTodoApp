using TodoApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Infrastructure.Data
{
    public class TodoAppContext : DbContext
    {
        public TodoAppContext(DbContextOptions<TodoAppContext> options) : base(options) { }

        public DbSet<TODO> TODO { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Pending" },
                new Status { Id = 2, Name = "Done" });

            modelBuilder.Entity<TODO>().HasData(
                new TODO { Id = 1, Description = "Task One", StatusId = 1 },
                new TODO { Id = 2, Description = "Task Two", StatusId = 1 });

            base.OnModelCreating(modelBuilder);
        }

    }
}
