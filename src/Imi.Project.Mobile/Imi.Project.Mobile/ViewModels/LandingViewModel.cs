using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.ViewModels.Base;
using Imi.Project.Mobile.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class LandingViewModel : BaseViewModel
    {
        public LandingViewModel(INavigationService navigationService,
            IDialogService dialogService,
            IUserSettingsService userSettingsService)
            : base(navigationService, dialogService, userSettingsService)
        {

        }

        public ICommand NavigateToRegisterCommand => new Command(OnNavigateToRegister);
        public ICommand NavigateToLoginCommand => new Command(OnNavigateToLogin);

        private async void OnNavigateToRegister()
        {
            await _navigationService.PushModalAsync(new RegisterView(), true);
        }

        private async void OnNavigateToLogin()
        {
            await _navigationService.PushModalAsync(new LoginView(), true);
        }
    }
}
