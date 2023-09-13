using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
{
    public class TodoContext : DbContext
    {
        private readonly string fileName = "data.json";

        public TodoContext()
        {
        }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("TodoDb");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(x => x.CategoryId);
            modelBuilder.Entity<Models.Task>().HasKey(x => x.TaskId);
            modelBuilder.Entity<User>().HasKey(x => x.UserId);

            modelBuilder.Entity<Category>(entity => { entity.Property(e => e.CategoryId).IsRequired(); });
            modelBuilder.Entity<User>(entity => { entity.Property(e => e.UserId).IsRequired(); });

            modelBuilder.Entity<Models.Task>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey("CategoryId");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey("UserId");
            });

            if (File.Exists(fileName))
            {
                ReadDataFromFile(modelBuilder);
            }
            else
            {
                // Seed initial data if the JSON file does not exist
                SeedInitialData(modelBuilder);
            }
        }

        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 10, Name = "Uni" },
                new Category { CategoryId = 20, Name = "Work" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 101, FirstName = "Jan", LastName = "Doe" },
                new User { UserId = 102, FirstName = "Mr.", LastName = "Smith" }
            );

            modelBuilder.Entity<Models.Task>().HasData(
                new Models.Task
                {
                    TaskId = 1,
                    Name = "School Task",
                    CategoryId = 10,
                    UserId = 101,
                    StartDate = new DateTime(2023, 05, 01),
                    DueDate = new DateTime(2023, 09, 15),
                    Description = "Task1",
                    Importance = Models.Task.Priority.Low,
                    IsCompleted = false
                },
                new Models.Task
                {
                    TaskId = 2,
                    Name = "Validation",
                    CategoryId = 10,
                    UserId = 102,
                    StartDate = new DateTime(2023, 05, 01),
                    DueDate = new DateTime(2023, 09, 15),
                    Description = "Task2",
                    Importance = Models.Task.Priority.Medium,
                    IsCompleted = false
                },
                new Models.Task
                {
                    TaskId = 3,
                    Name = "Request Ticket RT5",
                    CategoryId = 20,
                    UserId = 102,
                    StartDate = new DateTime(2023, 05, 01),
                    DueDate = new DateTime(2023, 09, 15),
                    Description = "Something very important",
                    Importance = Models.Task.Priority.VeryHigh,
                    IsCompleted = false
                }
            );
        }

        public void SaveDataToFile()
        {
            var dbSets = new List<string>
            {
                JsonSerializer.Serialize(Tasks.ToList()),
                JsonSerializer.Serialize(Categories.ToList()),
                JsonSerializer.Serialize(Users.ToList())
            };
            File.WriteAllLines(fileName, dbSets);
        }

        public void ReadDataFromFile(ModelBuilder modelBuilder)
        {
            var dbSets = File.ReadAllLines(fileName);
            modelBuilder.Entity<Models.Task>().HasData(JsonSerializer.Deserialize<List<Models.Task>>(dbSets[0]));
            modelBuilder.Entity<Category>().HasData(JsonSerializer.Deserialize<List<Category>>(dbSets[1]));
            modelBuilder.Entity<User>().HasData(JsonSerializer.Deserialize<List<User>>(dbSets[2]));
        }
    }
}