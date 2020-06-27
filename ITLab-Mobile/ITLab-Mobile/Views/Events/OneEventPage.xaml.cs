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
    public partial class OneEventPage : ContentPage
    {
        public Guid EventId { get; set; }

        OneEventViewModel oneEventViewModel;

        public OneEventPage(Guid eventId)
        {
            InitializeComponent();

            EventId = eventId;
            BindingContext = oneEventViewModel = new OneEventViewModel(EventId)
            {
                Navigation = this.Navigation
            };
        }
    }
}