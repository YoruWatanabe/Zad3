using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows.Input;
using Todo.Data;
using Todo.Interfaces;
using Todo.Models;

namespace Todo.ViewModels
{
    public class EditTaskViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly TodoContext _context;
        private readonly IDialogService _dialogService;
        private Models.Task? _task = new Models.Task();

        public string Error { get; } = string.Empty;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "TaskId" && TaskId == 0)
                {
                    return "TaskId is Required";
                }
                if ((columnName == "Name" || columnName == "Description" || columnName == "StartDate" || columnName == "DueDate") &&
                    string.IsNullOrEmpty(GetPropertyValue(columnName)))
                {
                    return $"{columnName} is Required";
                }
                if ((columnName == "CategoryId" || columnName == "UserId") &&
                    int.Parse(GetPropertyValue(columnName)) <= 0)
                {
                    return $"{columnName} is Required";
                }
                return string.Empty;
            }
        }

        private int _taskId = 0;
        public int TaskId
        {
            get => _taskId;
            set
            {
                _taskId = value;
                OnPropertyChanged(nameof(TaskId));
                LoadTaskData();
            }
        }

        public int CategoryId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Models.Task.Priority Importance { get; set; } = Models.Task.Priority.Low;

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public string Response { get; set; } = string.Empty;

        private ICommand? _back = null;
        public ICommand Back
        {
            get
            {
                if (_back is null)
                {
                    _back = new RelayCommand<object>(NavigateBack);
                }
                return _back;
            }
        }

        private void NavigateBack(object? obj)
        {
            var instance = MainWindowViewModel.Instance();
            if (instance != null)
            {
                instance.TasksSubView = new TasksViewModel(_context, _dialogService);
            }
        }

        private ICommand? _save = null;
        public ICommand Save
        {
            get
            {
                if (_save is null)
                {
                    _save = new RelayCommand<object>(SaveData);
                }
                return _save;
            }
        }

        private void SaveData(object? obj)
        {
            if (!IsValid())
            {
                Response = "Please complete all required fields";
                return;
            }

            if (_task is null)
            {
                return;
            }

            _task.Name = Name;
            _task.Description = Description;
            _task.Importance = Importance;
            _task.StartDate = StartDate;
            _task.DueDate = DueDate;
            _task.CategoryId = CategoryId;
            _task.UserId = UserId;
            _task.IsCompleted = IsCompleted;

            _context.Entry(_task).State = EntityState.Modified;
            _context.SaveChanges();

            Response = "Data Updated";
        }

        public EditTaskViewModel(TodoContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;
        }

        private bool IsValid()
        {
            string[] properties = { "TaskId", "Name", "Description", "StartDate", "DueDate", "CategoryId", "UserId" };
            foreach (string property in properties)
            {
                if (!string.IsNullOrEmpty(this[property]) ||
                    (property == "CategoryId" || property == "UserId") && int.Parse(GetPropertyValue(property)) <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void LoadTaskData()
        {
            _context.Tasks.Load();
            if (_context?.Tasks is null)
            {
                return;
            }
            _task = _context.Tasks.Find(TaskId);
            if (_task is null)
            {
                return;
            }
            Name = _task.Name;
            Description = _task.Description;
            Importance = _task.Importance;
            StartDate = _task.StartDate;
            DueDate = _task.DueDate;
            CategoryId = _task.CategoryId;
            UserId = _task.UserId;
            IsCompleted = _task.IsCompleted;
        }

        private string GetPropertyValue(string propertyName)
        {
            return typeof(EditTaskViewModel).GetProperty(propertyName)?.GetValue(this, null)?.ToString() ?? string.Empty;
        }
    }
}