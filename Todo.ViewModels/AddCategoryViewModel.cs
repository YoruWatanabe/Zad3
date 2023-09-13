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
public class AddCategoryViewModel : ViewModelBase, IDataErrorInfo
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
                if (columnName == "CategoryId")
                {
                    if (CategoryId == 0)
                    {
                        return "CategoryId is Required";
                    }
                }
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        return "Name is Required";
                    }
                }
                return string.Empty;
            }
        }

        private int _categoryId = 0;
        public int CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                _categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private ObservableCollection<Task>? _tasks = null;
        public ObservableCollection<Task> Tasks
        {
            get
            {
                if(_tasks is null)
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
            get
            {
                return _response;
            }
            set
            {
                _response = value;
                OnPropertyChanged(nameof(Response));
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
                instance.CategoriesSubView = new CategoriesViewModel(_context, _dialogService);
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

            Category category = new Category
            {
                CategoryId = this.CategoryId,
                Name = this.Name,
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            Response = "Data Saved";
        }

        public AddCategoryViewModel(TodoContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;
        }

        private bool IsValid()
        {
            string[] properties = { "CategoryId", "Name" };
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