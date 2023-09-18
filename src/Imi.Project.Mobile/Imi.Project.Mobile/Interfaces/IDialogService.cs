using System.Threading.Tasks;

namespace Imi.Project.Mobile.Interfaces
{
    public interface IDialogService
    {
        Task<bool> ShowConfirmationDialog(string message, string title, string confirmText, string cancelText);

        Task ShowDialog(string message, string title, string buttonText);

    }
}
