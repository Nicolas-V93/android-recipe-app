using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.ViewModels;
using Imi.Project.Mobile.ViewModels.Base;
using Imi.Project.Mobile.Views;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Imi.Project.Mobile.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IAuthenticationService _authenticationService;

        public NavigationService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task PushModalAsync(Page page, bool isAnimated)
        {
            var mainPage = Application.Current.MainPage;

            if (mainPage != null)
            {
                await mainPage.Navigation.PushModalAsync(page, isAnimated);
            }
        }

        public async Task PopModalAsync(bool isAnimated)
        {
            var mainPage = Application.Current.MainPage;

            if (mainPage != null)
            {
                await mainPage.Navigation.PopModalAsync(isAnimated);
            }

        }

        public async Task InitializeAsync()
        {
            var authToken = await _authenticationService.GetAuthToken();
            if (string.IsNullOrEmpty(authToken))
            {
                await NavigateToAsync<LandingViewModel>();
            }
            else
            {
                await NavigateToAsync<RecipesViewModel>();
            }
        }

        public async Task PopToRootAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                await mainPage.Navigation.PopToRootAsync();
            }
        }

        public async Task GoBackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                await mainPage.Navigation.PopAsync();
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            if (page is LandingView)
            {
                Application.Current.MainPage = page;
            }
            else
            {
                var navigationPage = Application.Current.MainPage as CustomNavigationView;
                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new CustomNavigationView(page);
                }
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

    }
}

