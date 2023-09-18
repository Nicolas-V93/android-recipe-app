using Imi.Project.Mobile.Interfaces;
using Xamarin.Essentials;

namespace Imi.Project.Mobile.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        public void AddSetting(string key, string value)
        {
            Preferences.Set(key, value);
        }
        public string GetSetting(string key)
        {
            return Preferences.Get(key, "");
        }
    }
}
