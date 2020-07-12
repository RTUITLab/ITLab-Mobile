using ITLab_Mobile.ViewModels.Events;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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


            BindingContext = eventViewModel = App.ServiceProvider.GetService<EventViewModel>();
            eventViewModel.Navigation = this.Navigation;

            //BindingContext = eventViewModel = new EventViewModel()
            //{
            //    Navigation = this.Navigation
            //};
        }
    }
}