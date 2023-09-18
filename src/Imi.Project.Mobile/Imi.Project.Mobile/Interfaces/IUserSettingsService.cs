namespace Imi.Project.Mobile.Interfaces
{
    public interface IUserSettingsService
    {
        void AddSetting(string key, string value);
        string GetSetting(string key);

    }
}
