using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Todo.Data;
using Todo.Interfaces;
using Todo.Services;
using Todo.ViewModels;

namespace Todo.Main
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
           
            MainWindow mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<TodoContext>(options =>
            {
                options.UseInMemoryDatabase("TodoDB");
                options.UseLazyLoadingProxies();
            });
            serviceCollection.AddSingleton<IDialogService, DialogService>();
            serviceCollection.AddSingleton<MainWindowViewModel>();
            serviceCollection.AddSingleton<MainWindow>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ServiceProvider.GetService<TodoContext>().SaveDataToFile();
        }

        private void ApplicationDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.InnerException, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }
    }
}
