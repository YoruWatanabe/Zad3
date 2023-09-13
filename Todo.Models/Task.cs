using System;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Todo.Models;

namespace Todo.Models
{
    public class Task
    {
        public enum Priority
        {
            Low,
            Medium,
            High,
            VeryHigh
        }

        public int TaskId { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Priority Importance { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public Task()
        {
            // Set default values
            TaskId = 0;
            CategoryId = 0;
            UserId = 0;
            Name = string.Empty;
            Description = string.Empty;
            Importance = Priority.Low;
            StartDate = null;
            DueDate = null;
            IsCompleted = false;
        }
    }
}


