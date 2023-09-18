using Imi.Project.Mobile.Interfaces;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly INavigationService _navigationService;
        protected readonly IDialogService _dialogService;
        protected readonly IUserSettingsService _userSettingsService;
        private bool _isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public BaseViewModel(INavigationService navigationService,
            IDialogService dialogService,
            IUserSettingsService userSettingsService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _userSettingsService = userSettingsService;
        }

        public virtual Task InitializeAsync(object parameter)
        {
            return Task.FromResult(false);
        }

    }
}
