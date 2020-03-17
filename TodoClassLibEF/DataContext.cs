using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoClassLib;

namespace TodoClassLibEF
{
    public class DataContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Fluent API
            modelBuilder.Entity<Todo>()
                .HasIndex(t => t.Title);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=todolist.db");
        }
        public DbSet<Todo> Todos { get; set; }

    }
}
