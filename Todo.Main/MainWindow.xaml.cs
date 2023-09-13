using System;
using System.Windows;
using Todo.ViewModels;

namespace Todo.Main
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            InitializeComponent();
            DataContext = _mainWindowViewModel;
        }
    }
}
