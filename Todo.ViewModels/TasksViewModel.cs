using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Todo.Data;
using Todo.Interfaces;
using Todo.Models;
using Task = Todo.Models.Task;

namespace Todo.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {
        private readonly TodoContext _context;
        private readonly IDialogService _dialogService;

        public bool? DialogResult { get; set; }

        public ObservableCollection<Task> Tasks { get; set; }

        public ICommand Add => new RelayCommand<object>(AddNewTask);

        public ICommand Edit => new RelayCommand<object>(EditTask);

        public ICommand Remove => new RelayCommand<object>(RemoveTask);

        public TasksViewModel(TodoContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;

            _context.Database.EnsureCreated();
            _context.Tasks.Load();
            Tasks = _context.Tasks.Local.ToObservableCollection();
        }

        private void AddNewTask(object? obj)
        {
            var instance = MainWindowViewModel.Instance();
            if (instance is not null)
            {
                instance.TasksSubView = new AddTaskViewModel(_context, _dialogService);
            }
        }

        private void EditTask(object? obj)
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
                }
            }
        }

        private void RemoveTask(object? obj)
        {
            if (obj is not null)
            {
                int taskId = (int)obj;
                Task? task = _context.Tasks.Find(taskId);
                if (task is not null)
                {
                    DialogResult = _dialogService.Show(task.TaskId + " " + task.Name);
                    if (DialogResult == false)
                    {
                        return;
                    }

                    _context.Tasks.Remove(task);
                    _context.SaveChanges();
                }
            }
        }
    }
}