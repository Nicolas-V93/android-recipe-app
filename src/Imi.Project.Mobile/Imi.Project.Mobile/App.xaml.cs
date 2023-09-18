using Imi.Project.Mobile.Bootstrap;
using Imi.Project.Mobile.Helpers;
using Imi.Project.Mobile.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Imi.Project.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            var syncfusionKey = UserSecretsHelper.Instance["SyncFusionKey"];
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionKey);
            InitializeComponent();

            IocContainer.RegisterDependencies();

            InitNavigation();

        }

        private Task InitNavigation()
        {
            var navigationService = IocContainer.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
