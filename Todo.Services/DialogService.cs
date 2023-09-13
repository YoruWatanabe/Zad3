using Todo.Controls;
using Todo.Interfaces;

namespace Todo.Services;

public class DialogService : IDialogService
{
    public bool? Show(string itemName)
    {
        ConfirmationDialog confirmationDialog = new ConfirmationDialog(itemName);
        return confirmationDialog.ShowDialog();
    }
}
