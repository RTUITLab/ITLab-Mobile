using ITLab_Mobile.Api.Models.Event.EventType;
using ITLab_Mobile.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEventPage : ContentPage
    {
        CreateEventViewModel createEventViewModel;
        public CreateEventPage()
        {
            InitializeComponent();

            BindingContext = createEventViewModel = new CreateEventViewModel();
        }

        async void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            await createEventViewModel.UpdateEventTypes();
        }

        async void EventType_Tapped(object sender, EventArgs e)
        {
            createEventViewModel.EventTypeName = (sender as Label).Text;
        }
    }
}