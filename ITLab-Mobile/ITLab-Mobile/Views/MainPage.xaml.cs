using ITLab_Mobile.Views.Events;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace ITLab_Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            tabbedPage.Children.Insert(0, new NavigationPage(new EventsPage()) { Title = "Events"});
            tabbedPage.CurrentPage = tabbedPage.Children.FirstOrDefault();
            //MakeLogin();
        }

        private async void MakeLogin()
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}
