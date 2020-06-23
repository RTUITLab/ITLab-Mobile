using ITLab_Mobile.Api;
using ITLab_Mobile.Services;
using ITLab_Mobile.ViewModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        EventViewModel eventViewModel;

        public EventsPage()
        {
            InitializeComponent();

            BindingContext = eventViewModel = new EventViewModel();
        }
    }
}