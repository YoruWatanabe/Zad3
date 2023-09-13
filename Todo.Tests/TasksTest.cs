using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todo.Data;
using Todo.Interfaces;
using Todo.Models;
using Todo.Services;
using Task = Todo.Models.Task;

namespace Todo.Tests
{
    [TestClass]
    public class TasksTest
    {
        private IDialogService _dialogService;
        private DbContextOptions<TodoContext> _options;

        [TestInitialize()]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "TodoTestDB")
                .Options;
            SeedTestDB();
            _dialogService = new DialogService();
        }

        private void SeedTestDB()
        {
            using TodoContext context = new TodoContext(_options);
            {
                context.Database.EnsureDeleted();
                List<Task> tasks = new List<Task>
                {
                    new Task { TaskId = 1,
                        CategoryId = 1,
                        Name = "Uni Task",
                        StartDate = new DateTime(2023, 01, 05),
                        DueDate = new DateTime(2023, 15, 9),
                        Description = "Task1",
                        Importance = Models.Task.Priority.Low,
                        IsCompleted = true }
                };

                List<Category> categories = new List<Category>
                {
                    new Category { CategoryId = 1, Name = "Uni" }
                };

                context.Categories.AddRange(categories);
                context.Tasks.AddRange(tasks);
                context.SaveChanges();
            }
        }
    }
}
