using Xamarin.Forms;
using ITLab_Mobile.Views;
using ITLab_Mobile.Services;
using System;

namespace ITLab_Mobile
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Settings.AccessToken) || string.IsNullOrEmpty(Settings.RefreshToken))
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
            Current.Resources = Settings.CurrentTheme;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
