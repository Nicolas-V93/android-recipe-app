using Imi.Project.Mobile.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Imi.Project.Mobile.Interfaces
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
        Task PopToRootAsync();
        Task GoBackAsync();
        Task PushModalAsync(Page page, bool isAnimated);
        Task PopModalAsync(bool isAnimated);
    }
}
