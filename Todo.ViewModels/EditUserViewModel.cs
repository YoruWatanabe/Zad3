using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Todo.Data;
using Todo.Interfaces;
using Todo.Models;

namespace Todo.ViewModels
{
    public class EditUserViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly TodoContext _context;
        private readonly IDialogService _dialogService;
        private User? _user = new User();

        public string Error { get; } = string.Empty;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "UserId" && UserId <= 0)
                {
                    return "UserId is Required";
                }
                if ((columnName == "FirstName" || columnName == "LastName") && string.IsNullOrEmpty(GetPropertyValue(columnName)))
                {
                    return $"{columnName} is Required";
                }
                return string.Empty;
            }
        }

        public int UserId
        {
            get => _user.UserId;
            set
            {
                _user.UserId = value;
                OnPropertyChanged(nameof(UserId));
                LoadUserData();
            }
        }

        public string FirstName
        {
            get => _user.FirstName;
            set
            {
                _user.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _user.LastName;
            set
            {
                _user.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public ObservableCollection<Models.Task>? Tasks { get; set; }

        public string Response { get; set; } = string.Empty;

        public ICommand Back => new RelayCommand<object>(NavigateBack);

        private void NavigateBack(object? obj)
        {
            var instance = MainWindowViewModel.Instance();
            if (instance != null)
            {
                instance.UsersSubView = new UsersViewModel(_context, _dialogService);
            }
        }

        public ICommand Save => new RelayCommand<object>(SaveData);

        private void SaveData(object? obj)
        {
            if (!IsValid())
            {
                Response = "Please complete all required fields";
                return;
            }

            _context.Entry(_user).State = EntityState.Modified;
            _context.SaveChanges();

            Response = "Data Updated";
        }

        public EditUserViewModel(TodoContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;
        }

        private ObservableCollection<Models.Task> LoadTasks()
        {
            _context.Database.EnsureCreated();
            _context.Tasks.Load();
            return new ObservableCollection<Models.Task>(_context.Tasks.Local.Where(x => x.User.UserId == UserId));
        }

        private bool IsValid()
        {
            return UserId > 0 && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
        }

        private void LoadUserData()
        {
            if (_context?.Users is null)
            {
                return;
            }
            _context.Users.Load();
            _user = _context.Users.Find(UserId);
            if (_user != null)
            {
                FirstName = _user.FirstName;
                LastName = _user.LastName;
                Tasks = LoadTasks();
            }
        }

        private string GetPropertyValue(string propertyName)
        {
            return typeof(EditUserViewModel).GetProperty(propertyName)?.GetValue(this, null)?.ToString() ?? string.Empty;
        }
    }
}