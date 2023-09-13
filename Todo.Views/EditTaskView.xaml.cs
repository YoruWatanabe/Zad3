using System;
using System.Linq;
using System.Windows.Controls;

namespace Todo.Views
{
    public partial class EditTaskView : UserControl
    {
        public EditTaskView()
        {
            InitializeComponent();
            comboBoxImportance.ItemsSource = Enum.GetValues(typeof(Models.Task.Priority)).Cast<Models.Task.Priority>();
        }
    }
}
