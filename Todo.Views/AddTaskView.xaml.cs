using System;
using System.Linq;
using System.Windows.Controls;

namespace Todo.Views
{
    public partial class AddTaskView : UserControl
    {
        public AddTaskView()
        {
            InitializeComponent();
            comboBoxImportance.ItemsSource = Enum.GetValues(typeof(Models.Task.Priority)).Cast<Models.Task.Priority>();
        }
    }
}
