﻿using ITLab_Mobile.Models.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.IO;
using System.Net.Http;
using System.Reflection;

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

        public static DelegatingHandler RefreshTokenHandler { get; set; }

        private static string GetOptionFromAppsettings(string optionName)
        {
            var assembly = typeof(HttpClientFactory).GetTypeInfo().Assembly;
            string raw_json = "";
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Data.appsettings.json");
            using (var reader = new StreamReader(stream))
            {
                raw_json = reader.ReadToEnd();
            }
            JObject appsettingsFile = JObject.Parse(raw_json);
            return appsettingsFile[optionName].ToString();
        }

        public static ApiOptions ApiOptions
        {
            get
            {
                return JsonConvert.DeserializeObject<ApiOptions>(
                    GetOptionFromAppsettings(nameof(Models.Options.ApiOptions))
                );
            }
        }

        public static IdentityOptions IdentityOptions
        {
            get
            {
                return JsonConvert.DeserializeObject<IdentityOptions>(
                    GetOptionFromAppsettings(nameof(Models.Options.IdentityOptions))
                );
            }
        }
    }
}
