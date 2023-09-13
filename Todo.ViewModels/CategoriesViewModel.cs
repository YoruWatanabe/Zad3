using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Todo.Data;
using Todo.Interfaces;
using Todo.Models;

namespace Todo.ViewModels
{
    public class CategoriesViewModel : ViewModelBase
    {
        private readonly TodoContext _context;
        private readonly IDialogService _dialogService;

        public bool? DialogResult { get; set; }
        public ObservableCollection<Category>? Categories { get; set; }
        public ICommand? Add => new RelayCommand<object>(AddNewCategory);
        public ICommand? Edit => new RelayCommand<object>(EditCategory);
        public ICommand? Remove => new RelayCommand<object>(RemoveCategory);

        public CategoriesViewModel(TodoContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;
            _context.Database.EnsureCreated();
            _context.Categories.Load();
            Categories = _context.Categories.Local.ToObservableCollection();
        }

        private void AddNewCategory(object? obj)
        {
            var instance = MainWindowViewModel.Instance();
            if (instance != null)
            {
                instance.CategoriesSubView = new AddCategoryViewModel(_context, _dialogService);
            }
        }

        private void EditCategory(object? obj)
        {
            if (obj != null && obj is int categoryId)
            {
                var editCategoryViewModel = new EditCategoryViewModel(_context, _dialogService)
                {
                    CategoryId = categoryId
                };
                var instance = MainWindowViewModel.Instance();
                if (instance != null)
                {
                    instance.CategoriesSubView = editCategoryViewModel;
                }
            }
        }

        private void RemoveCategory(object? obj)
        {
            if (obj != null && obj is int categoryId)
            {
                var category = _context.Categories.Find(categoryId);
                if (category != null)
                {
                    DialogResult = _dialogService.Show(category.Name);
                    if (DialogResult == false)
                    {
                        return;
                    }

                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                }
            }
        }
    }
}