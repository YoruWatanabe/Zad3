using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Todo.Data;
using Todo.Extensions;
using Todo.Interfaces;
using Todo.Models;
using Task = Todo.Models.Task;

namespace Todo.ViewModels
{
    public class AddUserViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly TodoContext _context;
        private readonly IDialogService _dialogService;

        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "UserId")
                {
                    if (UserId == 0)
                    {
                        return "UserId is Required";
                    }
                }
                if (columnName == "FirstName")
                {
                    if (string.IsNullOrEmpty(FirstName))
                    {
                        return "First name is Required";
                    }
                }
                if (columnName == "LastName")
                {
                    if (string.IsNullOrEmpty(LastName))
                    {
                        return "Last name is Required";
                    }
                }
                return string.Empty;
            }
        }

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }

        private string _firstName = string.Empty;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        private ObservableCollection<Task>? _tasks = null;
        public ObservableCollection<Task> Tasks
        {
            get
            {
                if (_tasks is null)
                {
                    _tasks = new ObservableCollection<Task>();
                }
                return _tasks;
            }
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        private string _response = string.Empty;
        public string Response
        {
            get { return _response; }
            set
            {
                if (_response != value)
                {
                    _response = value;
                    OnPropertyChanged(nameof(Response));
                }
            }
        }

        private ICommand? _back = null;
        public ICommand? Back
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
            if (instance is not null)
            {
                instance.UsersSubView = new UsersViewModel(_context, _dialogService);
            }
        }

        private ICommand? _save = null;
        public ICommand? Save
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

            User user = new User
            {
                UserId = this.UserId,
                FirstName = this.FirstName,
                LastName = this.LastName
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            Response = "Data Saved";
        }

        public AddUserViewModel(TodoContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;
        }

        private bool IsValid()
        {
            string[] properties = { "UserId", "FirstName", "LastName" };
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