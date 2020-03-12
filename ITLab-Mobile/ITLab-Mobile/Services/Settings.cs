using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ITLab_Mobile.Services
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion

        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        private const string AccessTokenKey = "access_token_key";
        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccessTokenKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AccessTokenKey, value);
            }
        }

        private const string RefreshTokenKey = "refresh_token_key";
        public static string RefreshToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(RefreshTokenKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(RefreshTokenKey, value);
            }
        }
    }
}
