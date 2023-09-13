using System;
using Todo.Interfaces;
using Todo.Data;

namespace Todo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly TodoContext _context;
        private readonly IDialogService _dialogService;

        private int _selectedTab;
        public int SelectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                _selectedTab = value;
                OnPropertyChanged(nameof(SelectedTab));
            }
        }

        private object? _tasksSubView = null;
        public object? TasksSubView
        {
            get
            {
                return _tasksSubView;
            }
            set
            {
                _tasksSubView = value;
                OnPropertyChanged(nameof(TasksSubView));
            }
        }

        private object? _categoriesSubView = null;
        public object? CategoriesSubView
        {
            get
            {
                return _categoriesSubView;
            }
            set
            {
                _categoriesSubView = value;
                OnPropertyChanged(nameof(CategoriesSubView));
            }
        }
        
        private object? _usersSubView = null;
        public object? UsersSubView
        {
            get
            {
                return _usersSubView;
            }
            set
            {
                _usersSubView = value;
                OnPropertyChanged(nameof(UsersSubView));
            }
        }

        private object? _searchSubView = null;
        public object? SearchSubView
        {
            get
            {
                return _searchSubView;
            }
            set
            {
                _searchSubView = value;
                OnPropertyChanged(nameof(SearchSubView));
            }
        }

        private static MainWindowViewModel? _instance = null;
        public static MainWindowViewModel? Instance()
        {
            return _instance;
        }

        internal void NavigateToCategories(TodoContext context, IDialogService dialogService)
        {
            throw new NotImplementedException();
        }

        public MainWindowViewModel(TodoContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;

            if (_instance is null)
            {
                _instance = this;
            }

            TasksSubView = new TasksViewModel(_context, _dialogService);
            CategoriesSubView = new CategoriesViewModel(_context, dialogService);
            UsersSubView = new UsersViewModel(_context, dialogService);
            SearchSubView = new SearchViewModel(_context, _dialogService);
        }
    }
}