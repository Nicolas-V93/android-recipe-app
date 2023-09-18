using Acr.UserDialogs;
using Imi.Project.Mobile.Interfaces;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Services
{
    public class DialogService : IDialogService
    {
        public async Task<bool> ShowConfirmationDialog(string message, string title, string confirmText, string cancelText)
        {
            var result = await UserDialogs.Instance.ConfirmAsync(message, title, confirmText, cancelText);
            return result;
        }

        public async Task ShowDialog(string message, string title, string buttonText)
        {
            await UserDialogs.Instance.AlertAsync(message, title, buttonText);
        }
    }
}
