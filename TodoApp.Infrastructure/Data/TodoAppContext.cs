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
                new Status { Id = 1, Name = "Pendiente" },
                new Status { Id = 2, Name = "Resuelta" });

            modelBuilder.Entity<TODO>().HasData(
                new TODO { Id = 1, Description = "Tarea uno", StatusId = 1 },
                new TODO { Id = 2, Description = "Tarea dos", StatusId = 1 });

            base.OnModelCreating(modelBuilder);
        }

    }
}
