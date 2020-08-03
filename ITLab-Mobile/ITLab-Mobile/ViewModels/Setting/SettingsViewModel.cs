using ITLab_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.ViewModels.Setting
{
    public class SettingsViewModel : BaseViewModel
    {
        private bool isDarkTheme = Settings.IsDarkTheme;
        public bool IsDarkTheme
        {
            get => isDarkTheme;
            set { SetProperty(ref isDarkTheme, value); }
        }
        public ICommand ChangeThemeCommand { get; set; }
        public SettingsViewModel()
        {
            Title = "Settings";

            ChangeThemeCommand = new Command(() => ChangeTheme());
        }

        void ChangeTheme()
        {
            Settings.ChangeTheme();
            IsDarkTheme = Settings.IsDarkTheme;
        }
    }
}
