using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.Extensions.Events;
using ITLab_Mobile.Services;
using ITLab_Mobile.Views.Events;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.ViewModels.Events
{
    public class EventViewModel : BaseViewModel
    {
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        public Command EventCommand { get; set; }
        public List<CompactEventViewExtended> Events { get; set; }

        public INavigation Navigation { get; set; }

        private readonly HttpClient httpClient;

        public EventViewModel()
        {
            Title = "Events";

            EventCommand = new Command(async () => await GetEventsAsync());
            IsRefreshing = false;

            httpClient = App.ServiceProvider.GetService<IHttpClientFactory>().CreateClient(Settings.HttpClientName);

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

            try
            {
                var eventApi = RestService.For<IEventApi>(httpClient);
                Events = (await eventApi.GetEvents())
                    .OrderByDescending(key => key.BeginTime)
                    .ToList();
                OnPropertyChanged(nameof(Events));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = IsRefreshing = false;
        }

        async Task NavigateToEvent()
        {
            await Navigation.PushAsync(new OneEventPage(selectedEvent.Id));
        }
    }
}