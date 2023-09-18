using FluentValidation;
using Imi.Project.Mobile.Helpers;
using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using Imi.Project.Mobile.ViewModels.Base;
using Imi.Project.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _username;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private DateTime _dateOfBirth;
        private bool _termsAccepted;
        private IValidator _registerValidator;
        private readonly IAuthenticationService _authenticationService;

        public RegisterViewModel(INavigationService navigationService,
            IDialogService dialogService,
            IAuthenticationService authenticationService,
            IUserSettingsService userSettingsService)
            : base(navigationService, dialogService, userSettingsService)
        {
            _registerValidator = new RegisterValidator();
            _authenticationService = authenticationService;
            _dateOfBirth = DateTime.Now;
        }

        public ICommand RegisterCommand => new Command(OnRegister);
        public ICommand NavigateToLoginCommand => new Command(OnNavigateToLogin);

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
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
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));

            }
        }
        public bool TermsAccepted
        {
            get { return _termsAccepted; }
            set
            {
                _termsAccepted = value;
                OnPropertyChanged(nameof(TermsAccepted));
            }
        }
        public Dictionary<string, string> ErrorMessages { get; set; } = new Dictionary<string, string>();


        private bool Validate(RegisterRequest request)
        {
            ErrorMessages.Clear();

            var validationContext = new ValidationContext<RegisterRequest>(request);
            var validationResult = _registerValidator.Validate(validationContext);

            foreach (var e in validationResult.Errors)
            {
                ErrorMessages[e.PropertyName] = e.ErrorMessage;
            }

            OnPropertyChanged(nameof(ErrorMessages));

            return validationResult.IsValid;
        }

        private async void OnRegister()
        {
            var registerRequest = new RegisterRequest
            {
                Username = this.Username,
                Email = this.Email,
                Password = this.Password,
                ConfirmPassword = this.ConfirmPassword,
                DateOfBirth = this.DateOfBirth,
                TermsAccepted = this.TermsAccepted
            };

            if (!Validate(registerRequest))
                return;

            try
            {
                IsBusy = true;
                await _authenticationService.Register(registerRequest);
                await _dialogService.ShowDialog("Registration succesful. You will be redirected to the login page", "Succes", "OK");
                await _navigationService.PopModalAsync(false);
                await _navigationService.PushModalAsync(new LoginView(), false);
            }
            catch (HttpRequestException ex)
            {
                await _dialogService.ShowDialog(ex.Message, "Error", "OK");
            }

            IsBusy = false;

        }

        private async void OnNavigateToLogin()
        {
            await _navigationService.PopModalAsync(false);
            await _navigationService.PushModalAsync(new LoginView(), false);
        }


    }
}
