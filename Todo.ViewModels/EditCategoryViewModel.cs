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
    public class EditCategoryViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly TodoContext _context;
        private readonly IDialogService _dialogService;
        private Category? _category = new Category();

        public string Error { get; } = string.Empty;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Category" && CategoryId == 0)
                {
                    return "CategoryId is Required";
                }
                if (columnName == "Name" && string.IsNullOrEmpty(Name))
                {
                    return "Name is Required";
                }
                return string.Empty;
            }
        }

        private int _categoryId = 0;
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
                LoadCategoryData();
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

        private ObservableCollection<Models.Task>? _tasks = null;
        public ObservableCollection<Models.Task>? Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
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
                instance.CategoriesSubView = new CategoriesViewModel(_context, _dialogService);
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

            if (_category is null)
            {
                return;
            }
            _category.CategoryId = CategoryId;
            _category.Name = Name;

            _context.Entry(_category).State = EntityState.Modified;
            _context.SaveChanges();

            Response = "Data Updated";
        }

        public EditCategoryViewModel(TodoContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;
        }

        private ObservableCollection<Models.Task> LoadTasks()
        {
            _context.Database.EnsureCreated();
            _context.Tasks.Load();
            return new ObservableCollection<Models.Task>(_context.Tasks.Local.Where(x => x.Category.CategoryId == CategoryId));
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

        private void LoadCategoryData()
        {
            if (_context?.Categories is null)
            {
                return;
            }
            _context.Categories.Load();
            _category = _context.Categories.Find(CategoryId);
            if (_category is null)
            {
                return;
            }
            Name = _category.Name;
            Tasks = LoadTasks();
        }
    }
}