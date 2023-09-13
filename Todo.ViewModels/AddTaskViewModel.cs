using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Todo.Data;
using Todo.Interfaces;
using Todo.Models;
using Task = Todo.Models.Task;

namespace Todo.ViewModels
{
    public class AddTaskViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly TodoContext _context;
        private readonly IDialogService _dialogService;

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "TaskId" && TaskId <= 0)
                    return "TaskId is Required";

                if (columnName == "Name" && string.IsNullOrEmpty(Name))
                    return "Name is Required";

                if (columnName == "Description" && string.IsNullOrEmpty(Description))
                    return "Description is Required";

                if (columnName == "StartDate" && StartDate == null)
                    return "StartDate is Required";

                if (columnName == "DueDate" && DueDate == null)
                    return "DueDate is Required";

                if (columnName == "CategoryId" && CategoryId <= 0)
                    return "CategoryId is Required";

                if (columnName == "UserId" && UserId <= 0)
                    return "UserId is Required";

                return string.Empty;
            }
        }

        private int _taskId;
        public int TaskId
        {
            get => _taskId;
            set
            {
                _taskId = value;
                OnPropertyChanged(nameof(TaskId));
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private Task.Priority _importance = Task.Priority.Low;
        public Task.Priority Importance
        {
            get => _importance;
            set
            {
                _importance = value;
                OnPropertyChanged(nameof(Importance));
            }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime? _dueDate;
        public DateTime? DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        private int _categoryId;
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }

        private ObservableCollection<Category>? _assignedCategories;
        public ObservableCollection<Category>? AssignedCategories
        {
            get
            {
                if (_assignedCategories is null)
                {
                    _assignedCategories = LoadCategories();
                    return _assignedCategories;
                }
                return _assignedCategories;
            }
            set
            {
                _assignedCategories = value;
                OnPropertyChanged(nameof(AssignedCategories));
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        private string _response = string.Empty;
        public string Response
        {
            get => _response;
            set
            {
                _response = value;
                OnPropertyChanged(nameof(Response));
            }
        }

        private ICommand? _back;
        public ICommand? Back
        {
            get => _back ??= new RelayCommand<object>(NavigateBack);
        }

        private void NavigateBack(object? obj)
        {
            var instance = MainWindowViewModel.Instance();
            if (instance != null)
            {
                instance.TasksSubView = new TasksViewModel(_context, _dialogService);
            }
        }

        private ICommand? _save;
        public ICommand? Save
        {
            get => _save ??= new RelayCommand<object>(SaveData);
        }

        private ObservableCollection<Category> LoadCategories()
        {
            _context.Database.EnsureCreated();
            _context.Categories.Load();
            return _context.Categories.Local.ToObservableCollection();
        }

        private void SaveData(object? obj)
        {
            if (!IsValid())
            {
                Response = "Please complete all required fields";
                return;
            }

            Task task = new Task
            {
                TaskId = this.TaskId,
                Name = this.Name,
                Description = this.Description,
                Importance = this.Importance,
                StartDate = this.StartDate,
                DueDate = this.DueDate,
                CategoryId = this.CategoryId,
                UserId = this.UserId,
                IsCompleted = this.IsCompleted
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            Response = "Data Saved";
        }

        public AddTaskViewModel(TodoContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;
        }

        private bool IsValid()
        {
            string[] properties = { "TaskId", "Name", "Description", "Importance", "StartDate", "DueDate", "CategoryId", "UserId", "IsCompleted" };
            foreach (string property in properties)
            {
                if (!string.IsNullOrEmpty(this[property]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}