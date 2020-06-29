using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.Extensions;
using ITLab_Mobile.Services;
using ITLab_Mobile.Views.Events;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ITLab_Mobile.ViewModels.Events
{
    public class EventViewModel : BaseViewModel
    {
        public Command EventCommand { get; set; }
        public List<CompactEventViewExtended> Events { get; set; }
        public bool IsRefreshing { get; set; }

        public INavigation Navigation { get; set; }

        public EventViewModel()
        {
            Title = "Events";
            OnPropertyChanged(nameof(Title));

            EventCommand = new Command(async () => await GetEventsAsync());
            IsRefreshing = false;
            GetEventsAsync();
        }

        private CompactEventViewExtended selectedEvent;
        public CompactEventViewExtended SelectedEvent
        {
            get => selectedEvent;
            set
            {
                SetProperty(ref selectedEvent, value);
                if (value != null)
                {
                    NavigateToEvent();
                    SelectedEvent = null;
                }
            }
        }

        async Task GetEventsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = IsRefreshing = true;
            OnPropertyChanged(nameof(IsRefreshing));

            try
            {
                var eventApi = RestService.For<IEventApi>(HttpClientFactory.HttpClient);
                Events = await eventApi.GetEvents();
                OnPropertyChanged(nameof(Events));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        }

        async Task NavigateToEvent()
        {
            await Navigation.PushAsync(new OneEventPage(selectedEvent.Id));
        }
    }
}