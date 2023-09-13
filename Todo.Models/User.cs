using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todo.Models
{
    public class User
    {
        public int UserId { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual ICollection<Task>? Tasks { get; set; } = null;
        public bool IsSelected { get; set; } = false;
    }
}
