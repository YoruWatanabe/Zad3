using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Todo.Data;
using Todo.Interfaces;
using Todo.Models;
using Task = Todo.Models.Task;


namespace Todo.ViewModels;

public class SearchViewModel : ViewModelBase
{
    private readonly TodoContext _context;
    private readonly IDialogService _dialogService;

    private bool? _dialogResult = null;
    public bool? DialogResult
    {
        get
        {
            return _dialogResult;
        }
        set
        {
            _dialogResult = value;
        }
    }

    private string _sortingType = "Id";
    public string SortingType
    {
        get { return _sortingType; }
        set
        {
            _sortingType = value;
            OnPropertyChanged(nameof(SortingType));
        }
    }

    private string _firstCondition = string.Empty;
    public string FirstCondition
    {
        get
        {
            return _firstCondition;
        }
        set
        {
            _firstCondition = value;
            OnPropertyChanged(nameof(FirstCondition));
        }
    }

    private string _secondCondition = string.Empty;
    public string SecondCondition
    {
        get
        {
            return _secondCondition;
        }
        set
        {
            _secondCondition = value;
            OnPropertyChanged(nameof(SecondCondition));
        }
    }

    private bool _isVisible;
    public bool IsVisible
    {
        get
        {
            return _isVisible;
        }
        set
        {
            _isVisible = value;
            OnPropertyChanged(nameof(IsVisible));
        }
    }

    private ObservableCollection<Task>? _tasks = null;
    public ObservableCollection<Task>? Tasks
    {
        get
        {
            if (_tasks is null)
            {
                _tasks = new ObservableCollection<Task>();
                return _tasks;
            }
            return _tasks;
        }
        set
        {
            _tasks = value;
            OnPropertyChanged(nameof(Tasks));
        }
    }

    private ICommand? _comboBoxFilterSelectionChanged = null;
    public ICommand? ComboBoxFilterSelectionChanged
    {
        get
        {
            if (_comboBoxFilterSelectionChanged is null)
            {
                _comboBoxFilterSelectionChanged = new RelayCommand<object>(UpdateFilterCondition);
            }
            return _comboBoxFilterSelectionChanged;
        }
    }

    private void UpdateFilterCondition(object? obj)
    {
        if (obj is string objAsString)
        {
            IsVisible = true;
            SecondCondition = string.Empty;

            FirstCondition = objAsString switch
            {
                "Name" => "with name",
                "User" => "with user id",
                "Category" => "with category id",
                _ => FirstCondition // Default value if none of the cases match
            };
        }
    }
        private ICommand? _comboBoxSortSelectionChanged = null;
    public ICommand? ComboBoxSortSelectionChanged
    {
        get
        {
            if (_comboBoxSortSelectionChanged is null)
            {
                _comboBoxSortSelectionChanged = new RelayCommand<object>(UpdateSortCondition);
            }
            return _comboBoxSortSelectionChanged;
        }
    }

    private void UpdateSortCondition(object? obj)
    {
        if (obj is string objAsString)
        {
            IsVisible = true;
            string selectedValue = objAsString;
            SortingType = selectedValue;           
        }
    }

    private ICommand? _search = null;
    public ICommand? Search
    {
        get
        {
            if (_search is null)
            {
                _search = new RelayCommand<object>(SelectData);
            }
            return _search;
        }
    }

    private void SelectData(object? obj)
    {
        _context.Database.EnsureCreated();
        var tasks = _context.Tasks.ToList();
        List<Task> filteredTasks = new();

        Tasks = string.IsNullOrEmpty(SecondCondition)
    ? new ObservableCollection<Task>(tasks)
    : FirstCondition switch
    {
        "with name" => new ObservableCollection<Task>(tasks.Where(x => x.Name.ToLower().Contains(SecondCondition.ToLower()))),
        "with user id" => new ObservableCollection<Task>(tasks.Where(x => x.UserId == Convert.ToInt32(SecondCondition))),
        "with category id" => new ObservableCollection<Task>(tasks.Where(x => x.CategoryId == Convert.ToInt32(SecondCondition))),
        _ => Tasks // Default value if none of the cases match
    };

        Tasks = SortingType switch
        {
            "Name" => new ObservableCollection<Task>(Tasks.OrderBy(x => x.Name)),
            "Due date" => new ObservableCollection<Task>(Tasks.OrderBy(x => x.DueDate)),
            "Importance" => new ObservableCollection<Task>(Tasks.OrderBy(x => x.Importance)),
            _ => new ObservableCollection<Task>(Tasks.OrderBy(x => x.TaskId))
        };
    }
    private ICommand? _edit = null;
    public ICommand? Edit
    {
        get
        {
            if (_edit is null)
            {
                _edit = new RelayCommand<object>(EditItem);
            }
            return _edit;
        }
    }

    private void EditItem(object? obj)
    {
        if (obj is not null)
        {
            int taskId = (int)obj;
            EditTaskViewModel editTaskViewModel = new EditTaskViewModel(_context, _dialogService)
            {
                TaskId = taskId
            };
            var instance = MainWindowViewModel.Instance();
            if (instance is not null)
            {
                instance.TasksSubView = editTaskViewModel;
                instance.SelectedTab = 0;
            }
        }
    }

    private ICommand ?_remove = null;
    public ICommand? Remove
    {
        get
        {
            if (_remove is null)
            {
                _remove = new RelayCommand<object>(RemoveItem);
            }
            return _remove;
        }
    }

    private void RemoveItem(object? obj)
    {
        if (obj is not null)
        {
            
                int taskId = (int)obj;
                Task? task = _context.Tasks.Find(taskId);
                if (task is null)
                {
                    return;
                }

                DialogResult = _dialogService.Show(task.Name);
                if (DialogResult == false)
                {
                    return;
                }
                _context.Tasks.Remove(task);
                _context.SaveChanges();           
        }
    }

    public SearchViewModel(TodoContext context, IDialogService dialogService)
    {
        _context = context;
        _dialogService = dialogService;

        IsVisible = false;
    }
}
