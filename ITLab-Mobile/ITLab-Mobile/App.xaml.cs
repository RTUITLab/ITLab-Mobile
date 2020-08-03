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

                var mainPage = new MainPage();
                //mainPage.BarBackgroundColor = Color.FromHex("#1a1a1a");
                //mainPage.BarTextColor = Color.FromHex("#d6d6d6");
                //mainPage.SelectedTabColor = Color.FromHex("#e2e2e2");
                //mainPage.UnselectedTabColor = Color.FromHex("#d6d6d6");

                var navPage = new NavigationPage(mainPage);
                //navPage.BarBackgroundColor = Color.FromHex("#1a1a1a");
                //navPage.BarTextColor = Color.FromHex("#d6d6d6");
                //navPage.BackgroundColor = Color.FromHex("#1a1a1a");

                MainPage = navPage;
            }
        }

        protected override void OnStart()
        {
            Current.Resources.MergedDictionaries.Add(Settings.CurrentTheme);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
