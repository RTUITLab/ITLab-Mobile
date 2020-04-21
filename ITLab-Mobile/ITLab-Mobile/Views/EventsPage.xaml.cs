using ITLab_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {

        public EventsPage()
        {
            InitializeComponent();

            GetEvents();
        }

        private async Task GetEvents()
        {
            try
            {
                var response = await HttpClientFactory.HttpClient.GetAsync("/api/event");
                var content = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}