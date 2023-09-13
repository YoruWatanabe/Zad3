using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todo.Models
{
    public class Category
    {
        public int CategoryId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual ICollection<Task>? Tasks { get; set; } = null;
        public bool IsSelected { get; set; } = false;
    }
}
