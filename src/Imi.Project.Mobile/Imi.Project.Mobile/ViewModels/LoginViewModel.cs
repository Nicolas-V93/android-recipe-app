using FluentValidation;
using Imi.Project.Mobile.Helpers;
using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using Imi.Project.Mobile.ViewModels.Base;
using Imi.Project.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private readonly IAuthenticationService _authenticationService;
        private IValidator _loginValidator;

        public LoginViewModel(INavigationService navigationService,
            IDialogService dialogService,
            IAuthenticationService authenticationService,
            IUserSettingsService userSettingsService)
            : base(navigationService, dialogService, userSettingsService)
        {
            _loginValidator = new LoginValidator();
            _authenticationService = authenticationService;
        }

        public Dictionary<string, string> ErrorMessages { get; set; } = new Dictionary<string, string>();
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));

            }
        }


        public ICommand LoginCommand => new Command(OnLogin);
        public ICommand NavigateToRegisterCommand => new Command(OnNavigateToRegister);


        private bool Validate(AuthenticationRequest request)
        {
            ErrorMessages.Clear();

            var validationContext = new ValidationContext<AuthenticationRequest>(request);
            var validationResult = _loginValidator.Validate(validationContext);

            foreach (var e in validationResult.Errors)
            {
                ErrorMessages[e.PropertyName] = e.ErrorMessage;
            }

            OnPropertyChanged(nameof(ErrorMessages));

            return validationResult.IsValid;
        }

        private async void OnLogin()
        {
            var request = new AuthenticationRequest
            {
                Email = this.Email,
                Password = this.Password
            };

            if (!Validate(request))
                return;


            try
            {
                IsBusy = true;
                var authToken = await _authenticationService.Login(request);

                if (authToken != null)
                {

                    await _authenticationService.SetAuthToken(authToken);
                    var decodedToken = HelperMethods.DecodeJwt(authToken);
                    var username = decodedToken.Claims.First(claim => claim.Type == Constants.UsernameClaim).Value;
                    var email = decodedToken.Claims.First(claim => claim.Type == Constants.EmailClaim).Value;
                    var userId = decodedToken.Claims.First(claim => claim.Type == Constants.UserIdClaim).Value;

                    _userSettingsService.AddSetting(Constants.UserIdClaim, userId);
                    _userSettingsService.AddSetting(Constants.UsernameClaim, username);
                    _userSettingsService.AddSetting(Constants.EmailClaim, email);

                    await _navigationService.NavigateToAsync<RecipesViewModel>();
                }
            }
            catch (Exception)
            {
                await _dialogService.ShowDialog("Username and/or Password is incorrect", "Error", "OK");
            }


            IsBusy = false;
        }

        private async void OnNavigateToRegister()
        {
            await _navigationService.PopModalAsync(false);
            await _navigationService.PushModalAsync(new RegisterView(), false);
        }

    }
}
