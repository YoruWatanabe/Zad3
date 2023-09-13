using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Data;
using Todo.Interfaces;
using Todo.Models;

namespace Todo.Services
{
    public class TaskService
    {
        private readonly TodoContext _context;

        public TaskService(TodoContext context)
        {
            _context = context;
        }
    }
}