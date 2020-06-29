using Xamarin.Forms;
using ITLab_Mobile.Views;
using ITLab_Mobile.Services;

namespace ITLab_Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            Current.Resources = Settings.GurrentTheme;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
