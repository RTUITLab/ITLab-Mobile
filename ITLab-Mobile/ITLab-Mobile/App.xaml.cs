using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ITLab_Mobile.Services;
using ITLab_Mobile.Views;

namespace ITLab_Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
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
