using ITLab_Mobile.ViewModels.Events;
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


            BindingContext = eventViewModel = new EventViewModel
            {
                Navigation = this.Navigation
            };
        }
    }
}