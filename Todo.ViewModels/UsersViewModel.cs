using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Todo.Data;
using Todo.Interfaces;
using Todo.Models;

namespace Todo.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        private readonly TodoContext _context;
        private readonly IDialogService _dialogService;

        public bool? DialogResult { get; set; }

        public ObservableCollection<User> Users { get; set; }

        public ICommand Add => new RelayCommand<object>(AddNewUser);

        public ICommand Edit => new RelayCommand<object>(EditUser);

        public ICommand Remove => new RelayCommand<object>(RemoveUser);

        public UsersViewModel(TodoContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;

            _context.Database.EnsureCreated();
            _context.Users.Load();
            Users = _context.Users.Local.ToObservableCollection();
        }

        private void AddNewUser(object? obj)
        {
            var instance = MainWindowViewModel.Instance();
            if (instance is not null)
            {
                instance.UsersSubView = new AddUserViewModel(_context, _dialogService);
            }
        }

        private void EditUser(object? obj)
        {
            if (obj is not null)
            {
                int userId = (int)obj;
                EditUserViewModel editUserViewModel = new EditUserViewModel(_context, _dialogService)
                {
                    UserId = userId
                };
                var instance = MainWindowViewModel.Instance();
                if (instance is not null)
                {
                    instance.UsersSubView = editUserViewModel;
                }
            }
        }

        private void RemoveUser(object? obj)
        {
            if (obj is not null)
            {
                int userId = (int)obj;
                User? user = _context.Users.Find(userId);
                if (user is not null)
                {
                    DialogResult = _dialogService.Show(user.FirstName + " " + user.LastName);
                    if (DialogResult == false)
                    {
                        return;
                    }

                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
            }
        }
    }
}